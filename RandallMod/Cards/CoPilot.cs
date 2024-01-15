using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace RandallMod;

internal sealed class CoPilot : Card
{
    //Register
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("CoPilot", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModInit.Instance.RandallDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B],
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "CoPilot", "name"]).Localize
        });
    }

    //Traits and Cost
    public override CardData GetData(State state)
        => new()
        {
            cost = upgrade != Upgrade.A ? 3 : 2,
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
            status = ModInit.Instance.CoPilotStatus.Status,
            statusAmount = 1
        });

        if (upgrade != Upgrade.A)
        {
            actions.Add(
            new AStatus
            {
                targetPlayer = true,
                status = Status.evade,
                statusAmount = 2
            });
        }

        return actions;
    }
}