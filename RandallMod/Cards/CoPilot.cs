using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace RandallMod;

internal sealed class CoPilot : Card, IRegisterableCard
{
    //Register
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("CoPilot", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModInit.Instance.RandallDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B],
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "CoPilot", "name"]).Localize,
            Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/RandallCardArt13.png")).Sprite
        });
    }

    //Traits and Cost
    public override CardData GetData(State state)
        => new()
        {
            cost = upgrade != Upgrade.A ? 3 : 2,
            exhaust = true,
            retain = upgrade == Upgrade.B
        };

    //Actions
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new AStatus
        {
            dialogueSelector = ".RandallModCopilot",
            targetPlayer = true,
            status = ModInit.Instance.CoPilotStatus.Status,
            statusAmount = 1
        });

        if (upgrade != Upgrade.A)
        {
            actions.Add(
            new AStatus
            {
                targetPlayer = true,
                status = ModInit.Instance.HalfEvadeStatus.Status,
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
            allPresent = new() { ModInit.Instance.RandallDeck.Deck.Key() },
            lookup = new() { "RandallModCopilot" },
            oncePerCombatTags = new() { "RandallModCopilotTag" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = ModInit.Instance.RandallDeck.Deck.Key(),
                    Text = "This should get us some movement and energy.",
                    loopTag = "neutral"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.riggs.Key(),
                            Text = "Do I get to sit back and relax then?",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "Why do you do everything in halves?",
                            loopTag = "squint"
                        }
                    }
                }
            }
        };
    }
}