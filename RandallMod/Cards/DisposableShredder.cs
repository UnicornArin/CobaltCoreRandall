using Nanoray.PluginManager;
using Nickel;
using System.Reflection;

namespace RandallMod;

internal sealed class DisposableShredder : Card
{
    //Register
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("DisposableShredder", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = Deck.colorless,
                rarity = Rarity.common,
                upgradesTo = [],
                dontOffer = true
            },
            Name = ModInit.Instance.AnyLocalizations.Bind(["card", "DisposableShredder", "name"]).Localize,
            //Art = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/RandallCardArt7.png")).Sprite
        });
    }

    //Traits and Cost
    public override CardData GetData(State state)
        => new()
        {
            cost = 1,
            singleUse = true,
            description = ModInit.Instance.Localizations.Localize(["card", "DisposableShredder", "description"])
        };

    //Actions
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new ADelay
        {
            time = -0.5
        });

        actions.Add(
        new ACardSelect
        {
            filterUUID = this.uuid,
            filterUnremovableAtShops = true,
            browseAction = new DestroySelectedCard(),
            browseSource = CardBrowse.Source.Hand
        });

        return actions;
    }

    public class DestroySelectedCard : CardAction
    {
        public override void Begin(G g, State s, Combat c)
        {
            Card card = selectedCard;
            if (card != null)
            {
                c.QueueImmediate([
                    new ADestroyCard { 
                        uuid = card.uuid,
                        timer = 0,
                    }
                    ]);
            }
        }

        public override string? GetCardSelectText(State s)
        {
            return Loc.T("action.ChooseCardInYourHandToPlayForFree.GetCardSelectText", "Choose a card in your hand. It will be played at no energy cost.");
        }
    }
}