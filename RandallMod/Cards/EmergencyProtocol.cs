using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace RandallMod;

internal sealed class EmergencyProtocol : Card
{
    //Register
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("EmergencyProtocol", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModInit.Instance.RandallDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "EmergencyProtocol", "name"]).Localize,
            Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/RandallCardArt18.png")).Sprite
        });
    }

    //Traits and Cost
    public override CardData GetData(State state)
        => new()
        {
            cost = 2,
            exhaust = true,
            retain = upgrade == Upgrade.B
        };

    //Actions
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = Status.perfectShield,
            statusAmount = 1
        });
        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = Status.energyLessNextTurn,
            statusAmount = upgrade == Upgrade.A ? 2 : 1
        });
        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = Status.drawLessNextTurn,
            statusAmount = 2
        });

        return actions;
    }
}