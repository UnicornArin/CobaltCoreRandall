using Microsoft.Extensions.Logging;

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

        //Row 1

        DB.story.all[$"ArtifactNanofiberHull_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = new() { randall },
            hasArtifacts = new() { "NanofiberHull" },
            oncePerRunTags = new() { "NanofiberHull" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "A more aggressive artifact would have been better.",
                    loopTag = "misplay"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.peri.Key(),
                            Text = "Agreed.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "Heh, you're right for once.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };

        DB.story.all[$"ArtifactCrosslink_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = new() { randall },
            hasArtifacts = new() { "Crosslink" },
            oncePerRunTags = new() { "Crosslink" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Every ship should come with a Crosslink.",
                    loopTag = "neutral"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.peri.Key(),
                            Text = "...",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };

        DB.story.all[$"ArtifactOvercharger_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = new() { randall },
            hasArtifacts = new() { "Overcharger" },
            oncePerRunTags = new() { "Overcharger" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "We need to go all out every fourth turn.",
                    loopTag = "explain"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.peri.Key(),
                            Text = "Give us the signal.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "The fourth what?",
                            loopTag = "squint"
                        }
                    }
                }
            }
        };

        DB.story.all[$"ArtifactShieldMemory_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = new() { randall },
            hasArtifacts = new() { "ShieldMemory" },
            oncePerRunTags = new() { "ShieldMemory" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "I think I can make use of Shield Memory.",
                    loopTag = "thoughtful"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.dizzy.Key(),
                            Text = "Me too.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };

        DB.story.all[$"ArtifactJumperCables_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = new() { randall },
            hasArtifacts = new() { "JumperCables" },
            oncePerRunTags = new() { "JumperCables" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "I hope we really don't need to use these.",
                    loopTag = "facepalm"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.hacker.Key(),
                            Text = "It's not an elegant solution.",
                            loopTag = "squint"
                        }
                    }
                }
            }
        };

        DB.story.all[$"ArtifactHullPlating_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = new() { randall },
            hasArtifacts = new() { "HullPlating" },
            oncePerRunTags = new() { "HullPlating" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "You know, having more hull to work with is nice.",
                    loopTag = "neutral"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "Let's be more reckless.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = "comp",
                            Text = "It feels like you use our ship as a resource.",
                            loopTag = "squint"
                        }
                    }
                }
            }
        };
        
        DB.story.all[$"ArtifactArmoredBay_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = new() { randall },
            hasArtifacts = new() { "ArmoredBay" },
            oncePerRunTags = new() { "ArmoredBay" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "I have mixed feelings about an armored bay.",
                    loopTag = "thoughtful"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.goat.Key(),
                            Text = "It doesn't protect my drones.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.shard.Key(),
                            Text = "Geodes are better.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };

        //Row 2
        
        DB.story.all[$"ArtifactJetThrusters_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = new() { randall },
            hasArtifacts = new() { "JetThrusters" },
            oncePerRunTags = new() { "JetThrusters" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "I'll never complain about extra evades.",
                    loopTag = "explain"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.riggs.Key(),
                            Text = "Does that mean I get to take a break?",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };

        DB.story.all[$"ArtifactEnergyPrep_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
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

        DB.story.all[$"ArtifactEnergyRefund_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = new() { randall },
            hasArtifacts = new() { "EnergyRefund" },
            oncePerRunTags = new() { "EnergyRefund" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Alright, playing those big setup cards should be easier now.",
                    loopTag = "explain"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.riggs.Key(),
                            Text = "That sounds like a good thing.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };

        //Row 3

        DB.story.all[$"ArtifactPiercer_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = new() { randall },
            hasArtifacts = new() { "Piercer" },
            oncePerRunTags = new() { "Piercer" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "All we need is a strong attack every turn.",
                    loopTag = "explain"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "Heh, leave that to me.",
                            loopTag = "sly"
                        },
                        new CustomSay()
                        {
                            who = Deck.goat.Key(),
                            Text = "Not quite useful for me.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };
 
        DB.story.all[$"ArtifactHealBooster_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = new() { randall },
            hasArtifacts = new() { "HealBooster" },
            oncePerRunTags = new() { "HealBooster" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Let's not depend on Heal Booster too much if we can.",
                    loopTag = "neutral"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "No promises.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.dizzy.Key(),
                            Text = "Let's take no damage.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };
        
        DB.story.all[$"ArtifactJettisonHatch_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = new() { randall },
            hasArtifacts = new() { "JettisonHatch" },
            oncePerRunTags = new() { "JettisonHatch" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Are we early enough in the run to thin our deck?",
                    loopTag = "thoughtful"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = "comp",
                            Text = "What?",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.riggs.Key(),
                            Text = "Is that similar to memory loss?",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };

        //Row 4

        /*DB.story.all[$"ArtifactGenomeSplicing_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = new() { randall },
            hasArtifacts = new() { "TheJazMaster.TyAndSasha::GenomeSplicing" },
            oncePerRunTags = new() { "TheJazMaster.TyAndSasha::GenomeSplicing" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = randall,
                    loopTag = "misplay"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = "Shockah.Soggins.Deck.Soggins",
                            Text = "TestResponse.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "Heh, you're right for once.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };*/

        //Crossmod Artifacts

        /*DB.story.all[$"ArtifactAresCannon_{randall}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = new() { randall },
            hasArtifacts = new() { "AresCannon" },
            oncePerRunTags = new() { "AresCannon" },
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
        };*/
    }
}