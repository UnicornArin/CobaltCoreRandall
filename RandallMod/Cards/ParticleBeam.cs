using Nanoray.PluginManager;
using Nickel;
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

        actions.Add(new AVariableHintFake
        {
            displayAmount = GetX(s, c),
        });

        actions.Add(new AAttack
        {
            damage = GetDmg(s, GetX(s, c)),
            xHint = 1,
        });
        return actions;
    }

    public int GetX(State s, Combat c) {
        int x = 0;
        if (upgrade != Upgrade.A)
        {
            x = (((s.ship.Get(ModInit.Instance.HalfDamageStatus.Status) + 1 + s.ship.Get(Status.boost)) % 2) +
                s.ship.Get(ModInit.Instance.HalfCardStatus.Status) +
                s.ship.Get(ModInit.Instance.HalfEvadeStatus.Status) +
                s.ship.Get(ModInit.Instance.HalfShieldStatus.Status) +
                s.ship.Get(ModInit.Instance.HalfTempShieldStatus.Status) +
                s.ship.Get(ModInit.Instance.ChargeUpStatus.Status) +
                ((s.ship.Get(Status.energyFragment) % 3)));
        }
        else {
            x = ((((s.ship.Get(ModInit.Instance.HalfDamageStatus.Status) + 1) % 2) +
                s.ship.Get(ModInit.Instance.HalfCardStatus.Status) +
                s.ship.Get(ModInit.Instance.HalfEvadeStatus.Status) +
                s.ship.Get(ModInit.Instance.HalfShieldStatus.Status) +
                s.ship.Get(ModInit.Instance.HalfTempShieldStatus.Status) +
                s.ship.Get(ModInit.Instance.ChargeUpStatus.Status) +
                ((s.ship.Get(Status.energyFragment) + 1 + s.ship.Get(Status.boost)) % 3)));
        }
        return x;
    }
}