using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicBalanceConfigurator.Generators
{
    public class Rob_T1_Generator : BaseRobeGenerator
    {
        public Rob_T1_Generator(RandomController controller) : base(controller, Consts.Rob_T1_FileName) 
        {
            TierPrefix = CommonTemplates.TierPrefix_T1;
            ItemIdPrefix = CommonTemplates.Robe_IdPrefix;
            ItemName = "Накидка Послушника";            
            ItemType = CommonTemplates.Robe_RandSufix;
            ModPower = 3;
            ItemsPrice = 4500;
            SetModsCountRange(1, 2);
        }

        override protected void ProcessTemplate(int itemIndex, (string FullId, string Id) itemIdInfo, ItemModsSet modsSet)
        {
            armorID = new Random(GetRandomSeed()).Next(0, CommonTemplates.RobeT1Visuals.Length);
            base.ProcessTemplate(itemIndex, itemIdInfo, modsSet);
        }

        protected override string GetItemVisual() => CommonTemplates.RobeT1Visuals[armorID];
        protected override string GetRandomVisual_Сhange() => CommonTemplates.RobeT1VisualСhanges[armorID];
    }
}