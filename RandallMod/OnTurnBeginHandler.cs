namespace RandallMod
{
    public class OnTurnBeginHandler
    {
        public static void HarmonyPostfix_Ship_OnBeginTurn(Ship __instance, State s, Combat c)
        {
            if (__instance.Get(ModInit.Instance.CoPilotStatus.Status) > 0)
            {
                c.QueueImmediate([new AStatus
                {
                    timer = 0,
                    status = ModInit.Instance.HalfEvadeStatus.Status,
                    statusAmount = __instance.Get(ModInit.Instance.CoPilotStatus.Status),
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.energyFragment,
                    statusAmount = 1,
                    targetPlayer = true
                }]);
            }
            if (__instance.Get(ModInit.Instance.AuxiliaryShieldsStatus.Status) > 0)
            {
                c.QueueImmediate([new AStatus
                {
                    timer = 0,
                    status = ModInit.Instance.HalfShieldStatus.Status,
                    statusAmount = __instance.Get(ModInit.Instance.AuxiliaryShieldsStatus.Status),
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.energyFragment,
                    statusAmount = 1,
                    targetPlayer = true
                }]);
            }
            if (__instance.Get(ModInit.Instance.ArchiveStatus.Status) > 0)
            {
                c.QueueImmediate([new AStatus
                {
                    timer = 0,
                    status = ModInit.Instance.HalfCardStatus.Status,
                    statusAmount = __instance.Get(ModInit.Instance.ArchiveStatus.Status),
                    targetPlayer = true
                },
                new ASynergize {
                    count = __instance.Get(ModInit.Instance.ArchiveStatus.Status)
                 }]);
            }
        }
    }
}