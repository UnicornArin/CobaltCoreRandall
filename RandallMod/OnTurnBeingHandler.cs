using HarmonyLib;
using Microsoft.Extensions.Logging;
using RandallMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandallMod
{

    public class OnTurnBeingHandler
    {
        public static void HarmonyPostfix_Ship_OnBeginTurn(Ship __instance, State s, Combat c)
        {
            if (__instance.Get(ModInit.Instance.CoPilotStatus.Status) > 0)
            {
                c.QueueImmediate(new AStatus
                {
                    status = ModInit.Instance.HalfEvadeStatus.Status,
                    statusAmount = __instance.Get(ModInit.Instance.CoPilotStatus.Status),
                    targetPlayer = true
                });
            }
            if (__instance.Get(ModInit.Instance.AuxiliaryShieldsStatus.Status) > 0)
            {
                c.QueueImmediate(new AStatus
                {
                    status = ModInit.Instance.HalfShieldStatus.Status,
                    statusAmount = __instance.Get(ModInit.Instance.AuxiliaryShieldsStatus.Status),
                    targetPlayer = true
                });
            }
        }
    }
}