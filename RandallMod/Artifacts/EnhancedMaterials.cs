using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nickel;

namespace RandallMod.Artifacts
{
    internal class EnhancedMaterials : Artifact
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
            state.GetCurrentQueue().Queue(
                new AHullMax()
                {
                    targetPlayer = true,
                    amount = 1
                }
            );
            state.GetCurrentQueue().QueueImmediate(
                new AShieldMax()
                {
                    targetPlayer = true,
                    amount = 1
                }
            );
            state.GetCurrentQueue().QueueImmediate(
                new ASetHP()
                {
                    targetPlayer = true,
                    healthAmount = state.ship.hull + 3,
                }
            );
        }
    }
}
