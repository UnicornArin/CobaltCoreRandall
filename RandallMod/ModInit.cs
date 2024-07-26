using HarmonyLib;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using Nickel;
using RandallMod.Artifacts;
using RandallMod.Jester;
using Shockah.Soggins;

namespace RandallMod
{
    public sealed class ModInit : SimpleMod, IStatusRenderHook
    {
        
        internal static ModInit Instance { get; private set; } = null!;

        internal Harmony Harmony { get; }
        internal IKokoroApi KokoroApi { get; }
        internal IMoreDifficultiesApi? MoreDifficultiesApi { get; }
        internal IJesterApi? JesterApi { get; }
        internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
        internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }
        internal ISpriteEntry PartialStatusIcon { get; }


        //Initialize Statuses
        internal IStatusEntry ChargeUpStatus { get; }
        internal IStatusEntry HalfEvadeStatus { get; }
        internal IStatusEntry HalfShieldStatus { get; }
        internal IStatusEntry HalfTempShieldStatus { get; }
        internal IStatusEntry HalfDamageStatus { get; }
        internal IStatusEntry CoPilotStatus { get; }
        internal IStatusEntry AuxiliaryShieldsStatus { get; }
        internal IStatusEntry OverchargeStatus { get; }
        internal IStatusEntry HalfCardStatus { get; }
        internal IStatusEntry ArchiveStatus { get; }
        //internal IStatusEntry DummyHalvesStatus { get; } OBSOLETE
        internal IShipEntry RandallShip { get; }

        //Initialize Trait
        internal ICardTraitEntry SynergizedTrait { get; private set; } = null!;
        internal ISpriteEntry SynergyChargeSprite { get; private set; } = null!;
        internal ISpriteEntry IconSynzergize { get; private set; } = null!;

        //Initialize Deck
        internal IDeckEntry RandallDeck { get; }
        internal IDeckEntry RandallShipDeck { get; }

        //ExeTypes
        private readonly HashSet<Type> ExeTypes = [];

        //Initialize Common Artifacts
        internal static IReadOnlyList<Type> CommonArtifacts { get; } = [
            typeof(SparePieces),
            typeof(EnhancedMaterials),
            typeof(PatchingProgram),
            typeof(Teapot),
            typeof(BonusSynergy)
        ];
        internal static IReadOnlyList<Type> BossArtifacts { get; } = [
            typeof(DivertedCharge),
            typeof(RepurposedParts),
            typeof(SynergyPower)
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
            typeof(Teamwork),
            typeof(Rondell),
            typeof(ShieldV1_5),
            typeof(InParts),
            typeof(Magnify)
        };
        internal static readonly Type[] UncommonCards = new Type[]
        {
            typeof(EvadeV1_5),
            typeof(CompleteSet),
            typeof(ParticleBeam),
            typeof(Overcharge),
            typeof(SlowBarrage),
            typeof(Cooperate),
            typeof(SynergyStrike)
        };
        internal static readonly Type[] RareCards = new Type[]
        {
            typeof(EmergencyProtocol),
            typeof(EnhancedMagnify),
            typeof(Archive),
            typeof(CoPilot),
            typeof(AuxiliaryShields)
        };

        internal static readonly Type[] ShipCards = new Type[]
        {
            typeof(DisposableShield),
            typeof(DisposableCannon),
            typeof(DisposableWinglets),
            typeof(DisposableShredder),
        };

        internal static readonly Type[] RandallExeCard = new Type[]
        {
            typeof(RandallExe),
        };

        internal static IEnumerable<Type> AllCards
            => StarterCards.Concat(CommonCards).Concat(UncommonCards).Concat(RareCards).Append(typeof(RandallExe));
        internal static IEnumerable<Type> AllArtifacts
            => CommonArtifacts.Concat(BossArtifacts);

