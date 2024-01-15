using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace RandallMod;

internal sealed class InParts : Card, IRegisterableCard
{
    //Register
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("InParts", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModInit.Instance.RandallDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "InParts", "name"]).Localize,
            Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/RandallCardArt9.png")).Sprite
        });
    }

    //Traits and Cost
    public override CardData GetData(State state)
        => new()
        {
            cost = 0,

        };

    //Actions
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        if (upgrade != Upgrade.None)
        {
            actions.Add(
            new AStatus
            {
                targetPlayer = true,
                status = Status.energyFragment,
                statusAmount = upgrade == Upgrade.A ? 1 : 2,
                timer = 0.2
            });
        }

        actions.Add(
        new AStatus
        {
            dialogueSelector = ".RandallModAssemblyRequired",
            targetPlayer = true,
            status = ModInit.Instance.HalfEvadeStatus.Status,
            statusAmount = 1,
            timer = 0.2
        });

        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = ModInit.Instance.HalfShieldStatus.Status,
            statusAmount = 1,
            timer = 0.2
        });

        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = ModInit.Instance.HalfCardStatus.Status,
            statusAmount = 1,
            timer = 0.2
        });

        if (upgrade == Upgrade.B)
        {
            actions.Add(
            new AStatus
            {
                targetPlayer = true,
                status = ModInit.Instance.HalfDamageStatus.Status,
                statusAmount = 1
            });
        }

        if (upgrade == Upgrade.A)
        {
            actions.Add(
            new ASynergize
            {
                count = 2
            });
        }

        return actions;
    }

    public void InjectDialogue()
    {
        //DB.story.all[$"{Key()}_0"] = new()
        DB.story.all[$"{Key()}_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { ModInit.Instance.RandallDeck.Deck.Key() },
            lookup = new() { "RandallModAssemblyRequired" },
            oncePerCombatTags = new() { "RandallModAssemblyRequiredTag" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = ModInit.Instance.RandallDeck.Deck.Key(),
                    Text = "They might not be much, but they add up!",
                    loopTag = "neutral"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.goat.Key(),
                            Text = "Could I borrow some of those for my drones?",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.hacker.Key(),
                            Text = "It's the sum of the parts that matter.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };
    }
}