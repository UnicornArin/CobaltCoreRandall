using Nanoray.PluginManager;
using Nickel;
using System.Reflection;

namespace RandallMod;

internal sealed class Overcharge : Card, IRegisterableCard
{
    //Register
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Overcharge", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModInit.Instance.RandallDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "Overcharge", "name"]).Localize,
            Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/RandallCardArt15.png")).Sprite
        });
    }

    //Traits and Cost
    public override CardData GetData(State state)
        => new()
        {
            cost = 1,
            exhaust = upgrade != Upgrade.B ? true : false
        };

    //Actions
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = ModInit.Instance.OverchargeStatus.Status,
            statusAmount = upgrade != Upgrade.A ? 1 : 2,
        });


        actions.Add(
        new ASynergize
        {
            count = upgrade == Upgrade.None ? 4 : upgrade == Upgrade.A ? 5 : 3,
        });


        return actions;
    }

    public void InjectDialogue()
    {
        DB.story.all[$"{Key()}_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { ModInit.Instance.RandallDeck.Deck.Key() },
            lookup = new() { "RandallModOverchargeB" },
            oncePerCombatTags = new() { "RandallModOverchargeBTag" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = ModInit.Instance.RandallDeck.Deck.Key(),
                    Text = "That should improve the entire deck.",
                    loopTag = "neutral"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.riggs.Key(),
                            Text = "What's a deck?",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.hacker.Key(),
                            Text = "I see this little icon appear all over the place, is this a virus?",
                            loopTag = "mad"
                        }
                    }
                }
            }
        };
    }
}