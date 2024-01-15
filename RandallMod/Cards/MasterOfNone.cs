using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace RandallMod;

internal sealed class MasterOfNone : Card
{
    //Register
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("MasterOfNone", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModInit.Instance.RandallDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "MasterOfNone", "name"]).Localize
        });
    }

    //Traits and Cost
    public override CardData GetData(State state)
        => new()
        {
            cost = 2,
        };

    //Actions
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new AAttack
        {
            damage = GetDmg(s, 3),
            status = ModInit.Instance.HalfDamageStatus.Status,
            statusAmount = 1,
        });
        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = ModInit.Instance.HalfEvadeStatus.Status,
            statusAmount = 1
        });
        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = ModInit.Instance.HalfShieldStatus.Status,
            statusAmount = 1
        });
        if (upgrade == Upgrade.A)
        {
            actions.Add(
            new AStatus
            {
                targetPlayer = true,
                status = ModInit.Instance.HalfTempShieldStatus.Status,
                statusAmount = 1
            });
            actions.Add(
            new AStatus
            {
                targetPlayer = true,
                status = Status.droneShift,
                statusAmount = 1
            });
        }
        if (upgrade == Upgrade.B)
        {
            actions.Add(
            new AStatus
            {
                targetPlayer = true,
                status = Status.shard,
                statusAmount = 1
            });
            actions.Add(
            new AStatus
            {
                targetPlayer = true,
                status = Status.heat,
                statusAmount = -1
            });
        }

        return actions;
    }
}