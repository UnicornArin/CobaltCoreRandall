using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nickel;
using static System.Net.Mime.MediaTypeNames;

namespace RandallMod.Artifacts
{
    internal class BonusSynergy : Artifact, IRegisterableArtifact
    {
        public static void Register(IModHelper helper)
        {
            helper.Content.Artifacts.RegisterArtifact("BonusSynergyArtifact", new()
            {
                ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
                Meta = new()
                {
                    owner = ModInit.Instance.RandallDeck.Deck,
                    pools = [ArtifactPool.Common]
                },
                Sprite = helper.Content.Sprites.RegisterSprite(ModInit.Instance.Package.PackageRoot.GetRelativeFile("assets/Artifacts/ArtifactBonusSynergy.png")).Sprite,
                Name = ModInit.Instance.AnyLocalizations.Bind(["artifact", "BonusSynergyArtifact", "name"]).Localize,
                Description = ModInit.Instance.AnyLocalizations.Bind(["artifact", "BonusSynergyArtifact", "description"]).Localize,
            });
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
                    Text = "We should play more Synergizing cards.",
                    DynamicLoopTag = "explain"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "Is that some teamwork thing?",
                            loopTag = "squint"
                        },
                        new CustomSay()
                        {
                            who = Deck.dizzy.Key(),
                            Text = "I'll just pretend I understood that.",
                            loopTag = "explain"
                        }
                    }
                }
            }
            };
        }
    }
}
