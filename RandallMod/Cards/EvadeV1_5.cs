using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace RandallMod;

internal sealed class EvadeV1_5 : Card
{
    //Register
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("EvadeV1_5", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModInit.Instance.RandallDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "EvadeV1_5", "name"]).Localize,
            Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/RandallCardArt8.png")).Sprite

        });
    }

    //Traits and Cost
    public override CardData GetData(State state)
        => new()
        {
            cost = 1,
        };

    //Actions
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = Status.evade,
            statusAmount = 1
        });

        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = ModInit.Instance.HalfEvadeStatus.Status,
            statusAmount = 1,
            timer = 0.2
        });

        if (upgrade == Upgrade.A)
        {
            actions.Add(
            new AStatus
            {
                targetPlayer = true,
                status = Status.tempShield,
                statusAmount = 1
            });
        }

        if (upgrade == Upgrade.B)
        {
            actions.Add(
            new ASynergize
            {
                count = 6
            });
        }

        return actions;
    }
}