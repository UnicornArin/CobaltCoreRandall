using Nanoray.PluginManager;
using Nickel;
using System.Reflection;

namespace RandallMod;

internal sealed class MasterOfNone : Card, IRegisterableCard
{
    //Register
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
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
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "MasterOfNone", "name"]).Localize,
            Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/RandallCardArt6.png")).Sprite
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
            timer = 0.2,
            dialogueSelector = ".RandallModMasterOfNone"
        });
        actions.Add(
        new AStatus
        {
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

        if (upgrade == Upgrade.A)
        {
            actions.Add(
            new AStatus
            {
                targetPlayer = true,
                status = Status.droneShift,
                statusAmount = 1
            });

            actions.Add(
            new AStatus
            {
                dialogueSelector = ".RandallModMasterOfNoneA",
                targetPlayer = true,
                status = Status.heat,
                statusAmount = -1
            });
        }

        if (upgrade == Upgrade.B)
        {
            actions.Add(
            new AStatus
            {
                targetPlayer = true,
                status = Status.droneShift,
                statusAmount = 1
            });

            actions.Add(
            new AStatus
            {
                dialogueSelector = ".RandallModMasterOfNoneB",
                targetPlayer = true,
                status = Status.shard,
                statusAmount = 1
            });
        }

        return actions;
    }

    public void InjectDialogue()
    {
        DB.story.all[$"{Key()}_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { "comp" },
            lookup = new() { "RandallModMasterOfNone" },
            oncePerCombatTags = new() { "RandallModMasterOfNoneTag" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = "comp",
                    Text = "Heeey, wait a minute...",
                    loopTag = "squint"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = ModInit.Instance.RandallDeck.Deck.Key(),
                            Text = "You're not the only generalist here, CAT.",
                            loopTag = "explain"
                        }
                    }
                }
            }
        };
        DB.story.all[$"{Key()}_1"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { ModInit.Instance.RandallDeck.Deck.Key() },
            lookup = new() { "RandallModMasterOfNone" },
            oncePerCombatTags = new() { "RandallModMasterOfNoneTag" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = ModInit.Instance.RandallDeck.Deck.Key(),
                    Text = "It's a bit of everything for everyone.",
                    loopTag = "explain"
                }
            }
        };
        DB.story.all[$"{Key()}_2"] = new()
        {
            type = NodeType.combat,
            lookup = new() { "RandallModMasterOfNoneA" },
            oncePerCombatTags = new() { "RandallModMasterOfNoneATag" },
            oncePerRun = true,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                       new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "Oh you shouldn't have.",
                            loopTag = "sly"
                        }
                    }
                }
            }
        };
        DB.story.all[$"{Key()}_3"] = new()
        {
            type = NodeType.combat,
            lookup = new() { "RandallModMasterOfNoneB" },
            oncePerCombatTags = new() { "RandallModMasterOfNoneBTag" },
            oncePerRun = true,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.shard.Key(),
                            Text = "Look! A shard!",
                            loopTag = "stoked"
                        }
                    }
                }
            }
        };
    }
}