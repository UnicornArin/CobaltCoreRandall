using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nickel;

namespace RandallMod.Artifacts
{
    internal class PatchingProgram : Artifact
    {
        public static void Register(IModHelper helper)
        {
            helper.Content.Artifacts.RegisterArtifact("PatchingProgramArtifact", new()
            {
                ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
                Meta = new()
                {
                    owner = ModInit.Instance.RandallDeck.Deck,
                    pools = [ArtifactPool.Common]
                },
                Sprite = helper.Content.Sprites.RegisterSprite(ModInit.Instance.Package.PackageRoot.GetRelativeFile("assets/Artifacts/ArtifactPatchingProgram.png")).Sprite,
                Name = ModInit.Instance.AnyLocalizations.Bind(["artifact", "PatchingProgramArtifact", "name"]).Localize,
                Description = ModInit.Instance.AnyLocalizations.Bind(["artifact", "PatchingProgramArtifact", "description"]).Localize,
            });
        }

        public override void OnReceiveArtifact(State state)
        {
            state.GetCurrentQueue().QueueImmediate(new AUpgradeCardSelect());
        }
    }
}
