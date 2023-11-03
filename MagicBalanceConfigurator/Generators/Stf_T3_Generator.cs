using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicBalanceConfigurator.Generators
{
    public class Stf_T3_Generator : BaseStuffGenerator
    {
        public Stf_T3_Generator(RandomController controller) : base(controller, Consts.Stf_T3_FileName) 
        {
            TierPrefix = CommonTemplates.TierPrefix_T3;
            ItemIdPrefix = CommonTemplates.Stuff_IdPrefix;
            ItemName = "Магический Жезл Жреца";            
            ItemType = CommonTemplates.Stuff_RandSufix;
            ModPower = 9;
            ItemsPrice = 7500;
            SetModsCountRange(3, 5);

            Min_ManaCondition = 400;
            Max_ManaCondition = 600;
            Min_WeaponDamage = 275;
            Max_WeaponDamage = 375;
            Min_WeaponRange = 130;
            Max_WeaponRange = 135;
        }

        protected override string GetItemVisual() => CommonTemplates.StuffT3Visuals.GetRandomElement();
    }
}