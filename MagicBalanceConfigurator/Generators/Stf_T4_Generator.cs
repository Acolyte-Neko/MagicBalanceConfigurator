using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicBalanceConfigurator.Generators
{
    public class Stf_T4_Generator : BaseStuffGenerator
    {
        public Stf_T4_Generator(RandomController controller) : base(controller, Consts.Stf_T4_FileName) 
        {
            TierPrefix = CommonTemplates.TierPrefix_T4;
            ItemIdPrefix = CommonTemplates.Stuff_IdPrefix;
            ItemName = "Древний Скипетр Высшего Жреца";            
            ItemType = CommonTemplates.Stuff_RandSufix;
            ModPower = 12;
            ItemsPrice = 10000;
            SetModsCountRange(4, 5);

            Min_ManaCondition = 900;
            Max_ManaCondition = 1200;
            Min_WeaponDamage = 375;
            Max_WeaponDamage = 475;
            Min_WeaponRange = 135;
            Max_WeaponRange = 140;
        }

        protected override string GetItemVisual() => CommonTemplates.StuffT4Visuals.GetRandomElement();
    }
}