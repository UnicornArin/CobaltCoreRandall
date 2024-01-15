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
using Nanoray.PluginManager;
using Nickel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RandallMod.Artifacts;

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

        /*private static void ApplyHarmonyPatches(IPluginPackage<IModManifest> package)
        {
            //Setup Harmony, create a new Harmony named harmony, harmony harmony... harmony
            Harmony harmony = new Harmony(package.Manifest.UniqueName);

            harmony.Patch(
                original: AccessTools.DeclaredMethod(typeof(Card), nameof(Card.GetActionsOverridden)),
                postfix: new HarmonyMethod(AccessTools.DeclaredMethod(typeof(TraitManager), nameof(TraitManager.SynergizeTrait)))
            );
        }*/

        public static void HarmonyPostfix_Card_GetActionsOverridden(State s, Card __instance, List<CardAction> __result) {
            if (__instance.IsSynergized()) {

                //This handles Bonus Energy Boss Artifact
                var synergyPowerArtifact = s.EnumerateAllArtifacts().OfType<SynergyPower>().FirstOrDefault();
                if (synergyPowerArtifact != null)
                {
                    __result.Add(ModInit.Instance.KokoroApi.Actions.MakeHidden(new AStatus()
                    {
                        status = Status.energyFragment,
                        statusAmount = 1,
                        targetPlayer = true,
                        timer = 0.2,
                        artifactPulse = synergyPowerArtifact.Key()
                })
                    );  
                }

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

        public static void HarmonyPostfix_Card_Tooltip(Card __instance, State s, bool showCardTraits, ref IEnumerable<Tooltip> __result)
        {
            if (!showCardTraits)
                return;

            //escape if trait not found
            var isSynergized = IsSynergized(__instance);
            if (!isSynergized)
                return;
            
            static IEnumerable<Tooltip> ModifyTooltips(IEnumerable<Tooltip> tooltips)
            {
                return [
                new CustomTTGlossary(
                    CustomTTGlossary.GlossaryType.action,
                    () => ModInit.Instance.SynergyChargeSprite.Sprite,
                    () => ModInit.Instance.Localizations.Localize(["trait", "Synergized", "name"]),
                    () => ModInit.Instance.Localizations.Localize(["trait", "Synergized", "description"])
                        )
                ];
            }
            //__result = ModifyTooltips(__result);
        }
    }
}
