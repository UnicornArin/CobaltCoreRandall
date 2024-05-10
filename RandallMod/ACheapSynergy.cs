namespace RandallMod;

public sealed class ACheapSynergy : CardAction
{
    //Action
    public override void Begin(G g, State s, Combat c)
    {
        c.hand.ForEach(h => {
            if (h.IsSynergized(s)) {
                h.discount = h.discount > -1 ? -1 : h.discount;
            }
        });
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