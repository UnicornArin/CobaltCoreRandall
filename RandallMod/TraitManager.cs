using RandallMod.Artifacts;

namespace RandallMod
{
    public static class TraitManager
    {
        private static ModInit Instance => ModInit.Instance;

        public static bool IsSynergized(this Card self, State state)
            => Instance.Helper.Content.Cards.IsCardTraitActive(state, self, Instance.SynergizedTrait);

        public static void SetSynergized(this Card self, State state, bool value)
            => Instance.Helper.Content.Cards.SetCardTraitOverride(state, self, Instance.SynergizedTrait, value, permanent: false);

        public static void HarmonyPostfix_Card_GetActionsOverridden(State s, Card __instance, List<CardAction> __result) {
            if (__instance.IsSynergized(s)) {

                /* DEPRECATED??
                //This handles Bonus Energy Boss Artifact
                var synergyPowerArtifact = s.EnumerateAllArtifacts().OfType<SynergyPower>().FirstOrDefault();
                if (synergyPowerArtifact != null)
                {
                    if (synergyPowerArtifact.TriggeredThisTurn == false)
                    {
                        //synergyPowerArtifact.TriggeredThisTurn = true;
                        __result.Add(ModInit.Instance.KokoroApi.Actions.MakeHidden(new AStatus()
                        {
                            status = Status.energyFragment,
                            statusAmount = 1,
                            targetPlayer = true,
                            timer = 0.2,
                            artifactPulse = synergyPowerArtifact.Key(),
                        })
                        );
                    }
                }*/

                __result.Add(ModInit.Instance.KokoroApi.Actions.MakeHidden(new AStatus()
                {
                    status = ModInit.Instance.ChargeUpStatus.Status,
                    statusAmount = 1,
                    targetPlayer = true
                })
                );

                __result.Add(ModInit.Instance.KokoroApi.Actions.MakeHidden(new ARemoveSynergy()
                {
                    CardId = __instance.uuid
                })
                );
            }
        }
    }
}
