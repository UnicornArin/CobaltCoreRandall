using FMOD;
using FSPRO;
using Microsoft.Xna.Framework.Audio;
using Newtonsoft.Json;
using RandallMod;
using System.Collections.Generic;

namespace RandallMod;

public sealed class ASynergize : CardAction
{
    private static ModInit Instance => ModInit.Instance;

    public required int Count = 1;
    //Action

    public override void Begin(G g, State s, Combat c)
    {
        if (ApplySynergy(s.deck, s)) return;
        if (ApplySynergy(c.discard, s)) return;
        ApplySynergy(c.hand, s);
        Audio.Play(Event.Status_PowerUp);
    }

    private bool ApplySynergy(List<Card> cardList, State s) {
        foreach (Card card in cardList.Shuffle(s.rngActions))
        {
            bool isSynergized = TraitManager.IsSynergized(card);
            if (!isSynergized) 
            {
                TraitManager.SetSynergized(card, true);
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

    public override Icon? GetIcon(State s)
        => new(ModInit.Instance.IconSynzergize.Sprite, Count, Colors.textMain);

}