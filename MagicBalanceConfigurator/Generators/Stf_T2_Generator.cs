using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicBalanceConfigurator.Generators
{
    public class Stf_T2_Generator : BaseStuffGenerator
    {
        public Stf_T2_Generator(RandomController controller) : base(controller, Consts.Stf_T2_FileName) 
        {
            TierPrefix = CommonTemplates.TierPrefix_T2;
            ItemIdPrefix = CommonTemplates.Stuff_IdPrefix;
            ItemName = "Посох Мага";            
            ItemType = CommonTemplates.Stuff_RandSufix;
            ModPower = 6;
            ItemsPrice = 4000;
            SetModsCountRange(2, 3);

            Min_ManaCondition = 175;
            Max_ManaCondition = 300;
            Min_WeaponDamage = 175;
            Max_WeaponDamage = 275;
            Min_WeaponRange = 125;
            Max_WeaponRange = 130;
        }

        protected override string GetItemVisual() => CommonTemplates.StuffT2Visuals.GetRandomElement();
    }
}