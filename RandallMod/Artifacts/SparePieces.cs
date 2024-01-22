using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nickel;

namespace RandallMod.Artifacts
{
    internal class SparePieces : Artifact
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
    }
}
