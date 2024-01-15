using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nanoray.PluginManager;
using Nickel;

namespace RandallMod
{
    public class AVariableHintFake : AVariableHint
    {
        public int displayAmount;
        

        public AVariableHintFake() : base()
        {
            hand = true;
        }

        public override Icon? GetIcon(State s)
        {
            return new Icon(ModInit.Instance.PartialStatusIcon.Sprite, null, Colors.textMain);
        }

        public override List<Tooltip> GetTooltips(State s) =>
            [new TTText(ModInit.Instance.Localizations.Localize(["action", "AVariableHintFake", "description"], new { Amount = displayAmount.ToString() }))];
    }
}
