using FMOD;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using Nickel;
using RandallMod.Artifacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RandallMod
{
    public sealed class ModInit : SimpleMod
    {

        internal static ModInit Instance { get; private set; } = null!;

        internal Harmony Harmony { get; }
        //Here would be a kokoro reference if I needed one
        internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
        internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }

        //Initialize Statuses
        internal IStatusEntry ChargeUpStatus { get; }
        internal IStatusEntry HalfEvadeStatus { get; }
        internal IStatusEntry HalfShieldStatus { get; }
        internal IStatusEntry HalfTempShieldStatus { get; }
        internal IStatusEntry HalfDamageStatus { get; }
        internal IStatusEntry CoPilotStatus { get; }
        internal IStatusEntry AuxiliaryShieldsStatus { get; }

        //Initialize Deck
        internal IDeckEntry RandallDeck { get; }

        //Initialize Common Artifacts
        internal static IReadOnlyList<Type> CommonArtifacts { get; } = [
            typeof(SparePieces),
            typeof(EnhancedMaterials),
            typeof(PatchingProgram)
        ];
        internal static IReadOnlyList<Type> BossArtifacts { get; } = [
        ];

        //Initialize Cards
        internal static readonly Type[] StarterCards = new Type[]
        {
            typeof(SynergyEvade),
            typeof(SynergyShield),
        };
        internal static readonly Type[] CommonCards = new Type[]
        {
            typeof(AttackAndAHalf),
            typeof(MasterOfNone),
        };
        internal static readonly Type[] UncommonCards = new Type[]
        {
            typeof(EmergencyProtocol),
        };
        internal static readonly Type[] RareCards = new Type[]
        {
            typeof(EnhancedMagnify),
        };

        public ModInit(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
        {
            //This is a constructor, logic goes here
            Instance = this;

            //i18n setup (This has to go on top for reasons
            this.AnyLocalizations = new JsonLocalizationProvider(
            tokenExtractor: new SimpleLocalizationTokenExtractor(),
            localeStreamFunction: locale => package.PackageRoot.GetRelativeFile($"i18n/{locale}.json").OpenRead()
            );
            this.Localizations = new MissingPlaceholderLocalizationProvider<IReadOnlyList<string>>(
                new CurrentLocaleOrEnglishLocalizationProvider<IReadOnlyList<string>>(this.AnyLocalizations)
            );

            //Character definitions
            RandallDeck = helper.Content.Decks.RegisterDeck("Randall", new()
            {
                Definition = new() { color = new Color("3e8ad5"), titleColor = new Color("ffffff") },
                DefaultCardArt = StableSpr.cards_colorless,
                BorderSprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Cards/border_randall.png")).Sprite,
                Name = this.AnyLocalizations.Bind(["character", "name"]).Localize
            });

            helper.Content.Characters.RegisterCharacter("Randall", new()
            {
                Deck = RandallDeck.Deck,
                Description = this.AnyLocalizations.Bind(["character", "description"]).Localize,
                BorderSprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Character/char_randall.png")).Sprite,
                StarterCardTypes = StarterCards,
            });

            //Register cards:
            //Starters
            SynergyEvade.Register(helper);
            SynergyShield.Register(helper);
            //Commons
            AttackAndAHalf.Register(helper);
            MasterOfNone.Register(helper);
            //Uncommons
            EmergencyProtocol.Register(helper);
            //Rares
            EnhancedMagnify.Register(helper);
            CoPilot.Register(helper);
            AuxiliaryShields.Register(helper);

            //CharacterAnimations
            helper.Content.Characters.RegisterCharacterAnimation("Neutral", new()
            {
                Deck = RandallDeck.Deck,
                LoopTag = "neutral",
                Frames = Enumerable.Range(0, 4)
                .Select(i => helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile($"assets/Character/neutral/{i}.png")).Sprite)
                .ToList()
            });
            helper.Content.Characters.RegisterCharacterAnimation("Squint", new()
            {
                Deck = RandallDeck.Deck,
                LoopTag = "squint",
                Frames = Enumerable.Range(0, 4)
                .Select(i => helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile($"assets/Character/squint/{i}.png")).Sprite)
                .ToList()
            });
            helper.Content.Characters.RegisterCharacterAnimation("GameOver", new()
            {
                Deck = RandallDeck.Deck,
                LoopTag = "gameover",
                Frames = [helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Character/gameover/0.png")).Sprite]
            });
            helper.Content.Characters.RegisterCharacterAnimation("Mini", new()
            {
                Deck = RandallDeck.Deck,
                LoopTag = "mini",
                Frames = [helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Character/mini/0.png")).Sprite]
            });

            //Define Statuses
            ChargeUpStatus = helper.Content.Statuses.RegisterStatus("ChargeUp", new()
            {
                Definition = new()
                {
                    icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Icons/IconChargeUp.png")).Sprite,
                    color = new("3e8ad5"),
                    isGood = true
                },
                Name = this.AnyLocalizations.Bind(["status", "ChargeUp", "name"]).Localize,
                Description = this.AnyLocalizations.Bind(["status", "ChargeUp", "description"]).Localize
            });
            HalfEvadeStatus = helper.Content.Statuses.RegisterStatus("HalfEvade", new()
            {
                Definition = new()
                {
                    icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Icons/IconHalfEvade.png")).Sprite,
                    color = new("3e8ad5"),
                    isGood = true
                },
                Name = this.AnyLocalizations.Bind(["status", "HalfEvade", "name"]).Localize,
                Description = this.AnyLocalizations.Bind(["status", "HalfEvade", "description"]).Localize
            });
            HalfShieldStatus = helper.Content.Statuses.RegisterStatus("HalfShield", new()
            {
                Definition = new()
                {
                    icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Icons/IconHalfShield.png")).Sprite,
                    color = new("3e8ad5"),
                    isGood = true
                },
                Name = this.AnyLocalizations.Bind(["status", "HalfShield", "name"]).Localize,
                Description = this.AnyLocalizations.Bind(["status", "HalfShield", "description"]).Localize
            });
            HalfTempShieldStatus = helper.Content.Statuses.RegisterStatus("HalfTempShield", new()
            {
                Definition = new()
                {
                    icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Icons/IconHalfTempShield.png")).Sprite,
                    color = new("3e8ad5"),
                    isGood = true
                },
                Name = this.AnyLocalizations.Bind(["status", "HalfTempShield", "name"]).Localize,
                Description = this.AnyLocalizations.Bind(["status", "HalfTempShield", "description"]).Localize
            });
            HalfDamageStatus = helper.Content.Statuses.RegisterStatus("HalfDamage", new()
            {
                Definition = new()
                {
                    icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Icons/IconHalfDamage.png")).Sprite,
                    color = new("3e8ad5"),
                    isGood = false
                },
                Name = this.AnyLocalizations.Bind(["status", "HalfDamage", "name"]).Localize,
                Description = this.AnyLocalizations.Bind(["status", "HalfDamage", "description"]).Localize
            });
            CoPilotStatus = helper.Content.Statuses.RegisterStatus("CoPilotStatus", new()
            {
                Definition = new()
                {
                    icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Icons/IconCoPilot1.png")).Sprite,
                    color = new("3e8ad5"),
                    isGood = false
                },
                Name = this.AnyLocalizations.Bind(["status", "CoPilotStatus", "name"]).Localize,
                Description = this.AnyLocalizations.Bind(["status", "CoPilotStatus", "description"]).Localize
            });
            AuxiliaryShieldsStatus = helper.Content.Statuses.RegisterStatus("AuxiliaryShieldsStatus", new()
            {
                Definition = new()
                {
                    icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Icons/IconAuxiliaryShields.png")).Sprite,
                    color = new("3e8ad5"),
                    isGood = false
                },
                Name = this.AnyLocalizations.Bind(["status", "AuxiliaryShieldsStatus", "name"]).Localize,
                Description = this.AnyLocalizations.Bind(["status", "AuxiliaryShieldsStatus", "description"]).Localize
            });

            //Register Artifacts
            SparePieces.Register(helper);
            EnhancedMaterials.Register(helper);
            PatchingProgram.Register(helper);

            //This has to be at the end, this applies all Harmony patches
            //This is an instance method, THIS instance is calling it, this. can be removed
            //passing the package info as a parameter
            this.ApplyHarmonyPatches(package);
        }

        private void ApplyHarmonyPatches(IPluginPackage<IModManifest> package)
        {
            //Setup Harmony, create a new Harmony named harmony, harmony harmony... harmony
            Harmony harmony = new Harmony (package.Manifest.UniqueName);

            harmony.Patch(
                //This refers to the code that is about to be intruded into.
                //This is how Harmony wants MethodInfo through AccessTools
                original: AccessTools.DeclaredMethod(typeof(Combat), nameof(Combat.Update)),
                //Prefix is no longer necessary
                //prefix: new HarmonyMethod(AccessTools.DeclaredMethod(typeof(ModInit), nameof(ModInit.Combat_Update_Prefix))),
                postfix: new HarmonyMethod(AccessTools.DeclaredMethod(typeof(ModInit), nameof(ModInit.Combat_Update_Postfix)))
            );

            harmony.Patch(
                //This refers to the code that is about to be intruded into.
                //This is how Harmony wants MethodInfo through AccessTools
                original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.OnBeginTurn)),
                postfix: new HarmonyMethod(AccessTools.DeclaredMethod(typeof(OnTurnBeingHandler), nameof(OnTurnBeingHandler.HarmonyPostfix_Ship_OnBeginTurn)))
            );
        }

        private static void Combat_Update_Prefix(G g, ref int __state) {
            //This does absolutely nothing but getting the game state
            __state = g.state.ship.Get(Instance.HalfEvadeStatus.Status);
        }
        /*
        private static void Combat_Update_Postfix(G g, ref int __state, Combat __instance)
        {
            var newAmount = g.state.ship.Get(Instance.HalfEvadeStatus.Status);
            var realEvade = newAmount / 2;
            if (realEvade == 0) return; //This exists this function
            var oldRealEvade = g.state.ship.Get(Status.evade);
            if (realEvade != oldRealEvade) {
                __instance.QueueImmediate(
                [new AStatus()
                {
                    targetPlayer = true,
                    status = ModInit.Instance.HalfEvadeStatus.Status,
                    statusAmount = -(oldRealEvade - __state)
                },
                new AStatus()
                {
                    targetPlayer = true,
                    status = Status.evade,
                    statusAmount = (oldRealEvade - __state) / 2
                }]);
            }
        }*/

        /*private static void Combat_Update_Postfix(G g, ref int __state, Combat __instance)
        {
            int halfEvade = g.state.ship.Get(Instance.HalfEvadeStatus.Status);
            if (halfEvade > 1) {
                int totalEvadeChange = halfEvade / 2;
                halfEvade = 0;
                __instance.QueueImmediate(
                [new AStatus()
                {
                    targetPlayer = true,
                    status = ModInit.Instance.HalfEvadeStatus.Status,
                    statusAmount = -totalEvadeChange * 2
                },
                new AStatus()
                {
                    targetPlayer = true,
                    status = Status.evade,
                    statusAmount = totalEvadeChange
                }]);
            }
        }*/

        private static void Combat_Update_Postfix(G g, Combat __instance)
        {
            Partial_Status_Handler(g, __instance, ModInit.Instance.HalfEvadeStatus.Status, Status.evade);
            Partial_Status_Handler(g, __instance, ModInit.Instance.HalfShieldStatus.Status, Status.shield);
            Partial_Status_Handler(g, __instance, ModInit.Instance.HalfTempShieldStatus.Status, Status.tempShield);
            Partial_Hurt_Handler(g, __instance);
            Partial_Enemy_Hurt_Handler(g, __instance);
            Partial_Attack_Handler(g, __instance);
        }

        private static void Partial_Status_Handler(G g, Combat __instance, Status partialStatus, Status fullStatus)
        {
            var toAdd = g.state.ship.Get(partialStatus) / 2;
            if (toAdd == 0)
                return;

            IEnumerable<CardAction> allActions = __instance.cardActions;
            if (__instance.currentCardAction is not null)
                //Append all card actions to the queue check, might not be needed
                allActions = allActions.Append(__instance.currentCardAction);

            foreach (var action in allActions)
                //This checks if any action in the action queue contains the status to be checked, if so we don't execute this code
                if (action is AStatus statusAction && statusAction.status == partialStatus && statusAction.statusAmount < 0)
                    return;

            __instance.QueueImmediate([
                new AStatus() { targetPlayer = true, status = fullStatus, statusAmount = toAdd },
                new AStatus() { targetPlayer = true, status = partialStatus, statusAmount = -toAdd * 2 },
            ]);
        }

        private static void Partial_Hurt_Handler(G g, Combat __instance)
        {
            var partialStatus = ModInit.Instance.HalfDamageStatus.Status;
            var toAdd = g.state.ship.Get(partialStatus) / 2;
            if (toAdd == 0)
                return;

            IEnumerable<CardAction> allActions = __instance.cardActions;
            if (__instance.currentCardAction is not null)
                //Append all card actions to the queue check, might not be needed
                allActions = allActions.Append(__instance.currentCardAction);

            foreach (var action in allActions)
                //This checks if any action in the action queue contains the status to be checked, if so we don't execute this code
                if (action is AStatus statusAction && statusAction.status == partialStatus && statusAction.statusAmount < 0)
                    return;

            __instance.QueueImmediate([
                new AHurt() { targetPlayer = true, hurtAmount = toAdd, hurtShieldsFirst = true },
                new AStatus() { targetPlayer = true, status = partialStatus, statusAmount = -toAdd * 2 },
            ]);
        }

        private static void Partial_Enemy_Hurt_Handler(G g, Combat __instance)
        {
            var partialStatus = ModInit.Instance.HalfDamageStatus.Status;
            var toAdd = __instance.otherShip.Get(partialStatus) / 2;
            if (toAdd == 0)
                return;

            IEnumerable<CardAction> allActions = __instance.cardActions;
            if (__instance.currentCardAction is not null)
                //Append all card actions to the queue check, might not be needed
                allActions = allActions.Append(__instance.currentCardAction);

            foreach (var action in allActions)
                //This checks if any action in the action queue contains the status to be checked, if so we don't execute this code
                if (action is AStatus statusAction && statusAction.status == partialStatus && statusAction.statusAmount < 0)
                    return;

            __instance.QueueImmediate([
                new AHurt() { targetPlayer = false, hurtAmount = toAdd, hurtShieldsFirst = true },
                new AStatus() { targetPlayer = false, status = partialStatus, statusAmount = -toAdd * 2 },
            ]);
        }

        private static void Partial_Attack_Handler(G g, Combat __instance)
        {
            var partialStatus = ModInit.Instance.ChargeUpStatus.Status;
            var toAdd = g.state.ship.Get(partialStatus) / 3;
            if (toAdd == 0)
                return;
            IEnumerable<CardAction> allActions = __instance.cardActions;
            if (__instance.currentCardAction is not null)
                //Append all card actions to the queue check, might not be needed
                allActions = allActions.Append(__instance.currentCardAction);
            foreach (var action in allActions)
                //This checks if any action in the action queue contains the status to be checked, if so we don't execute this code
                if (action is AStatus statusAction && statusAction.status == partialStatus && statusAction.statusAmount < 0)
                    return;

            __instance.QueueImmediate([
                new AAttack() { damage = toAdd },
                new AStatus() { targetPlayer = true, status = partialStatus, statusAmount = -toAdd * 3 },
            ]);
        }
    }
}
