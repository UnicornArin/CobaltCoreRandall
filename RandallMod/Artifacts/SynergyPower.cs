using System.Reflection;
using Newtonsoft.Json;
using Nickel;

namespace RandallMod.Artifacts
{
    internal class SynergyPower : Artifact, IRegisterableArtifact
    {
        private static ISpriteEntry ActiveSprite = null!;
        private static ISpriteEntry InactiveSprite = null!;

        [JsonProperty]
        public bool TriggeredThisTurn;
        public static void Register(IModHelper helper)
        {
            ActiveSprite = helper.Content.Sprites.RegisterSprite(ModInit.Instance.Package.PackageRoot.GetRelativeFile("assets/Artifacts/ArtifactSynergyPower.png"));
            InactiveSprite = helper.Content.Sprites.RegisterSprite(ModInit.Instance.Package.PackageRoot.GetRelativeFile("assets/Artifacts/ArtifactSynergyPowerInactive.png"));

            helper.Content.Artifacts.RegisterArtifact("SynergyPowerArtifact", new()
            {
                
                ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
                Meta = new()
                {
                    owner = ModInit.Instance.RandallDeck.Deck,
                    pools = [ArtifactPool.Boss]
                },
                Sprite = ActiveSprite.Sprite,
                Name = ModInit.Instance.AnyLocalizations.Bind(["artifact", "SynergyPowerArtifact", "name"]).Localize,
                Description = ModInit.Instance.AnyLocalizations.Bind(["artifact", "SynergyPowerArtifact", "description"]).Localize,
            });
        }

        public override Spr GetSprite()
        => (TriggeredThisTurn ? InactiveSprite : ActiveSprite).Sprite;

        public override List<Tooltip>? GetExtraTooltips()
        => [
            .. ModInit.Instance.SynergizedTrait.Configuration.Tooltips?.Invoke(MG.inst.g.state, null) ?? [],
            .. StatusMeta.GetTooltips(Status.energyFragment, 1),
        ];

        public override void OnCombatStart(State state, Combat combat)
        {
            base.OnCombatStart(state, combat);
            Narrative.SpeakBecauseOfAction(MG.inst.g, combat, $".{Key()}Trigger");
            TriggeredThisTurn = false;
        }

        public override void OnTurnStart(State state, Combat combat)
        {
            base.OnTurnStart(state, combat);
            TriggeredThisTurn = false;
        }

        public override void OnPlayerPlayCard(int energyCost, Deck deck, Card card, State state, Combat combat, int handPosition, int handCount)
        {
            if (TriggeredThisTurn == false) {
                base.OnPlayerPlayCard(energyCost, deck, card, state, combat, handPosition, handCount);
                if (!ModInit.Instance.Helper.Content.Cards.IsCardTraitActive(state, card, ModInit.Instance.SynergizedTrait ))
                    return;
                combat.QueueImmediate(new AStatus { targetPlayer = true, status = Status.energyFragment, statusAmount = 1, artifactPulse = Key() });
                TriggeredThisTurn = true;
            }
        }

        public void InjectDialogue()
        {
            DB.story.all[$"Artifact{Key()}"] = new()
            {
                type = NodeType.combat,
                oncePerRun = true,
                lookup = new() { $"{Key()}Trigger" },
                oncePerRunTags = new() { $"{Key()}Tag" },
                allPresent = new() { ModInit.Instance.RandallDeck.Deck.Key() },
                hasArtifacts = new() { Key() },
                lines = new()
            {
                new CustomSay()
                {
                    who = ModInit.Instance.RandallDeck.Deck.Key(),
                    Text = "Teamwork!",
                    loopTag = "neutral"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.peri.Key(),
                            Text = "Teamwork.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.riggs.Key(),
                            Text = "Teamwork.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.dizzy.Key(),
                            Text = "Teamwork.",
                            loopTag = "explains"
                        },
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "Ew.",
                            loopTag = "squint"
                        },
                        new CustomSay()
                        {
                            who = Deck.shard.Key(),
                            Text = "The power of friendship!",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.goat.Key(),
                            Text = "Teamwork.",
                            loopTag = "eyesClosed"
                        },
                        new CustomSay()
                        {
                            who = Deck.hacker.Key(),
                            Text = "Teamwork!",
                            loopTag = "intense"
                        },
                        new CustomSay()
                        {
                            who = "comp",
                            Text = "I don't know what's happening but... Teamwork!",
                            loopTag = "neutral"
                        },
                    }
                }
            }
            };
        }
    }
}
