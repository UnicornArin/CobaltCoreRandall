using Nanoray.PluginManager;
using Nickel;
using System.Reflection;

namespace RandallMod;

internal sealed class RandallExe : Card
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("RandallExe", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = Deck.colorless,
<<<<<<< HEAD
                rarity = Rarity.common,
=======
                rarity = Rarity.uncommon,
>>>>>>> Likely-release
                upgradesTo = [Upgrade.A, Upgrade.B],
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "RandallExe", "name"]).Localize,
            Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/RandallCardArt23.png")).Sprite
        });
    }

    public override CardData GetData(State state)
        => new()
        {
            artTint = ModInit.Instance.RandallDeck.Configuration.Definition.color.ToString(),
            cost = upgrade == Upgrade.A ? 0 : upgrade == Upgrade.B ? 2 : 1,
            exhaust = true,
            description = upgrade == Upgrade.B ? ModInit.Instance.Localizations.Localize(["card", "RandallExe", "description", "B"]) :
            ModInit.Instance.Localizations.Localize(["card", "RandallExe", "description", "None"])
        };

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];
        actions.Add(new ACardOffering
        {
<<<<<<< HEAD
            amount = upgrade == Upgrade.B ? 3 : 2,
=======
            amount = 3,
>>>>>>> Likely-release
            limitDeck = ModInit.Instance.RandallDeck.Deck,
            makeAllCardsTemporary = true,
            overrideUpgradeChances = false,
            canSkip = false,
            inCombat = true,
            discount = -1,
<<<<<<< HEAD
            dialogueSelector = $".summon{ModInit.Instance.RandallDeck.UniqueName}",
            timer = upgrade == Upgrade.B ? 2 : 1
=======
            dialogueSelector = $".summon{ModInit.Instance.RandallDeck.UniqueName}"
>>>>>>> Likely-release
        });

        if (upgrade == Upgrade.B)
        {
            actions.Add(new ACardOffering
            {
                amount = 3,
                limitDeck = ModInit.Instance.RandallDeck.Deck,
                makeAllCardsTemporary = true,
                overrideUpgradeChances = false,
                canSkip = false,
                inCombat = true,
                discount = -1,
                dialogueSelector = $".summon{ModInit.Instance.RandallDeck.UniqueName}"
            });
        }
        return actions;
    }
}