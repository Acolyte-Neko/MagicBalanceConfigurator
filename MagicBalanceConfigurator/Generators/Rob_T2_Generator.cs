using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicBalanceConfigurator.Generators
{
    public class Rob_T2_Generator : BaseRobeGenerator
    {
        public Rob_T2_Generator(RandomController controller) : base(controller, Consts.Rob_T2_FileName) 
        {
            TierPrefix = CommonTemplates.TierPrefix_T2;
            ItemIdPrefix = CommonTemplates.Robe_IdPrefix;
            ItemName = "Мантия Мага";            
            ItemType = CommonTemplates.Robe_RandSufix;
            ModPower = 4;
            ItemsPrice = 5500;
            SetModsCountRange(2, 3);
        }

        override protected void ProcessTemplate(int itemIndex, (string FullId, string Id) itemIdInfo, ItemModsSet modsSet)
        {
            armorID = new Random(GetRandomSeed()).Next(0, CommonTemplates.RobeT2Visuals.Length);
            base.ProcessTemplate(itemIndex, itemIdInfo, modsSet);
        }

        protected override string GetItemVisual() => CommonTemplates.RobeT2Visuals[armorID];
        protected override string GetRandomVisual_Сhange() => CommonTemplates.RobeT2VisualСhanges[armorID];
    }
}