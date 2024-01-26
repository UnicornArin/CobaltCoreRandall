using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nickel;

namespace RandallMod.Artifacts
{
    internal class RepurposedParts : Artifact
    {
        public static void Register(IModHelper helper)
        {
            helper.Content.Artifacts.RegisterArtifact("RepurposedPartsArtifact", new()
            {
                ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
                Meta = new()
                {
                    owner = ModInit.Instance.RandallDeck.Deck,
                    pools = [ArtifactPool.Boss]
                },
                Sprite = helper.Content.Sprites.RegisterSprite(ModInit.Instance.Package.PackageRoot.GetRelativeFile("assets/Artifacts/ArtifactRepurposedParts.png")).Sprite,
                Name = ModInit.Instance.AnyLocalizations.Bind(["artifact", "RepurposedPartsArtifact", "name"]).Localize,
                Description = ModInit.Instance.AnyLocalizations.Bind(["artifact", "RepurposedPartsArtifact", "description"]).Localize,
            });
        }

        public override void OnTurnStart(State s, Combat c) {
            List<Status> possibleStatuses = new List<Status>();
            if (s.ship.Get(ModInit.Instance.HalfCardStatus.Status) > 0) { possibleStatuses.Add(ModInit.Instance.HalfCardStatus.Status); }
            if (s.ship.Get(ModInit.Instance.HalfEvadeStatus.Status) > 0) { possibleStatuses.Add(ModInit.Instance.HalfEvadeStatus.Status); }
            if (s.ship.Get(ModInit.Instance.HalfShieldStatus.Status) > 0) { possibleStatuses.Add(ModInit.Instance.HalfShieldStatus.Status); }
            if (s.ship.Get(ModInit.Instance.HalfTempShieldStatus.Status) > 0) { possibleStatuses.Add(ModInit.Instance.HalfTempShieldStatus.Status); }
            if (s.ship.Get(ModInit.Instance.HalfDamageStatus.Status) > 0) { possibleStatuses.Add(ModInit.Instance.HalfDamageStatus.Status); }
            
            if (possibleStatuses.Count == 0) return;
            /*
            Status selectedStatus = ModInit.Instance.DummyHalvesStatus.Status; //Dummy or something
            switch (possibleStatuses[s.rngActions.NextInt() % possibleStatuses.Count]) {
                case 0:
                    selectedStatus = ModInit.Instance.HalfCardStatus.Status;
                    break;
                case 1:
                    selectedStatus = ModInit.Instance.HalfEvadeStatus.Status;
                    break;
                case 2:
                    selectedStatus = ModInit.Instance.HalfShieldStatus.Status;
                    break;
                case 3:
                    selectedStatus = ModInit.Instance.HalfTempShieldStatus.Status;
                    break;
                case 4:
                    selectedStatus = ModInit.Instance.HalfTempShieldStatus.Status;
                    break;
            }*/

            c.QueueImmediate([
                new AStatus()
                {
                    targetPlayer = true,
                    status = possibleStatuses[s.rngActions.NextInt() % possibleStatuses.Count],
                    statusAmount = -1,
                    timer = 0.2,
                    artifactPulse = Key()
                },
                new ADrawCard()
                {
                    count = 1
                }
                ]);
        }
    }
}