using Nanoray.PluginManager;
using Nickel;
using System.Reflection;

namespace RandallMod;

internal sealed class EnhancedMagnify : Card, IRegisterableCard
{
    //Register
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("EnhancedMagnify", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModInit.Instance.RandallDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B],
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "EnhancedMagnify", "name"]).Localize,
            Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/RandallCardArt16.png")).Sprite
        });
    }

    //Traits and Cost
    public override CardData GetData(State state)
        => new()
        {
            cost = upgrade != Upgrade.A ? 4 : 3,
            exhaust = true
        };

    //Actions
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new AStatus
        {
            dialogueSelector = ".RandallModEnhancedMagnify",
            targetPlayer = true,
            status = Status.powerdrive,
            statusAmount = 1
        });

        if (upgrade == Upgrade.B)
        {
            actions.Add(
            new AStatus
            {
                targetPlayer = true,
                status = Status.overdrive,
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
            lookup = new() { "RandallModEnhancedMagnify" },
            oncePerCombatTags = new() { "RandallModEnhancedMagnifyTag" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = ModInit.Instance.RandallDeck.Deck.Key(),
                    Text = "Alright, it's time for you guys to deal damage.",
                    loopTag = "accusatory"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.peri.Key(),
                            Text = "Leave it to me.",
                            loopTag = "vengeful"
                        },
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "Heh, sit back and watch.",
                            loopTag = "sly"
                        }
                    }
                }
            }
        };
    }
}