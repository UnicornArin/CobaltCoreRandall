using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace RandallMod;

internal sealed class SlowBarrage : Card
{
    //Register
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("SlowBarrage", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModInit.Instance.RandallDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "SlowBarrage", "name"]).Localize
        });
    }

    //Traits and Cost
    public override CardData GetData(State state)
        => new()
        {
            cost = 1,
            recycle = upgrade == Upgrade.B ? false : true,
        };

    //Actions
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new AAttack
        {
            damage = GetDmg(s, 1),
            status = ModInit.Instance.HalfDamageStatus.Status,
            statusAmount = 1,
        });

        if (upgrade != Upgrade.B)
        {
            actions.Add(
            new AStatus
            {
                targetPlayer = true,
                status = ModInit.Instance.HalfEvadeStatus.Status,
                statusAmount = 1
            });

            actions.Add(
            new ASynergize
            {
                count = upgrade == Upgrade.None ? 1 : 2
            });
        } else
        {
            actions.Add(
            new AStatus
            {
                targetPlayer = true,
                status = Status.evade,
                statusAmount = 1
            });
        }

        return actions;
    }
}