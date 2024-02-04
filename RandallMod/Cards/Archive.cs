using Nanoray.PluginManager;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace RandallMod;

internal sealed class Archive : Card, IRegisterableCard
{
    //Register
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Archive", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModInit.Instance.RandallDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B],
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "Archive", "name"]).Localize,
            Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/RandallCardArt14.png")).Sprite
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
            dialogueSelector = ".RandallModSynergyAccelerator",
            targetPlayer = true,
            status = ModInit.Instance.ArchiveStatus.Status,
            statusAmount = 1
        });

        actions.Add(
        new ASynergize
        {
            count = upgrade != Upgrade.A ? 4 : 2
        });


        return actions;
    }

    public void InjectDialogue()
    {
        DB.story.all[$"{Key()}_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { ModInit.Instance.RandallDeck.Deck.Key() },
            lookup = new() { "RandallModSynergyAccelerator" },
            oncePerCombatTags = new() { "RandallModSynergyAcceleratorTag" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = ModInit.Instance.RandallDeck.Deck.Key(),
                    Text = "This will get us more options every two turns.",
                    loopTag = "neutral"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.riggs.Key(),
                            Text = "Cool!",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.dizzy.Key(),
                            Text = "Ah, that's interesting.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };
    }
}