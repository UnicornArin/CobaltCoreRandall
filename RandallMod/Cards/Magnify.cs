using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace RandallMod;

internal sealed class Magnify : Card, IRegisterableCard
{
    //Register
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Magnify", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModInit.Instance.RandallDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B],
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "Magnify", "name"]).Localize,
            Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/RandallCardArt10.png")).Sprite
        });
    }

    //Traits and Cost
    public override CardData GetData(State state)
        => new()
        {
            cost = 1,
            recycle = upgrade == Upgrade.B
        };

    //Actions
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new AStatus
        {
            dialogueSelector = ".RandallModMagnify",
            targetPlayer = true,
            status = Status.overdrive,
            statusAmount = 1
        });

        if (upgrade == Upgrade.A)
        {
            actions.Add(
            new ASynergize
            {
                count = 3
            });
        }

        if (upgrade == Upgrade.B)
        {
            actions.Add(
            new AStatus
            {
                targetPlayer = true,
                status = ModInit.Instance.HalfTempShieldStatus.Status,
                statusAmount = 1,
                timer = 0.2
            });
        }

        return actions;
    }

    public void InjectDialogue()
    {
        DB.story.all[$"{Key()}_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Deck.peri.Key() },
            lookup = new() { "RandallModMagnify" },
            oncePerCombatTags = new() { "RandallModMagnifyTag" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = Deck.peri.Key(),
                    Text = "That looks familiar.",
                    loopTag = "squint"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = ModInit.Instance.RandallDeck.Deck.Key(),
                            Text = "I'm just helping you do your job better.",
                            loopTag = "explain"
                        }
                    }
                }
            }
        };
    }
}