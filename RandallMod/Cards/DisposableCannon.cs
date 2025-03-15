using Nanoray.PluginManager;
using Nickel;
using System.Reflection;

namespace RandallMod;

internal sealed class DisposableCannon : Card
{
    private static ISpriteEntry TopArt = null!;
    private static ISpriteEntry BottomArt = null!;
    //Register
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        TopArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/disposable_BG_top.png"));
        BottomArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/disposable_BG_bottom.png"));
        helper.Content.Cards.RegisterCard("DisposableCannon", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = Deck.colorless,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A],
                dontOffer = true
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "DisposableCannon", "name"]).Localize,
        });
    }

    //Traits and Cost
    public override CardData GetData(State state)
        => new()
        {
            cost = 1,
            floppable = true,
            art = (flipped == false ? TopArt.Sprite : BottomArt.Sprite),
            exhaust = (upgrade == Upgrade.A ? flipped : false)
        };

    //Actions
    public override List<CardAction> GetActions(State s, Combat c)
    {
        if (upgrade == Upgrade.None)
        {
            List<CardAction> actions = [];

            actions.Add(
            new AAttack
            {
                damage = GetDmg(s, 1),
                disabled = flipped
            });

            actions.Add(
            new ADummyAction
            { });

            actions.Add(
            new AAttack
            {
                damage = GetDmg(s, 3),
                disabled = !flipped
            });

            actions.Add(
            new ASelfDestructCard
            {
                canRunAfterKill = true,
                uuid = this.uuid,
                disabled = !flipped
            });

            return actions;
        } else
        {
            List<CardAction> actions = [];

            actions.Add(
            new AAttack
            {
                damage = GetDmg(s, 1),
                disabled = flipped
            });

            actions.Add(
            new ADummyAction
            { });

            actions.Add(
            new AAttack
            {
                damage = GetDmg(s, 2),
                disabled = !flipped
            });

            actions.Add(
            new AExhaustSelfDummy
            {
                uuid = this.uuid,
                disabled = !flipped
            });

            return actions;
        }
    }
}