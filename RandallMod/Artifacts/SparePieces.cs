using System.Reflection;
using Nickel;

namespace RandallMod.Artifacts
{
    internal class SparePieces : Artifact, IRegisterableArtifact
    {
        public static void Register(IModHelper helper)
        {
            helper.Content.Artifacts.RegisterArtifact("SparePiecesArtifact", new()
            {
                ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
                Meta = new()
                {
                    owner = ModInit.Instance.RandallDeck.Deck,
                    pools = [ArtifactPool.Common]
                },
                Sprite = helper.Content.Sprites.RegisterSprite(ModInit.Instance.Package.PackageRoot.GetRelativeFile("assets/Artifacts/ArtifactSpareParts.png")).Sprite,
                Name = ModInit.Instance.AnyLocalizations.Bind(["artifact", "SparePiecesArtifact", "name"]).Localize,
                Description = ModInit.Instance.AnyLocalizations.Bind(["artifact", "SparePiecesArtifact", "description"]).Localize,
            });
        }

        public override List<Tooltip>? GetExtraTooltips()
        => [
            .. StatusMeta.GetTooltips(ModInit.Instance.HalfEvadeStatus.Status, 1),
            .. StatusMeta.GetTooltips(ModInit.Instance.HalfShieldStatus.Status, 1),
            .. StatusMeta.GetTooltips(ModInit.Instance.HalfCardStatus.Status, 1),
        ];

        public override void OnTurnStart(State s, Combat c) {
            if (c.turn > 0) { 

                int value = s.rngActions.NextInt() % 3;
                
                switch (value)
                {
                    case 0:
                        c.QueueImmediate(
                            [new AStatus() { 
                                status = ModInit.Instance.HalfEvadeStatus.Status,
                                statusAmount = 1,
                                targetPlayer = true,
                                artifactPulse = Key()
                            }]
                        );
                        break;
                    case 1:
                        c.QueueImmediate(
                            [new AStatus()
                            {
                                status = ModInit.Instance.HalfShieldStatus.Status,
                                statusAmount = 1,
                                targetPlayer = true,
                                artifactPulse = Key()
                            }]
                        );
                        break;
                    case 2:
                        c.QueueImmediate(
                            [new AStatus()
                            {
                                status = ModInit.Instance.HalfCardStatus.Status,
                                statusAmount = 1,
                                targetPlayer = true,
                                artifactPulse = Key()
                            }]
                        );
                        break;
                }
            }
        }

        public override void OnCombatStart(State state, Combat combat)
        {
            base.OnCombatStart(state, combat);
            Narrative.SpeakBecauseOfAction(MG.inst.g, combat, $".{Key()}Trigger");
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
                    Text = "We can scrap a few things and get extra resources.",
                    loopTag = "explain"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.goat.Key(),
                            Text = "You're not getting those off my drones, are you?",
                            loopTag = "panic"
                        },
                        new CustomSay()
                        {
                            who = Deck.dizzy.Key(),
                            Text = "As long as you're not dismantling my science devices.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.riggs.Key(),
                            Text = "That looks like part of a forklift.",
                            loopTag = "squint"
                        },
                        new CustomSay()
                        {
                            who = Deck.shard.Key(),
                            Text = "I can lend you some of the smaller shards I have.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
            };
        }
    }
}
