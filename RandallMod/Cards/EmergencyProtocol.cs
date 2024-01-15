using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace RandallMod;

internal sealed class EmergencyProtocol : Card
{
    //Register
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("EmergencyProtocol", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModInit.Instance.RandallDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "EmergencyProtocol", "name"]).Localize
        });
    }

    //Traits and Cost
    public override CardData GetData(State state)
        => new()
        {
            cost = 2,
            exhaust = true
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
            statusAmount = upgrade == Upgrade.B ? 1 : 2
        });
        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = Status.drawLessNextTurn,
            statusAmount = upgrade == Upgrade.None ? 2 : upgrade == Upgrade.A ? 1 : 3
        });

        return actions;
    }
}