using FMOD;
using FSPRO;
using Microsoft.Xna.Framework.Audio;
using Newtonsoft.Json;
using RandallMod;
using System.Collections.Generic;

namespace RandallMod;

public sealed class ARemoveAllSynergy : CardAction
{

    //Action
        public override void Begin(G g, State s, Combat c)
    {
        timer = 0;
        for (var i = 0; i < 500; i++)
        {
            //The one time Shockah was wrong
            //if (!ApplySynergy(s.deck, s)) continue;
            //if (!ApplySynergy(c.discard, s)) continue;
            //ApplySynergy(c.hand, s);

            if (!RemoveSingleSynergy(s.deck, s))
            {
                if (!RemoveSingleSynergy(c.discard, s))
                {
                    if (!RemoveSingleSynergy(c.hand, s)) {
                        RemoveSingleSynergy(c.exhausted, s);
                    }
                }
            }
        }
    }

    private bool RemoveSingleSynergy(List<Card> cardList, State s)
    {
        foreach (Card card in cardList.Shuffle(s.rngActions))
        {
            bool isSynergized = TraitManager.IsSynergized(card);
            if (isSynergized)
            {
                TraitManager.SetSynergized(card, false);
                return true;
            }
        }
        return false;
    }

    //Tooltip thing
    /*
    public override List<Tooltip> GetTooltips(State s)
    {
        if (s.route is Combat combat && combat.stuff.TryGetValue(WorldX, out var @object))
            @object.hilight = 2;

        return [
            new CustomTTGlossary(
                CustomTTGlossary.GlossaryType.action,
                () => StableSpr.icons_droneFlip,
                () => Loc.T("action.ASynergize.name"),
                () => Loc.T("action.ASynergize.desc")
            )
        ];
    }*/

    //This requires no icon, as it will have no display ever
    /*public override Icon? GetIcon(State s)
        => new(ModInit.Instance.IconSynzergize.Sprite, Count, Colors.textMain);*/

}