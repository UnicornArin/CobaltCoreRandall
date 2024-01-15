using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace RandallMod;

internal sealed class SynergyShield : Card
{
    //Register
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("SynergyShield", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModInit.Instance.RandallDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "SynergyShield", "name"]).Localize
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
            status = Status.shield,
            statusAmount = 1
        });

        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = ModInit.Instance.ChargeUpStatus.Status,
            statusAmount = 1
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
            new AStatus
            {
                targetPlayer = false,
                status = ModInit.Instance.HalfDamageStatus.Status,
                statusAmount = 1
            });
        }

        return actions;
    }
}