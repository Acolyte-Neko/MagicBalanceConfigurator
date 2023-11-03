using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicBalanceConfigurator.Generators
{
    public abstract class BaseGrimoireGenerator : BaseGenerator
    {
        protected BaseGrimoireGenerator(RandomController controller, string fileName) : 
            base(controller, fileName) 
        {
            UseUniqName = true;
        }

        protected override ItemMod[] GetItemsMods() =>
            ItemModsProvider.ItemMods.Where(x => x.IsEnabled && (x.Id < 100)).ToArray();

        public override string GetTemplate() => @"instance [IdPrefix][Id](c_item) {
    name = stext_item_rnd_name;
    mainflag = item_kat_docs;
    flags = item_multi;
    value = [Price];
    visual = [Visual];
    material = mat_leather;
    scemename = ""MAP"";
    description = [NameId];
[ModsText]
    text[5] = name_value;
    count[5] = value;
    on_state = use[IdPrefix][Id];
    inv_animate = 1;
};
func void use[IdPrefix][Id]() 
{
[ModsEquip]
};
";

    }
}
