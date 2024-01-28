using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace RandallMod;

internal sealed class ParticleBeam : Card
{
    //Register
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ParticleBeam", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModInit.Instance.RandallDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B],
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "ParticleBeam", "name"]).Localize,
            Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/RandallCardArt17.png")).Sprite
        });
    }

    //Traits and Cost
    public override CardData GetData(State state)
        => new()
        {
            cost = 2,
            retain = upgrade != Upgrade.B ? false : true,
        };

    //Actions
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];
        if (upgrade == Upgrade.A)
        {
            actions.Add(new AStatus
            {
                targetPlayer = true,
                status = Status.energyFragment,
                statusAmount = 1,
                timer = 0.2
            });
        }

        actions.Add(new AStatus
        {
            targetPlayer = true,
            status = ModInit.Instance.HalfDamageStatus.Status,
            statusAmount = 1,
            timer = 0.2
        });

        actions.Add(new AVariableHint
        {
            status = ModInit.Instance.DummyHalvesStatus.Status
        });

        actions.Add(new AAttack
        {
            damage = GetDmg(s, s.ship.Get(ModInit.Instance.HalfDamageStatus.Status) +
                s.ship.Get(ModInit.Instance.HalfCardStatus.Status) +
                s.ship.Get(ModInit.Instance.HalfEvadeStatus.Status) +
                s.ship.Get(ModInit.Instance.HalfShieldStatus.Status) +
                s.ship.Get(ModInit.Instance.HalfTempShieldStatus.Status) +
                s.ship.Get(ModInit.Instance.ChargeUpStatus.Status) +
                s.ship.Get(Status.energyFragment)),
            xHint = 1,
        });
        return actions;
    }
}