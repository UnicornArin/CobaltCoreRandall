using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nickel;

namespace RandallMod.Artifacts
{
    internal class Teapot : Artifact
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
    }
}