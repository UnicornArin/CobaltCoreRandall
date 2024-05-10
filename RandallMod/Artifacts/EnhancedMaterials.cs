using System.Reflection;
using Nickel;

namespace RandallMod.Artifacts
{
    internal class EnhancedMaterials : Artifact, IRegisterableArtifact
    {
        public static void Register(IModHelper helper)
        {
            helper.Content.Artifacts.RegisterArtifact("EnhancedMaterialsArtifact", new()
            {
                ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
                Meta = new()
                {
                    owner = ModInit.Instance.RandallDeck.Deck,
                    pools = [ArtifactPool.Common]
                },
                Sprite = helper.Content.Sprites.RegisterSprite(ModInit.Instance.Package.PackageRoot.GetRelativeFile("assets/Artifacts/ArtifactEnhancedMaterials.png")).Sprite,
                Name = ModInit.Instance.AnyLocalizations.Bind(["artifact", "EnhancedMaterialsArtifact", "name"]).Localize,
                Description = ModInit.Instance.AnyLocalizations.Bind(["artifact", "EnhancedMaterialsArtifact", "description"]).Localize,
            });
        }

        public override void OnReceiveArtifact(State state)
        {
            /*state.GetCurrentQueue().QueueImmediate(
                new AShieldMax()
                {
                    targetPlayer = true,
                    amount = 1
                }
            );*/

            state.ship.hullMax += 1;
            state.ship.shieldMaxBase += 1;
            int num = 3;
            int num2 = num;
            foreach (Artifact item in state.EnumerateAllArtifacts())
            {
                num2 += item.ModifyHealAmount(num, state, targetPlayer: true);
            }

            state.ship.hull += num2;
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
                        Text = "An extra bit of hull and shields could turn the tide when we need it.",
                        loopTag = "neutral"
                    },
                    new SaySwitch()
                    {
                        lines = new()
                        {
                            new CustomSay()
                            {
                                who = Deck.dizzy.Key(),
                                Text = "Especially the shields.",
                                loopTag = "neutral"
                            },
                            new CustomSay()
                            {
                                who = Deck.eunice.Key(),
                                Text = "That's for cowards, get more offensive upgrades.",
                                loopTag = "neutral"
                            }
                        }
                    }
                }
            };
        }
    }
}
