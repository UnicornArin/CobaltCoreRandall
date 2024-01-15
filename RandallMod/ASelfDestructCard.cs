using FMOD;
using FSPRO;
using Microsoft.Xna.Framework.Audio;
using Newtonsoft.Json;
using RandallMod;
using System.Collections.Generic;

namespace RandallMod;

public class ASelfDestructCard : CardAction
{
    public int uuid;

    public override void Begin(G g, State s, Combat c)
    {
        s.RemoveCardFromWhereverItIs(uuid);
    }

    public override List<Tooltip> GetTooltips(State s)
    {
        Card card = s.FindCard(uuid);
        List<Tooltip> list = new List<Tooltip>
        {
            new TTGlossary("action.destroyCard")
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
        return new Icon(Spr.icons_destroyCard, 1, Colors.redd);
    }
}