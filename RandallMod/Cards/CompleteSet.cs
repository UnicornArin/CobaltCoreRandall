using Nanoray.PluginManager;
using Nickel;
using System.Reflection;

namespace RandallMod;

internal sealed class CompleteSet : Card
{
    //Register
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("CompleteSet", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModInit.Instance.RandallDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B],
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "CompleteSet", "name"]).Localize,
            Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/RandallCardArt20.png")).Sprite
        });
    }

    //Traits and Cost
    public override CardData GetData(State state)
        => new()
        {
            cost = upgrade != Upgrade.A ? 2 : 1,
            exhaust = upgrade != Upgrade.B ? true : false,
            retain = upgrade != Upgrade.B ? true : false,
            description = ModInit.Instance.Localizations.Localize(["card", "CompleteSet", "description"])
        };

    //Actions
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(new ACompletePartial { });
        return actions;
    }
}