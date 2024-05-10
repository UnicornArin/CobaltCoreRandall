using FSPRO;
using Nickel;
using RandallMod.Artifacts;

namespace RandallMod;

public sealed class ASynergize : CardAction
{
    private static ModInit Instance => ModInit.Instance;

    public required int count = 1;
    //Action

    public override void Begin(G g, State s, Combat c)
    {
        var bonusSynergyArtifact = s.EnumerateAllArtifacts().OfType<BonusSynergy>().FirstOrDefault();
        if (bonusSynergyArtifact != null) {
            count++;
            bonusSynergyArtifact.Pulse();
        }
        for (var i = 0; i < count; i++)
        {
            //The one time Shockah was wrong
            //if (!ApplySynergy(s.deck, s)) continue;
            //if (!ApplySynergy(c.discard, s)) continue;
            //ApplySynergy(c.hand, s);

            if (!ApplySynergy(s.deck, s)) {
                if (!ApplySynergy(c.discard, s)) {
                    ApplySynergy(c.hand, s);
                }
            } 
        }
        Audio.Play(Event.Status_PowerUp);
    }

    private bool ApplySynergy(List<Card> cardList, State s)
    {
        foreach (Card card in cardList.Shuffle(s.rngActions))
        {
            bool isSynergized = TraitManager.IsSynergized(card, s);
            if (!isSynergized)
            {
                TraitManager.SetSynergized(card, s, true);
                return true;
            }
        }
        return false;
    }

    //Tooltip thing
    public override List<Tooltip> GetTooltips(State s)
    {
        //if (s.route is Combat combat && combat.stuff.TryGetValue(WorldX, out var @object))
        //    @object.hilight = 2;

        return [
            new GlossaryTooltip($"action.{ModInit.Instance.Package.Manifest.UniqueName}::Synergize")
            {
                Icon = ModInit.Instance.IconSynzergize.Sprite,
                TitleColor = Colors.action,
                Title = ModInit.Instance.Localizations.Localize(["action", "ASynergize", "name"]),
                Description = ModInit.Instance.Localizations.Localize(["action", "ASynergize", "description"], new { count = count })
            }
        ];
    }

    public override Icon? GetIcon(State s)
        => new(ModInit.Instance.IconSynzergize.Sprite, count, Colors.textMain);

}