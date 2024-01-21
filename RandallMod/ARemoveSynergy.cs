using FMOD;
using FSPRO;
using Microsoft.Xna.Framework.Audio;
using Newtonsoft.Json;
using RandallMod;
using System.Collections.Generic;

namespace RandallMod;

public sealed class ARemoveSynergy : CardAction
{
    [JsonProperty]
    public required int CardId { get; init; }

    //Action
        public override void Begin(G g, State s, Combat c)
    {
        timer = 0;
        Card? card = c.hand.Concat(s.deck).Concat(c.discard).Concat(c.exhausted).FirstOrDefault(c => c.uuid == CardId);
        if (card is null)
            return;
        TraitManager.SetSynergized(card, false);
        //Audio.Play(Event.Status_PowerUp);
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