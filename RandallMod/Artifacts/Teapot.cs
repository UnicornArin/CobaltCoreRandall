using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nickel;

namespace RandallMod.Artifacts
{
    internal class Teapot : Artifact, IRegisterableArtifact
    {
        public static void Register(IModHelper helper)
        {
            helper.Content.Artifacts.RegisterArtifact("TeapotArtifact", new()
            {
                ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
                Meta = new()
                {
                    owner = ModInit.Instance.RandallDeck.Deck,
                    pools = [ArtifactPool.Common]
                },
                Sprite = helper.Content.Sprites.RegisterSprite(ModInit.Instance.Package.PackageRoot.GetRelativeFile("assets/Artifacts/ArtifactTeapot.png")).Sprite,
                Name = ModInit.Instance.AnyLocalizations.Bind(["artifact", "TeapotArtifact", "name"]).Localize,
                Description = ModInit.Instance.AnyLocalizations.Bind(["artifact", "TeapotArtifact", "description"]).Localize,
            });
        }

        public override void OnTurnStart(State s, Combat c) {
            if (c.turn > 0) {
                c.QueueImmediate(
                    new ASynergize { count = 1,
                    artifactPulse = Key()}
                );
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
                    Text = "Slow rolling some <c=cardtrait>Synergized</c> cards.",
                    loopTag = "explain"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.riggs.Key(),
                            Text = "Cards?",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.shard.Key(),
                            Text = "You can speak in colors!",
                            loopTag = "stoked"
                        },
                        new CustomSay()
                        {
                            who = "comp",
                            Text = "More meta concepts?",
                            loopTag = "squint"
                        }
                    }
                }
            }
            };
        }
    }
}