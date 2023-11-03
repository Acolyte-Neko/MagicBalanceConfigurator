using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicBalanceConfigurator.Generators
{
    public abstract class BaseRobeGenerator : BaseGenerator
    {
        protected int armorID;
        
        protected BaseRobeGenerator(RandomController controller, string fileName) : 
            base(controller, fileName) 
        {
            UseUniqName = true;
        }

        protected override ItemMod[] GetItemsMods() =>
            ItemModsProvider.ItemMods.Where(x => x.IsEnabled && (x.Id < 100 || x.Id > 200)).ToArray();

        protected override void PostProcessTemplate(StringBuilder template)
        {
            string visual_change = GetRandomVisual_Сhange();
            template.Replace("[Visual_Сhange]", visual_change);
        }

        protected abstract string GetRandomVisual_Сhange();

        public override string GetTemplate() => @"instance [IdPrefix][Id](c_item) 
{
    name = stext_itar_rnd_name;
    description = [NameId];
    mainflag = item_kat_armor;
    flags = 0;
    value = [Price];
    wear = wear_torso;
    visual = [Visual];
    visual_change = [Visual_Сhange];
    visual_skin = 0;
    material = mat_leather;
    on_equip = equip_[IdPrefix][Id];
    on_unequip = unequip_[IdPrefix][Id];
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
