using System;
using Microsoft.Extensions.Logging;
using RandallMod;

namespace RandallMod;

internal static class ArtifactDialogue
{
    private static ModInit Instance => ModInit.Instance;

    internal static void Inject()
    {
        Instance.Logger.LogInformation("Artifact Dialogue Inject got called at some point");

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
            oncePerRunTags = new() { "EnergyPrep" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Just one energy at the start? That's not much at all.",
                    loopTag = "misplay" 
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.hacker.Key(),
                            Text = "I like it, it kickstarts some processes.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };

        DB.story.all[$"ArtifactAresCannon_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            lookup = new() { "AresCannon" },
            allPresent = new() { randall },
            hasArtifacts = new() { "AresCannon" },
            maxTurnsThisCombat = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "This ship is kind of OP.",
                    loopTag = "explain"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.hacker.Key(),
                            Text = "It's good that we're riding it then.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = "comp",
                            Text = "It's like you've analyzed everything too much.",
                            loopTag = "squint"
                        },
                    }
                }
            }
        };

    }
}