        public ModInit(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger) 
        {
            //This is a constructor, logic goes here
            Instance = this;
            //This is the Kokoro API registry
            KokoroApi = helper.ModRegistry.GetApi<IKokoroApi>("Shockah.Kokoro")!;
            KokoroApi.RegisterStatusRenderHook(this, 0);
            //Alt Deck check
            MoreDifficultiesApi = helper.ModRegistry.GetApi<IMoreDifficultiesApi>("TheJazMaster.MoreDifficulties");
            //This is gonna do Jester BS
            JesterApi = helper.ModRegistry.GetApi<IJesterApi>("rft.Jester");

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
                Name = this.AnyLocalizations.Bind(["character", "name"]).Localize,
            });

            //Register cards:
            //Starters
            SynergyEvade.Register(package, helper);
            SynergyShield.Register(package, helper);
            //Commons
            AttackAndAHalf.Register(package, helper);
            MasterOfNone.Register(package, helper);
            Teamwork.Register(package, helper);
            ShieldV1_5.Register(package, helper);
            InParts.Register(package, helper);
            Rondell.Register(package, helper);
            Magnify.Register(package, helper);
            //Uncommons
            Cooperate.Register(package, helper);
            Overcharge.Register(package, helper);
            SlowBarrage.Register(package, helper);
            EvadeV1_5.Register(package, helper);
            SynergyStrike.Register(package, helper);
            CompleteSet.Register(package, helper);
            ParticleBeam.Register(package, helper);
            //Rares
            EmergencyProtocol.Register(package, helper);
            EnhancedMagnify.Register(package, helper);
            CoPilot.Register(package, helper);
            AuxiliaryShields.Register(package, helper);
            Archive.Register(package, helper);
<<<<<<< HEAD
            //Exe
=======
            //EXE
