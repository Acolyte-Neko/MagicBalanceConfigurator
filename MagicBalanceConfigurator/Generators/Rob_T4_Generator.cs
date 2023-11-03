using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicBalanceConfigurator.Generators
{
    public class Rob_T4_Generator : BaseRobeGenerator
    {
        public Rob_T4_Generator(RandomController controller) : base(controller, Consts.Rob_T4_FileName) 
        {
            TierPrefix = CommonTemplates.TierPrefix_T4;
            ItemIdPrefix = CommonTemplates.Robe_IdPrefix;
            ItemName = "Магический Покров Архимага";       
            UseUniqName = true;
            ItemType = CommonTemplates.Robe_RandSufix;
            ModPower = 8;
            ItemsPrice = 7500;
            SetModsCountRange(4, 5);
        }

        override protected void ProcessTemplate(int itemIndex, (string FullId, string Id) itemIdInfo, ItemModsSet modsSet)
        {
            armorID = new Random(GetRandomSeed()).Next(0, CommonTemplates.RobeT4Visuals.Length);
            base.ProcessTemplate(itemIndex, itemIdInfo, modsSet);
        }

        protected override string GetItemVisual() => CommonTemplates.RobeT4Visuals[armorID];
        protected override string GetRandomVisual_Сhange() => CommonTemplates.RobeT4VisualСhanges[armorID];
    }
}