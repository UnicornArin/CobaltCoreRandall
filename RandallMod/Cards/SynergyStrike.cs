using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace RandallMod;

internal sealed class SynergyStrike : Card
{
    //Register
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("SynergyStrike", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModInit.Instance.RandallDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "SynergyStrike", "name"]).Localize,
            Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/RandallCardArt21.png")).Sprite
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
            damage = GetDmg(s, upgrade != Upgrade.A ? 3 : 4),
            status = ModInit.Instance.HalfDamageStatus.Status,
            statusAmount = 1
        });

        actions.Add(
        new ASynergize
        {
            count = 4
        });

        if (upgrade == Upgrade.B)
        {
            actions.Add(
            new AStatus
            {
                targetPlayer = true,
                status = Status.energyFragment,
                statusAmount = 2,
                timer = 0.2
            });

            actions.Add(
            new AStatus
            {
                targetPlayer = true,
                status = ModInit.Instance.HalfDamageStatus.Status,
                statusAmount = 1,
                timer = 0.2
            });
        }

        return actions;
    }
}