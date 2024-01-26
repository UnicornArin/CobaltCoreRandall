using System;
using RandallMod;

namespace RandallMod;

internal static class ArtifactDialogue
{/*
    private static ModInit Instance => ModInit.Instance;

    internal static void Inject()
    {
        string randall = Instance.RandallDeck.Deck.Key();

        foreach (var artifactType in ModInit.AllArtifacts)
        {
            if (Activator.CreateInstance(artifactType) is not IRegisterableArtifact artifact)
                continue;
            artifact.InjectDialogue();
        }

        DB.story.all[$"ArtifactEnergyPrep_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            lookup = new() { "EnergyPrepTrigger" },
            allPresent = new() { randall },
            hasArtifacts = new() { "EnergyPrep" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "I just got zapped by static.",
                    DynamicLoopTag = Dialogue.CurrentSmugLoopTag
                }
            }
        };
        DB.story.all[$"ArtifactEnergyRefund_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            oncePerCombatTags = new() { "EnergyRefund" },
            allPresent = new() { randall },
            hasArtifacts = new() { "EnergyRefund" },
            minCostOfCardJustPlayed = 3,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "There's free energy laying around.",
                    DynamicLoopTag = Dialogue.CurrentSmugLoopTag
                }
            }
        };
        DB.story.all[$"ArtifactFractureDetection_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            oncePerCombatTags = new() { "FractureDetectionBarks" },
            allPresent = new() { randall },
            hasArtifacts = new() { "FractureDetection" },
            maxTurnsThisCombat = 1,
            turnStart = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Computer, find weak spot.",
                    DynamicLoopTag = Dialogue.CurrentSmugLoopTag
                }
            }
        };
        DB.story.all[$"ArtifactGeminiCoreBooster_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRunTags = new() { "GeminiCoreBooster" },
            allPresent = new() { randall },
            hasArtifacts = new() { "GeminiCoreBooster" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Don't worry! This is very simple.",
                    DynamicLoopTag = Dialogue.CurrentSmugLoopTag
                }
            }
        };
        DB.story.all[$"ArtifactGeminiCore_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRunTags = new() { "GeminiCore" },
            allPresent = new() { randall },
            hasArtifacts = new() { "GeminiCore" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "This ship is simple for a smart person like me.",
                    DynamicLoopTag = Dialogue.CurrentSmugLoopTag
                }
            }
        };
        DB.story.all[$"ArtifactJumperCables_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRunTags = new() { "ArtifactJumperCablesReady" },
            allPresent = new() { randall },
            hasArtifacts = new() { "JumperCables" },
            maxTurnsThisCombat = 1,
            maxHullPercent = 0.5,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "I feel really safe right now.",
                    DynamicLoopTag = Dialogue.CurrentSmugLoopTag
                }
            }
        };
        DB.story.all[$"ArtifactPowerDiversionMade{randall}AttackFail"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall, Deck.peri.Key() },
            hasArtifacts = new() { "PowerDiversion" },
            playerShotJustHit = true,
            maxDamageDealtToEnemyThisAction = 0,
            whoDidThat = (Deck)Instance.RandallDeck.Key(),
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "That's not very nice.",
                    loopTag = Instance.MadPortraitAnimation.Tag
                },
                new CustomSay()
                {
                    who = Deck.peri.Key(),
                    Text = "I'm keeping an eye on you.",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.all[$"ArtifactRecalibrator_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            hasArtifacts = new() { "ArtifactRecalibrator" },
            playerShotJustMissed = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "No misses, only happy accidents.",
                    DynamicLoopTag = Dialogue.CurrentSmugLoopTag
                }
            }
        };
        DB.story.all[$"ArtifactSimplicity_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            hasArtifacts = new() { "Simplicity" },
            oncePerRunTags = new() { "SimplicityShouts" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "No thoughts, head empty.",
                    DynamicLoopTag = Dialogue.CurrentSmugLoopTag
                }
            }
        };
        DB.story.all[$"ArtifactTridimensionalCockpit_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            hasArtifacts = new() { "TridimensionalCockpit" },
            turnStart = true,
            maxTurnsThisCombat = 1,
            oncePerCombatTags = new() { "TridimensionalCockpit" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "I don't understand where we are right now.",
                    loopTag = Instance.MadPortraitAnimation.Tag
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.hacker.Key(),
                            Text = "It's better that way.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };
    }*/
}