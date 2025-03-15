namespace RandallMod;
/// <summary>
/// THIS IS A DUMMY ACTION
/// Exhausting is handled by the trait
/// </summary>
public class AExhaustSelfDummy : CardAction
{
    public int uuid;

    public override void Begin(G g, State s, Combat c)
    {
        /*
        Card thisCard = s.FindCard(uuid);
        s.RemoveCardFromWhereverItIs(uuid);
        c.SendCardToExhaust(s, thisCard);*/
    }

    public override List<Tooltip> GetTooltips(State s)
    {
        Card card = s.FindCard(uuid);
        List<Tooltip> list = new List<Tooltip>
        {
            new TTGlossary("cardtrait.exhaust")
        };
        if (card != null)
        {
            list.Add(new TTCard
            {
                card = card.CopyWithNewId()
            });
        }

        return list;
    }

    public override Icon? GetIcon(State s)
    {
        return new Icon(StableSpr.icons_exhaust, null, Colors.redd);
    }
}