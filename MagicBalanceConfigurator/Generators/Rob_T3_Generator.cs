using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicBalanceConfigurator.Generators
{
    public class Rob_T3_Generator : BaseRobeGenerator
    {
        public Rob_T3_Generator(RandomController controller) : base(controller, Consts.Rob_T3_FileName) 
        {
            TierPrefix = CommonTemplates.TierPrefix_T3;
            ItemIdPrefix = CommonTemplates.Robe_IdPrefix;
            ItemName = "Роба Адепта Магии";      
            UseUniqName = true;
            ItemType = CommonTemplates.Robe_RandSufix;
            ModPower = 5.5;
            ItemsPrice = 6500;
            SetModsCountRange(3, 5);
        }

        override protected void ProcessTemplate(int itemIndex, (string FullId, string Id) itemIdInfo, ItemModsSet modsSet)
        {
            armorID = new Random(GetRandomSeed()).Next(0, CommonTemplates.RobeT3Visuals.Length);
            base.ProcessTemplate(itemIndex, itemIdInfo, modsSet);
        }

        protected override string GetItemVisual() => CommonTemplates.RobeT3Visuals[armorID];
        protected override string GetRandomVisual_Сhange() => CommonTemplates.RobeT3VisualСhanges[armorID];
    }
}