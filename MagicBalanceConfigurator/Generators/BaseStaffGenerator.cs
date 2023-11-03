using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicBalanceConfigurator.Generators
{
    public abstract class BaseStuffGenerator : BaseGenerator
    {
        protected int Min_ManaCondition;
        protected int Max_ManaCondition;
        protected int Min_WeaponDamage;
        protected int Max_WeaponDamage;
        protected int Min_WeaponRange;
        protected int Max_WeaponRange;
        
        protected BaseStuffGenerator(RandomController controller, string fileName) : 
            base(controller, fileName) 
        {
            UseUniqName = true;
        }

        protected override ItemMod[] GetItemsMods() =>
            ItemModsProvider.ItemMods.Where(x => x.IsEnabled && (x.Id < 100 || x.Id > 200)).ToArray();

        protected override void PostProcessTemplate(StringBuilder template)
        {
            template.Replace("[ManaCond]", GetRandomManaCondition().ToString());
            template.Replace("[Damage]", GetRandomWeaponDamage().ToString());
            template.Replace("[Range]", GetRandomWeaponRange().ToString());
        }

        //protected abstract string GetRandomVisual_Сhange();

        protected int GetRandomManaCondition() => new Random(GetRandomSeed()).Next(Min_ManaCondition, Max_ManaCondition);
        protected int GetRandomWeaponDamage() => new Random(GetRandomSeed()).Next(Min_WeaponDamage, Max_WeaponDamage);
        protected int GetRandomWeaponRange() => new Random(GetRandomSeed()).Next(Min_WeaponRange, Max_WeaponRange);

        public override string GetTemplate() => @"instance [IdPrefix][Id](c_item) 
{
    name = stext_itmw_2h_staff_rnd_name;
    cond_atr[2] = atr_mana_max;
    cond_value[2] = [ManaCond];
    damagetotal = [Damage];
    damagetype = dam_blunt;
    description = [NameId];
    flags = item_2hd_axe | item_mission;
    inv_animate = 1;
    mainflag = item_kat_nf;
    material = mat_wood;
    on_equip = equip_[IdPrefix][Id];
    on_unequip = unequip_[IdPrefix][Id];
    range = [Range];
    value = [Price];
    visual = [Visual];
    text[5] = name_value;
    count[5] = value;
};
func void equip_[IdPrefix][Id]()
{
[PrintModsText]
[ModsEquip]
};
func void unequip_[IdPrefix][Id]()
{
[ModsUnEquip]
};
";

    }
}
