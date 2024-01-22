using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nickel;

namespace RandallMod.Artifacts
{
    internal class DivertedCharge : Artifact
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
    }
}
