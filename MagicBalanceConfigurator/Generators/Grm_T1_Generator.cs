using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicBalanceConfigurator.Generators
{
    public class Grm_T1_Generator : BaseGrimoireGenerator
    {
        public Grm_T1_Generator(RandomController controller) : base(controller, Consts.Grm_T1_FileName) 
        {
            TierPrefix = CommonTemplates.TierPrefix_T1;
            ItemIdPrefix = CommonTemplates.Grimoire_IdPrefix;
            ItemName = "Старая книга";            
            ItemType = CommonTemplates.Grimoire_RandSufix;
            ModPower = 1;
            ItemsPrice = 300;
            SetModsCountRange(1, 2);
        }

        protected override string GetItemVisual() => CommonTemplates.GrimoireT1Visuals.GetRandomElement();
    }
}