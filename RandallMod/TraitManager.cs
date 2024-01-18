using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Nanoray.Shrike.Harmony;
using Nanoray.Shrike;
using Microsoft.Extensions.Logging;

namespace RandallMod
{
    public static class TraitManager
    {
        private static ModInit Instance => ModInit.Instance;

        internal static IEnumerable<CodeInstruction> Card_Render_Transpiler(IEnumerable<CodeInstruction> instructions, MethodBase originalMethod)
        {
            try
            {
                return new SequenceBlockMatcher<CodeInstruction>(instructions)
                    .Find(
                        //This finds the buoyant trait within IL, spits out the label for code injection
                        ILMatches.Ldloc<CardData>(originalMethod).ExtractLabels(out var labels).Anchor(out var findAnchor),
                        ILMatches.Ldfld("buoyant"),
                        ILMatches.Brfalse
                    )
                    .Find(
                        //Look for vec5 (position for rendering) and num11 (cardtrait index), extract them
                        ILMatches.Ldloc<Vec>(originalMethod).CreateLdlocInstruction(out var ldlocVec),
                        ILMatches.Ldfld("y"),
                        ILMatches.LdcI4(8),
                        ILMatches.Ldloc<int>(originalMethod).CreateLdlocaInstruction(out var ldlocaCardTraitIndex),
                        ILMatches.Instruction(OpCodes.Dup),
                        ILMatches.LdcI4(1),
                        ILMatches.Instruction(OpCodes.Add),
                        ILMatches.Stloc<int>(originalMethod)
                    )
                    .Anchors().PointerMatcher(findAnchor)
                    .Insert(
                        SequenceMatcherPastBoundsDirection.Before, SequenceMatcherInsertionResultingBounds.IncludingInsertion,
                        new CodeInstruction(OpCodes.Ldarg_1).WithLabels(labels),
                        new CodeInstruction(OpCodes.Ldarg_3),
                        new CodeInstruction(OpCodes.Ldarg_0),
                        ldlocaCardTraitIndex,
                        ldlocVec,
                        new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(TraitManager), nameof(Card_Render_Trait)))
                    )
                    .AllElements();
            }
            catch (Exception ex)
            {
                ModInit.Instance.Logger.LogError("Could not patch method {Method} - {Mod} probably won't work.\nReason: {Exception}", originalMethod, ModInit.Instance.Package.Manifest.UniqueName, ex);
                return instructions;
            }
        }

        private static void Card_Render_Trait(G g, State? state, Card card, ref int cardTraitIndex, Vec vec)
        {
            state ??= g.state;

            if (IsSynergized(card)) {
                Draw.Sprite(Instance.SynergyChargeSprite.Sprite, vec.x, vec.y - 8 * cardTraitIndex++);
            }
        }

        public static bool IsSynergized(this Card self)
            => ModInit.Instance.Helper.ModData.GetModDataOrDefault(self, "IsSynergized", false);

        public static void SetSynergized(this Card self, bool value)
            => ModInit.Instance.Helper.ModData.SetModData(self, "IsSynergized", value);

    }
}
