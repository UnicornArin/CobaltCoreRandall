using System.Reflection;
using Nickel;

namespace RandallMod.Artifacts
{
    internal class SynergyPower : Artifact, IRegisterableArtifact
    {
        public static void Register(IModHelper helper)
        {
            helper.Content.Artifacts.RegisterArtifact("SynergyPowerArtifact", new()
            {
                ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
                Meta = new()
                {
                    owner = ModInit.Instance.RandallDeck.Deck,
                    pools = [ArtifactPool.Boss]
                },
                Sprite = helper.Content.Sprites.RegisterSprite(ModInit.Instance.Package.PackageRoot.GetRelativeFile("assets/Artifacts/ArtifactSynergyPower.png")).Sprite,
                Name = ModInit.Instance.AnyLocalizations.Bind(["artifact", "SynergyPowerArtifact", "name"]).Localize,
                Description = ModInit.Instance.AnyLocalizations.Bind(["artifact", "SynergyPowerArtifact", "description"]).Localize,
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
                            loopTag = "explain"
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
