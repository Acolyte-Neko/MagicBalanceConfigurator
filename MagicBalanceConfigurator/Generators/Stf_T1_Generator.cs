using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicBalanceConfigurator.Generators
{
    public class Stf_T1_Generator : BaseStuffGenerator
    {
        public Stf_T1_Generator(RandomController controller) : base(controller, Consts.Stf_T1_FileName) 
        {
            TierPrefix = CommonTemplates.TierPrefix_T1;
            ItemIdPrefix = CommonTemplates.Stuff_IdPrefix;
            ItemName = "Старый Посох";            
            ItemType = CommonTemplates.Stuff_RandSufix;
            ModPower = 4;
            ItemsPrice = 1500;
            SetModsCountRange(1, 2);

            Min_ManaCondition = 60;
            Max_ManaCondition = 120;
            Min_WeaponDamage = 75;
            Max_WeaponDamage = 175;
            Min_WeaponRange = 120;
            Max_WeaponRange = 125;
        }

        protected override string GetItemVisual() => CommonTemplates.StuffT1Visuals.GetRandomElement();
    }
}