using Nanoray.PluginManager;
using Nickel;
using System.Reflection;

namespace RandallMod;

internal sealed class DisposableShield : Card
{
    //Register
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("DisposableShield", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = Deck.colorless,
                rarity = Rarity.common,
                upgradesTo = [],
                dontOffer = true
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "DisposableShield", "name"]).Localize,
            //Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/RandallCardArt7.png")).Sprite
        });
    }

    //Traits and Cost
    public override CardData GetData(State state)
        => new()
        {
            cost = 1,
            floppable = true,
        };

    //Actions
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = Status.shield,
            statusAmount = 1,
            disabled = flipped
        });

        actions.Add(
        new ADummyAction
        {});

        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = Status.shield,
            statusAmount = 1,
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
    }
}