>>>>>>> Likely-release
            RandallExe.Register(package, helper);

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
            helper.Content.Characters.RegisterCharacterAnimation("Accusatory", new()
            {
                Deck = RandallDeck.Deck,
                LoopTag = "accusatory",
                Frames = Enumerable.Range(0, 4)
                .Select(i => helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile($"assets/Character/accusatory/{i}.png")).Sprite)
                .ToList()
            });
            helper.Content.Characters.RegisterCharacterAnimation("Explain", new()
            {
                Deck = RandallDeck.Deck,
                LoopTag = "explain",
                Frames = Enumerable.Range(0, 4)
                .Select(i => helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile($"assets/Character/explain/{i}.png")).Sprite)
                .ToList()
            });
            helper.Content.Characters.RegisterCharacterAnimation("Facepalm", new()
            {
                Deck = RandallDeck.Deck,
                LoopTag = "facepalm",
                Frames = Enumerable.Range(0, 4)
                .Select(i => helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile($"assets/Character/facepalm/{i}.png")).Sprite)
                .ToList()
            });
            helper.Content.Characters.RegisterCharacterAnimation("Glee", new()
            {
                Deck = RandallDeck.Deck,
                LoopTag = "glee",
                Frames = Enumerable.Range(0, 4)
                .Select(i => helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile($"assets/Character/glee/{i}.png")).Sprite)
                .ToList()
            });
            helper.Content.Characters.RegisterCharacterAnimation("Misplay", new()
            {
                Deck = RandallDeck.Deck,
                LoopTag = "misplay",
                Frames = Enumerable.Range(0, 4)
                .Select(i => helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile($"assets/Character/misplay/{i}.png")).Sprite)
                .ToList()
            });
            helper.Content.Characters.RegisterCharacterAnimation("Smug", new()
            {
                Deck = RandallDeck.Deck,
                LoopTag = "smug",
                Frames = Enumerable.Range(0, 4)
                .Select(i => helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile($"assets/Character/smug/{i}.png")).Sprite)
                .ToList()
            });
            helper.Content.Characters.RegisterCharacterAnimation("Thoughtful", new()
            {
                Deck = RandallDeck.Deck,
                LoopTag = "thoughtful",
                Frames = Enumerable.Range(0, 4)
                .Select(i => helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile($"assets/Character/thoughtful/{i}.png")).Sprite)
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

            helper.Content.Characters.RegisterCharacter("Randall", new()
            {
                Deck = RandallDeck.Deck,
                Description = this.AnyLocalizations.Bind(["character", "description"]).Localize,
                BorderSprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Character/char_randall.png")).Sprite,
                Starters = new StarterDeck{ cards = new List<Card> {
                    new SynergyEvade(),
                    new SynergyShield()
                    }
                },
                ExeCardType = typeof(RandallExe)
            });

            MoreDifficultiesApi?.RegisterAltStarters(RandallDeck.Deck, new StarterDeck
            {
                cards = {
                new Teamwork(),
                new ShieldV1_5()
            }
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
            HalfCardStatus = helper.Content.Statuses.RegisterStatus("HalfCardStatus", new()
            {
                Definition = new()
                {
                    icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Icons/IconHalfCard.png")).Sprite,
                    color = new("3e8ad5"),
                    isGood = true
                },
                Name = this.AnyLocalizations.Bind(["status", "HalfCard", "name"]).Localize,
                Description = this.AnyLocalizations.Bind(["status", "HalfCard", "description"]).Localize
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
                    isGood = true
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
                    isGood = true
                },
                Name = this.AnyLocalizations.Bind(["status", "AuxiliaryShieldsStatus", "name"]).Localize,
                Description = this.AnyLocalizations.Bind(["status", "AuxiliaryShieldsStatus", "description"]).Localize
            });
            ArchiveStatus = helper.Content.Statuses.RegisterStatus("ArchiveStatus", new()
            {
                Definition = new()
                {
                    icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Icons/IconArchives.png")).Sprite,
                    color = new("3e8ad5"),
                    isGood = true
                },
                Name = this.AnyLocalizations.Bind(["status", "ArchiveStatus", "name"]).Localize,
                Description = this.AnyLocalizations.Bind(["status", "ArchiveStatus", "description"]).Localize
            });
            OverchargeStatus = helper.Content.Statuses.RegisterStatus("OverchargeStatus", new()
            {
                Definition = new()
                {
                    icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Icons/IconOvercharge.png")).Sprite,
                    color = new("3e8ad5"),
                    isGood = true
                },
                Name = this.AnyLocalizations.Bind(["status", "OverchargeStatus", "name"]).Localize,
                Description = this.AnyLocalizations.Bind(["status", "OverchargeStatus", "description"]).Localize

            });
            
        
            PartialStatusIcon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Icons/IconDummyHalves.png"));
            /* OBSOLETE
            DummyHalvesStatus = helper.Content.Statuses.RegisterStatus("DummyHalvesStatus", new()
            {
                Definition = new()
                {
                    icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Icons/IconDummyHalves.png")).Sprite,
                    color = new("3e8ad5"),
                    isGood = true
                },
                Name = this.AnyLocalizations.Bind(["status", "DummyHalvesStatus", "name"]).Localize,
                Description = this.AnyLocalizations.Bind(["status", "DummyHalvesStatus", "description"]).Localize

            });*/

            //Register Ship
            RandallShipDeck = helper.Content.Decks.RegisterDeck("RandallShip", new()
            {
                Definition = new() { color = DB.decks[Deck.dracula].color, titleColor = DB.decks[Deck.dracula].titleColor },
                DefaultCardArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Ship/border_ship.png")).Sprite,
                BorderSprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Ship/art_blank.png")).Sprite,
                Name = this.AnyLocalizations.Bind(["ship", "name"]).Localize
            });

            var shipWingLeft = helper.Content.Ships.RegisterPart("RandallShip.WingLeft", new()
            {
                Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Ship/RandallShipLeft.png")).Sprite
            });
            var shipWingRight = helper.Content.Ships.RegisterPart("RandallShip.WingRight", new()
            {
                Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Ship/RandallShipRight.png")).Sprite
            });
            var shipCockpit = helper.Content.Ships.RegisterPart("RandallShip.Cockpit", new()
            {
                Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Ship/RandallShipCockpit.png")).Sprite
            });
            var shipCannon = helper.Content.Ships.RegisterPart("RandallShip.Cannon", new()
            {
                Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Ship/RandallShipCannon.png")).Sprite
            });
            var shipBay = helper.Content.Ships.RegisterPart("RandallShip.Bay", new()
            {
                Sprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Ship/RandallShipBay.png")).Sprite
            });

            RandallShip = helper.Content.Ships.RegisterShip("RandallShip", new()
            {
                Ship = new()
                {
                    ship = new Ship
                    {
                        hull = 26,
                        hullMax = 26,
                        shieldMaxBase = 4,
                        parts =
                    {
                        new Part
                        {
                            type = PType.wing,
                            skin = shipWingLeft.UniqueName,
                        },
                        new Part
                        {
                            type = PType.cockpit,
                            skin = shipCockpit.UniqueName
                        },
                        new Part
                        {
                            type = PType.cannon,
                            skin = shipCannon.UniqueName
                        },
                        new Part
                        {
                            type = PType.missiles,
                            skin = shipBay.UniqueName
                        },
                        new Part
                        {
                            type = PType.cockpit,
                            skin = shipCockpit.UniqueName
                        },
                        new Part
                        {
                            type = PType.wing,
                            skin = shipWingRight.UniqueName,
                        }
                    }
                    },
                    artifacts =
                {
                    new ShieldPrep()
                },
                    cards =
                {
                    new DisposableCannon(),
                    new DisposableCannon(),
                    new DisposableShield(),
                    new DisposableWinglets(),
                    new DisposableShredder()
                }
                },
                UnderChassisSprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Ship/RandallShipChassis.png")).Sprite,
                /*ExclusiveArtifactTypes = new HashSet<Type>()
            {
                typeof(BatmobileArtifact),
            },*/
                Name = this.AnyLocalizations.Bind(["ship", "name"]).Localize,
                Description = this.AnyLocalizations.Bind(["ship", "description"]).Localize,
            });

            //Shipcards
            DisposableShield.Register(package, helper);
            DisposableCannon.Register(package, helper);
            DisposableWinglets.Register(package, helper);
            DisposableShredder.Register(package, helper);

            //Register Artifacts
            SparePieces.Register(helper);
            EnhancedMaterials.Register(helper);
            PatchingProgram.Register(helper);
            Teapot.Register(helper);
            BonusSynergy.Register(helper);

            DivertedCharge.Register(helper);
            RepurposedParts.Register(helper);
            SynergyPower.Register(helper);

            //Register additional sprites
            SynergyChargeSprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Icons/IconChargeUp2.png"));
            IconSynzergize = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Icons/IconSynergize.png"));

            //Register trait
            SynergizedTrait = helper.Content.Cards.RegisterTrait("Synergized", new()
            {
                Icon = (_, _) => SynergyChargeSprite.Sprite,
                Name = AnyLocalizations.Bind(["trait", "Synergized", "name"]).Localize,
                Tooltips = (_, _) => [
                    new GlossaryTooltip($"cardtrait.{Package.Manifest.UniqueName}::Synergized")
                    {
                        Icon = SynergyChargeSprite.Sprite,
                        TitleColor = Colors.cardtrait,
                        Title = Localizations.Localize(["trait", "Synergized", "name"]),
                        Description = Localizations.Localize(["trait", "Synergized", "description"])
                    }
                ]
            });

            helper.Events.OnModLoadPhaseFinished += (_, phase) =>
            {
                if (phase != ModLoadPhase.AfterDbInit)
                    return;

                var draculaApi = helper.ModRegistry.GetApi<ExternalAPIDracula>("Shockah.Dracula");
                if (draculaApi is null)
                    return;

                draculaApi.RegisterBloodTapOptionProvider(CoPilotStatus.Status, (_, _, status) => [
                    new AHurt { targetPlayer = true, hurtAmount = 2 },
                    new AStatus { targetPlayer = true, status = status, statusAmount = 1 },
                    new AStatus { targetPlayer = true, status = HalfEvadeStatus.Status, statusAmount = 1 },
                ]);
                draculaApi.RegisterBloodTapOptionProvider(AuxiliaryShieldsStatus.Status, (_, _, status) => [
                    new AHurt { targetPlayer = true, hurtAmount = 2 },
                    new AStatus { targetPlayer = true, status = status, statusAmount = 1 },
                    new AStatus { targetPlayer = true, status = HalfShieldStatus.Status, statusAmount = 1 },
                ]);
                draculaApi.RegisterBloodTapOptionProvider(ArchiveStatus.Status, (_, _, status) => [
                    new AHurt { targetPlayer = true, hurtAmount = 2 },
                    new AStatus { targetPlayer = true, status = status, statusAmount = 1 },
                    new AStatus { targetPlayer = true, status = HalfCardStatus.Status, statusAmount = 1 },
                ]);
                draculaApi.RegisterBloodTapOptionProvider(OverchargeStatus.Status, (_, _, status) => [
                    new AHurt { targetPlayer = true, hurtAmount = 1 },
                    new AStatus { targetPlayer = true, status = status, statusAmount = 1 },
                    new ASynergize { count = 6 },
                ]);
            };

            //Actual Jester BS here
            JesterApi?.RegisterProvider(new RandallJesterProvider());

            //Dialogue patching
            Dialogue.Inject();

            //This applies all Harmony patches
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
                //Artifact Patch for Artifact
                original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.OnBeginTurn)),
                postfix: new HarmonyMethod(AccessTools.DeclaredMethod(typeof(OnTurnBeginHandler), nameof(OnTurnBeginHandler.HarmonyPostfix_Ship_OnBeginTurn)))
            );

            harmony.Patch(
                original: AccessTools.DeclaredMethod(typeof(Card), nameof(Card.GetActionsOverridden)),
                postfix: new HarmonyMethod(AccessTools.DeclaredMethod(typeof(TraitManager), nameof(TraitManager.HarmonyPostfix_Card_GetActionsOverridden)))
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
            Partial_Card_Handler(g, __instance);
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
                new AStatus() { timer = 0, targetPlayer = true, status = fullStatus, statusAmount = toAdd },
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
                new AHurt() { timer = 0, targetPlayer = true, hurtAmount = toAdd, hurtShieldsFirst = true },
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
                new AHurt() { timer = 0, targetPlayer = false, hurtAmount = toAdd, hurtShieldsFirst = true },
                new AStatus() { targetPlayer = false, status = partialStatus, statusAmount = -toAdd * 2 },
            ]);
        }

        private static void Partial_Attack_Handler(G g, Combat __instance)
        {
            var partialStatus = ModInit.Instance.ChargeUpStatus.Status;
            var toAdd = g.state.ship.Get(partialStatus) / (3 + g.state.ship.Get(ModInit.Instance.OverchargeStatus.Status));
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
                new AAttack() { timer = 0, damage = Card.GetActualDamage(g.state, toAdd + g.state.ship.Get(ModInit.Instance.OverchargeStatus.Status)) },
                new AStatus() { targetPlayer = true, status = partialStatus, statusAmount = (toAdd + g.state.ship.Get(ModInit.Instance.OverchargeStatus.Status)) * -3 },
            ]);
        }

        private static void Partial_Card_Handler (G g, Combat __instance)
        {
            var partialStatus = ModInit.Instance.HalfCardStatus.Status;
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
                new ADrawCard() { timer = 0, count = toAdd},
                new AStatus() { targetPlayer = true, status = partialStatus, statusAmount = toAdd * -2 },
            ]);
        }

        //This supposedly changes the tooltip?
        public List<Tooltip> OverrideStatusTooltips(Status status, int amount, Ship? ship, List<Tooltip> tooltips)
        {
            var overchargeValue = ship is null ? 0 : ship.Get(Instance.OverchargeStatus.Status);
            for (int i = 0; i < tooltips.Count; i++)
            {
                var tooltip = tooltips[i];
                if (tooltip is TTGlossary glossary && glossary.key == $"status.{ModInit.Instance.ChargeUpStatus.Status.Key()}")
                    glossary.vals = new object[] { $"<c=boldPink>{overchargeValue + 3}</c>", $"<c=boldPink>{overchargeValue + 1}</c>" };
            }
            return tooltips;
        }

        public bool? ShouldOverrideStatusRenderingAsBars(State state, Combat combat, Ship ship, Status status, int amount)
            => status == Instance.ChargeUpStatus.Status ? true : null;

        public (IReadOnlyList<Color> Colors, int? BarTickWidth) OverrideStatusRendering(State state, Combat combat, Ship ship, Status status, int amount)
        {
            if (status != Instance.ChargeUpStatus.Status)
                return new();

            var max = ship.Get(Instance.OverchargeStatus.Status) + 3;

            var colors = Enumerable.Range(0, max)
                .Select(i => amount > i ? KokoroApi.DefaultActiveStatusBarColor : KokoroApi.DefaultInactiveStatusBarColor)
                .ToList();
            return (colors, null);
        }

        private void UpdateExeTypesIfNeeded()
        {
            if (ExeTypes.Count != 0)
                return;

            foreach (var deck in NewRunOptions.allChars)
                if (Helper.Content.Characters.LookupByDeck(deck) is { } character)
                    if (character.Configuration.ExeCardType is { } exeCardType)
                        ExeTypes.Add(exeCardType);
        }
        /*
        internal IEnumerable<Type> GetExeCardTypes()
        {
            if (EssentialsApi is { } essentialsApi)
            {
                foreach (var deck in NewRunOptions.allChars)
                    if (essentialsApi.GetExeCardTypeForDeck(deck) is { } exeCardType)
                        yield return exeCardType;
            }
            else
            {
                UpdateExeTypesIfNeeded();
                foreach (var exeCardType in ExeTypes)
                    yield return exeCardType;
            }
        }

        internal bool IsExeCardType(Type cardType)
        {
            if (EssentialsApi is { } essentialsApi)
                return essentialsApi.IsExeCardType(cardType);

            UpdateExeTypesIfNeeded();
            return ExeTypes.Contains(cardType);
        }*/
    }
}
