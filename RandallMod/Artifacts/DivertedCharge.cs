using System.Reflection;
using Nickel;

namespace RandallMod.Artifacts
{
    internal class DivertedCharge : Artifact, IRegisterableArtifact
    {
        public static void Register(IModHelper helper)
        {
            helper.Content.Artifacts.RegisterArtifact("DivertedChargeArtifact", new()
            {
                ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
                Meta = new()
                {
                    owner = ModInit.Instance.RandallDeck.Deck,
                    pools = [ArtifactPool.Boss]
                },
                Sprite = helper.Content.Sprites.RegisterSprite(ModInit.Instance.Package.PackageRoot.GetRelativeFile("assets/Artifacts/ArtifactDivertedCharge.png")).Sprite,
                Name = ModInit.Instance.AnyLocalizations.Bind(["artifact", "DivertedChargeArtifact", "name"]).Localize,
                Description = ModInit.Instance.AnyLocalizations.Bind(["artifact", "DivertedChargeArtifact", "description"]).Localize,
            });
        }

        public override List<Tooltip>? GetExtraTooltips()
        => [
            .. StatusMeta.GetTooltips(ModInit.Instance.ChargeUpStatus.Status, 1),
        ];

        public override void OnTurnStart(State s, Combat c) {
            if (c.turn > 0) { 
                Random r = new Random();
                if (s.ship.Get(ModInit.Instance.ChargeUpStatus.Status) > 1)
                {
                    c.QueueImmediate(
                        [new AEnergy()
                        {
                            changeAmount = 1,
                        },
                        new AStatus()
                        {
                            timer = 0,
                            status = ModInit.Instance.ChargeUpStatus.Status,
                            statusAmount = -2,
                            targetPlayer = true,
                            artifactPulse = Key()
                        },
                        ]
                    ); ;
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
                    Text = "We'll need some careful planning to make this work.",
                    loopTag = "neutral"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.riggs.Key(),
                            Text = "I'll leave the planning part to you.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.goat.Key(),
                            Text = "Planning, sure thing.",
                            loopTag = "writing"
                        }
                    }
                }
            }
            };
        }
    }
}
