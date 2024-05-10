namespace RandallMod
{
    public class AVariableHintFake : AVariableHint
    {
        public int displayAmount;

        public override Icon? GetIcon(State s)
        {
            return new Icon(ModInit.Instance.PartialStatusIcon.Sprite, null, Colors.textMain);
        }

        public override List<Tooltip> GetTooltips(State s) =>
            [new TTText(ModInit.Instance.Localizations.Localize(["action", "AVariableHintFake", "description", s.route is Combat ? "stateful" : "stateless"], new { Amount = displayAmount.ToString() }))];
    }
}
