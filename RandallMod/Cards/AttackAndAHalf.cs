using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace RandallMod;

internal sealed class AttackAndAHalf : Card
{
    //Register
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("AttackAndAHalf", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModInit.Instance.RandallDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "AttackAndAHalf", "name"]).Localize
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
        new AAttack
        {
            damage = GetDmg(s, upgrade == Upgrade.A ? 3 : 2),
            status = ModInit.Instance.HalfDamageStatus.Status,
            statusAmount = 1,
            piercing = upgrade == Upgrade.B
        });

        return actions;
    }
}