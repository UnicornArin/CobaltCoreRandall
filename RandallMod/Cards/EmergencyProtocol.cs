using Nanoray.PluginManager;
using Nickel;
using System.Reflection;

namespace RandallMod;

internal sealed class EmergencyProtocol : Card, IRegisterableCard
{
    //Register
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("EmergencyProtocol", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModInit.Instance.RandallDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "EmergencyProtocol", "name"]).Localize,
            Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/RandallCardArt18.png")).Sprite
        });
    }

    //Traits and Cost
    public override CardData GetData(State state)
        => new()
        {
            cost = 2,
            exhaust = true,
            retain = true
        };

    //Actions
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new AStatus
        {
            dialogueSelector = ".RandallModEmergencyProtocol",
            targetPlayer = true,
            status = Status.perfectShield,
            statusAmount = 1
        });
        if (upgrade != Upgrade.A)
        {
            actions.Add(
            new AStatus
            {
                targetPlayer = true,
                status = Status.energyLessNextTurn,
                statusAmount = 2
            });
        }
        if (upgrade != Upgrade.B) {
            actions.Add(
            new AStatus
            {
                targetPlayer = true,
                status = Status.drawLessNextTurn,
                statusAmount = 2
            });
        }

        return actions;
    }

    public void InjectDialogue()
    {
        DB.story.all[$"{Key()}_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { ModInit.Instance.RandallDeck.Deck.Key() },
            lookup = new() { "RandallModEmergencyProtocol" },
            oncePerCombatTags = new() { "RandallModEmergencyProtocolTag" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = ModInit.Instance.RandallDeck.Deck.Key(),
                    Text = "Next turn is going to be rough.",
                    loopTag = "squint"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.shard.Key(),
                            Text = "We'll pull through! Just watch.",
                            loopTag = "plan"
                        },
                        new CustomSay()
                        {
                            who = "comp",
                            Text = "I don't like this.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };
    }
}