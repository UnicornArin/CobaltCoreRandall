namespace RandallMod;

internal static class CombatDialogue
{
    private static ModInit Instance => ModInit.Instance;

    internal static void Inject()
    {
        string randall = ModInit.Instance.RandallDeck.Deck.Key();

        DB.story.all[$"BlockedALotOfAttacksWithArmor_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            oncePerRun = true,
            oncePerCombatTags = new() { "YowzaThatWasALOTofArmorBlock" },
            enemyShotJustHit = true,
            minDamageBlockedByPlayerArmorThisTurn = 3,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Good use of armored parts.",
                    loopTag = "explain"
                }
            }
        };
        DB.story.all[$"DizzyWentMissing_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            oncePerCombatTags = new() { "dizzyWentMissing" },
            lastTurnPlayerStatuses = new() { Status.missingDizzy },
            priority = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "I'll try to cover for him.",
                    loopTag = "squint"
                }
            }
        };
        DB.story.all[$"RiggsWentMissing_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            oncePerCombatTags = new() { "riggsWentMissing" },
            lastTurnPlayerStatuses = new() { Status.missingRiggs },
            priority = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "I can cover for her.",
                    loopTag = "squint"
                }
            }
        };
        DB.story.all[$"PeriWentMissing_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            oncePerCombatTags = new() { "periWentMissing" },
            lastTurnPlayerStatuses = new() { Status.missingPeri },
            priority = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "I'll deal some damage while she comes back.",
                    loopTag = "squint"
                }
            }
        };
        DB.story.all[$"IsaacWentMissing_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            oncePerCombatTags = new() { "isaacWentMissing" },
            lastTurnPlayerStatuses = new() { Status.missingIsaac },
            priority = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "I definitely can't cover for him.",
                    loopTag = "misplay"
                }
            }
        };
        DB.story.all[$"DrakeWentMissing_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            oncePerCombatTags = new() { "drakeWentMissing" },
            lastTurnPlayerStatuses = new() { Status.missingDrake },
            priority = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "That's okay, less heat shenanigans.",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.all[$"MaxWentMissing_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            oncePerCombatTags = new() { "maxWentMissing" },
            lastTurnPlayerStatuses = new() { Status.missingMax },
            priority = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "He's utility, we can keep going for a bit longer.",
                    loopTag = "thoughtful"
                }
            }
        };
        DB.story.all[$"BooksWentMissing_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            oncePerCombatTags = new() { "booksWentMissing" },
            lastTurnPlayerStatuses = new() { Status.missingBooks },
            priority = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "She should be able to cheat herself back.",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.all[$"CatWentMissing_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            oncePerCombatTags = new() { "CatWentMissing" },
            lastTurnPlayerStatuses = new() { Status.missingCat },
            priority = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Welp, there she goes.",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.GetNode("CrabFacts1_Multi_0")?.lines.OfType<SaySwitch>().LastOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = randall,
            Text = "Ugh.",
            loopTag = "facepalm"
        });
        DB.story.GetNode("CrabFacts2_Multi_0")?.lines.OfType<SaySwitch>().LastOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = randall,
            Text = "Why are you like this?",
            loopTag = "facepalm"
        });
        DB.story.GetNode("CrabFactsAreOverNow_Multi_0")?.lines.OfType<SaySwitch>().LastOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = randall,
            Text = "You made all of that up, didn't you?",
            loopTag = "accusatory"
        });
        DB.story.all[$"{randall}JustHit_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            whoDidThat = (Deck)Instance.RandallDeck.Deck,
            playerShotJustHit = true,
            minDamageDealtToEnemyThisAction = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "That was pretty alright.",
                    loopTag = "neutral"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.dizzy.Key(),
                            Text = "Good enough!",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.hacker.Key(),
                            Text = "Eeey!",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };
        DB.story.all[$"{randall}JustHit_1"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            whoDidThat = (Deck)Instance.RandallDeck.Deck,
            playerShotJustHit = true,
            minDamageDealtToEnemyThisAction = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "That could have been better.",
                    loopTag = "thoughtful"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "It could have been better.",
                            loopTag = "sly"
                        },
                        new CustomSay()
                        {
                            who = Deck.riggs.Key(),
                            Text = "I'm fine with okay.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };
        DB.story.all[$"{randall}JustHit_2"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            whoDidThat = (Deck)Instance.RandallDeck.Deck,
            playerShotJustHit = true,
            minDamageDealtToEnemyThisAction = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Any damage is good damage.",
                    loopTag = "neutral"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = "comp",
                            Text = "As long as we're not the ones taking it.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.goat.Key(),
                            Text = "We're making progress.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };
        DB.story.all[$"{randall}JustHit_3"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            whoDidThat = (Deck)Instance.RandallDeck.Deck,
            playerShotJustHit = true,
            minDamageDealtToEnemyThisAction = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Got em.",
                    loopTag = "neutral"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.shard.Key(),
                            Text = "Yay!",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.peri.Key(),
                            Text = "Let's keep at it.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };
        DB.story.all[$"JustHitGeneric_{randall}_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisAction = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Nice.",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.all[$"JustHitGeneric_{randall}_1"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisAction = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "We hit them.",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.all[$"JustHitGeneric_{randall}_2"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisAction = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "That's a hit.",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.all[$"JustHitGeneric_{randall}_3"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisAction = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "That's some damage.",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.all[$"JustHitGeneric_{randall}_4"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisAction = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Hit confirmed.",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.all[$"Duo_AboutToDieAndLoop_{randall}0"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            maxHull = 2,
            oncePerCombatTags = new() { "aboutToDie" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Hmmm, time for another run.",
                    loopTag = "squint"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.dizzy.Key(),
                            Text = "Another what?",
                            loopTag = "squint"
                        },
                        new CustomSay()
                        {
                            who = Deck.riggs.Key(),
                            Text = "Huh?",
                            loopTag = "squint"
                        },
                        new CustomSay()
                        {
                            who = Deck.peri.Key(),
                            Text = "...",
                            loopTag = "squint"
                        },
                        new CustomSay()
                        {
                            who = Deck.hacker.Key(),
                            Text = "You're an odd one.",
                            loopTag = "squint"
                        },
                        new CustomSay()
                        {
                            who = Deck.shard.Key(),
                            Text = "You talk funny, mister.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = "comp",
                            Text = "What do you mean by 'run'?",
                            loopTag = "squint"
                        }
                    }
                }
            }
        };
        DB.story.all[$"Duo_AboutToDieAndLoop_{randall}1"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall, "comp" },
            maxHull = 2,
            oncePerCombatTags = new() { "aboutToDie" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = "comp",
                    Text = "Can't you mod something to help here?",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = randall,
                    Text = "That's cheating though.",
                    loopTag = "squint"
                }
            }
        };
        DB.story.all[$"Duo_AboutToDieAndLoop_{randall}2"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall, Deck.dizzy.Key() },
            maxHull = 2,
            oncePerCombatTags = new() { "aboutToDie" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "This isn't looking pretty.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = randall,
                    Text = "What do you mean I don't look pretty.",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.all[$"Duo_AboutToDieAndLoop_{randall}3"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall, Deck.riggs.Key() },
            maxHull = 2,
            oncePerCombatTags = new() { "aboutToDie" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = Deck.riggs.Key(),
                    Text = "We have to do something!",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = randall,
                    Text = "I'll press some buttons!",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.all[$"Duo_AboutToDieAndLoop_{randall}4"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall, Deck.peri.Key() },
            maxHull = 2,
            oncePerCombatTags = new() { "aboutToDie" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = Deck.peri.Key(),
                    Text = "This is getting pretty dire.",
                    loopTag = "mad"
                },
                new CustomSay()
                {
                    who = randall,
                    Text = "Don't worry, I'm here with you all.",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.all[$"Duo_AboutToDieAndLoop_{randall}5"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall, Deck.goat.Key() },
            maxHull = 2,
            oncePerCombatTags = new() { "aboutToDie" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = Deck.goat.Key(),
                    Text = "Is there a way out of this mess?",
                    loopTag = "panic"
                },
                new CustomSay()
                {
                    who = randall,
                    Text = "Trust me, it's fine.",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.all[$"Duo_AboutToDieAndLoop_{randall}6"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall, Deck.eunice.Key() },
            maxHull = 2,
            oncePerCombatTags = new() { "aboutToDie" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = Deck.eunice.Key(),
                    Text = "I'm blaming you.",
                    loopTag = "mad"
                },
                new CustomSay()
                {
                    who = randall,
                    Text = "I haven't done anything wrong!",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.all[$"Duo_AboutToDieAndLoop_{randall}7"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall, Deck.hacker.Key() },
            maxHull = 2,
            oncePerCombatTags = new() { "aboutToDie" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = Deck.hacker.Key(),
                    Text = "I'm going to die and my computer is full of viruses.",
                    loopTag = "mad"
                },
                new CustomSay()
                {
                    who = randall,
                    Text = "You still have games on your phone.",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.all[$"Duo_AboutToDieAndLoop_{randall}8"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall, Deck.shard.Key() },
            maxHull = 2,
            oncePerCombatTags = new() { "aboutToDie" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = Deck.shard.Key(),
                    Text = "Use your magic powers to save us!",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = randall,
                    Text = "Yes, magic powers.",
                    loopTag = "neutral"
                }
            }
        };//End of about to die
        DB.story.all[$"EmptyHandWithEnergy_{randall}_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            handEmpty = true,
            minEnergy = 1,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.riggs.Key(),
                            Text = "We're done here.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.goat.Key(),
                            Text = "Huh? It's over.",
                            loopTag = "neutral"
                        }
                    }
                },
                new CustomSay()
                {
                    who = randall,
                    Text = "And we're left with energy, what a waste.",
                    loopTag = "facepalm"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "Leave it to the nerd to miscalculate.",
                            loopTag = "squint"
                        },
                        new CustomSay()
                        {
                            who = Deck.peri.Key(),
                            Text = "Don't be too harsh on yourself.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };
        DB.story.all[$"EmptyHandWithEnergy_{randall}_1"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            handEmpty = true,
            minEnergy = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Left with energy, that was a misplay.",
                    loopTag = "misplay"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = "comp",
                            Text = "Miswhat?",
                            loopTag = "squint"
                        },
                        new CustomSay()
                        {
                            who = Deck.shard.Key(),
                            Text = "Teach me your magic words!",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };
        DB.story.all[$"EnemyArmorHitLots_{randall}_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            playerShotJustHit = true,
            minDamageBlockedByEnemyArmorThisTurn = 3,
            oncePerCombat = true,
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Some of that damage better went through.",
                    loopTag = "accusatory"
                }
            }
        };
        DB.story.all[$"EnemyArmorHitLots_{randall}_1"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            playerShotJustHit = true,
            minDamageBlockedByEnemyArmorThisTurn = 3,
            oncePerCombat = true,
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "If we're hitting armor, it better be for a reason.",
                    loopTag = "accusatory"
                }
            }
        };
        DB.story.all[$"ExpensiveCardPlayed_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            minCostOfCardJustPlayed = 4,
            oncePerCombatTags = new() { "ExpensiveCardPlayed" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "That was some big utility card, right?.",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.all[$"HandOnlyHasTrashCards_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            handFullOfTrash = true,
            oncePerCombatTags = new() { "handOnlyHasTrashCards" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Just trash? Ugh.",
                    loopTag = "squint"
                }
            }
        };
        DB.story.all[$"HandOnlyHasUnplayableCards_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            handFullOfUnplayableCards = true,
            oncePerCombatTags = new() { "handFullOfUnplayableCards" },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Now what?",
                    loopTag = "facepalm"
                }
            }
        };
        DB.story.all[$"WeDontOverlapWithEnemyAtAllButWeDoHaveASeekerToDealWith_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            shipsDontOverlapAtAll = true,
            oncePerCombatTags = new() { "NoOverlapBetweenShipsSeeker" },
            anyDronesHostile = new() { "missile_seeker" },
            nonePresent = new() { "crab" },
            priority = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "A seeker, huh?",
                    loopTag = "squint"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.riggs.Key(),
                            Text = "We can't run away from it.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.dizzy.Key(),
                            Text = "Let's shield against it.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };
        DB.story.all[$"ManyTurns_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            minTurnsThisCombat = 9,
            oncePerCombatTags = new() { "manyTurns" },
            oncePerRun = true,
            turnStart = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "That's fine, I'm built for longer battles anyway.",
                    loopTag = "explain"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "I don't have that much patience.",
                            loopTag = "squint"
                        },
                        new CustomSay()
                        {
                            who = Deck.goat.Key(),
                            Text = "Slow and steady.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };
        DB.story.all[$"OverheatCatFix_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall, "comp" },
            wasGoingToOverheatButStopped = true,
            whoDidThat = Deck.colorless,
            oncePerCombatTags = new() { "OverheatCatFix" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Whew, we can stop melting now.",
                    loopTag = "neutral"
                },
                 new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = "comp",
                            Text = "You're welcome.",
                            loopTag = "neutral"
                        }
                    }
                }
            }
        };
        DB.story.all[$"OverheatDrakeFix_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall, Deck.eunice.Key() },
            wasGoingToOverheatButStopped = true,
            whoDidThat = Deck.eunice,
            oncePerCombatTags = new() { "OverheatDrakeFix" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Whew, at last.",
                    loopTag = "neutral"
                },
                 new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "Crybaby.",
                            loopTag = "squint"
                        }
                    }
                }
            }
        };
        DB.story.all[$"OverheatGeneric_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            goingToOverheat = true,
            oncePerCombatTags = new() { "OverheatGeneric" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "I don't like those heat readings.",
                    loopTag = "squint"
                }
            }
        };
        DB.story.all[$"ThatsALotOfDamageToThem_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisTurn = 10,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Heh.",
                    loopTag = "smug"
                }
            }
        };
        DB.story.all[$"ThatsALotOfDamageToUs_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            enemyShotJustHit = true,
            minDamageDealtToPlayerThisTurn = 3,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "That's really not good.",
                    loopTag = "squint"
                }
            }
        };
        DB.story.all[$"TookZeroDamageAtLowHealth_{randall}_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            enemyShotJustHit = true,
            maxDamageDealtToPlayerThisTurn = 0,
            maxHull = 2,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Living on the edge.",
                    loopTag = "neutral"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.peri.Key(),
                            Text = "Let's look for repairs soon.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.goat.Key(),
                            Text = "I'd like to get off the edge.",
                            loopTag = "panic"
                        }
                    }
                }
            }
        };
        DB.story.all[$"TookZeroDamageAtLowHealth_{randall}_1"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            oncePerCombat = true,
            oncePerCombatTags = new() { $"TookZeroDamageAtLowHealth_{randall}_1_Tag" },
            enemyShotJustHit = true,
            maxDamageDealtToPlayerThisTurn = 0,
            maxHull = 2,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.hacker.Key(),
                            Text = "Let's go to a repair yard soon.",
                            loopTag = "squint"
                        },
                        new CustomSay()
                        {
                            who = Deck.goat.Key(),
                            Text = "I'd like to visit Cleo after this.",
                            loopTag = "squint"
                        }
                    }
                },
                new CustomSay()
                {
                    who = randall,
                    Text = "No, we're not visiting Cleo for a repair.",
                    loopTag = "misplay"
                }
            }
        };
        DB.story.all[$"WeAreCorroded_{randall}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            lastTurnPlayerStatuses = new() { Status.corrode },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "I'd like to get out of this, unmelted if we can.",
                    loopTag = "squint"
                }
            }
        };
        DB.story.all[$"WeMissedOopsie_{randall}_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            playerShotJustMissed = true,
            oncePerCombat = true,
            doesNotHaveArtifacts = new() { "Recalibrator", "GrazerBeam" },
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Do we have a good reason to miss?",
                    loopTag = "misplay"
                },
            }
        };
        DB.story.all[$"WeMissedOopsie_{randall}_1"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            playerShotJustMissed = true,
            oncePerCombat = true,
            doesNotHaveArtifacts = new() { "Recalibrator", "GrazerBeam" },
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.peri.Key(),
                            Text = "We need to reposition.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "Just hit them already!",
                            loopTag = "mad"
                        }
                    }
                },
                new CustomSay()
                {
                    who = randall,
                    Text = "I'll try to get us a better shot.",
                    loopTag = "thoughtful"
                }
            }
        };
        DB.story.all[$"WeGotHurtButNotTooBad_{randall}_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            enemyShotJustHit = true,
            minDamageDealtToPlayerThisTurn = 1,
            maxDamageDealtToPlayerThisTurn = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "I'd rather not waste hull points.",
                    loopTag = "accusatory"
                },
                new SaySwitch()
                {
                    lines = new (){
                        new CustomSay()
                        {
                            who = Deck.peri.Key(),
                            Text = "You can use shields, right?.",
                            loopTag = "squint"
                        },
                        new CustomSay()
                        {
                            who = Deck.riggs.Key(),
                            Text = "Fly away from danger, got it!.",
                            loopTag = "neutral"
                        },
                    }
                }
            }
        };
        DB.story.all[$"WeGotHurtButNotTooBad_{randall}_1"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            enemyShotJustHit = true,
            minDamageDealtToPlayerThisTurn = 1,
            maxDamageDealtToPlayerThisTurn = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Let's not take more unnecessary damage.",
                    loopTag = "squint"
                },
                new SaySwitch()
                {
                    lines = new (){
                        new CustomSay()
                        {
                            who = Deck.dizzy.Key(),
                            Text = "It's just a little hole in the hull.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.hacker.Key(),
                            Text = "You're the one with the shields here.",
                            loopTag = "squint"
                        },
                    }
                }
            }
        };
        DB.story.all[$"WeGotHurtButNotTooBad_{randall}_2"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            enemyShotJustHit = true,
            minDamageDealtToPlayerThisTurn = 1,
            maxDamageDealtToPlayerThisTurn = 1,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new (){
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "We're taking damage here!",
                            loopTag = "mad"
                        },
                        new CustomSay()
                        {
                            who = Deck.riggs.Key(),
                            Text = "We got hit!",
                            loopTag = "neutral"
                        },
                    }
                },
                new CustomSay()
                {
                    who = randall,
                    Text = "I could have done something about it...",
                    loopTag = "misplay"
                }              
            }
        };
        DB.story.all[$"WeDidOverThreeDamage_{randall}_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisTurn = 4,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Good, good.",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.all[$"WeDidOverThreeDamage_{randall}_1"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisTurn = 4,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Good job, guys.",
                    loopTag = "neutral"
                },
                new SaySwitch()
                {
                    lines = new (){
                        new CustomSay()
                        {
                            who = Deck.peri.Key(),
                            Text = "Likewise",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.riggs.Key(),
                            Text = "You too!",
                            loopTag = "neutral"
                        },
                    }
                }
            }
        };
        DB.story.all[$"WeDidOverThreeDamage_{randall}_2"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisTurn = 4,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Pretty decent.",
                    loopTag = "neutral"
                },
                new SaySwitch()
                { 
                    lines = new (){
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "Just decent???",
                            loopTag = "mad"
                        },
                        new CustomSay()
                        {
                            who = Deck.goat.Key(),
                            Text = "You have high standards.",
                            loopTag = "neutral"
                        },
                    }
                }
            }
        };
        DB.story.all[$"WeDidOverFiveDamage_{randall}_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisTurn = 6,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "They're getting oofed.",
                    loopTag = "neutral"
                },
                new SaySwitch()
                {
                    lines = new (){
                        new CustomSay()
                        {
                            who = "comp",
                            Text = "Oofed? Is that a word?",
                            loopTag = "squint"
                        },
                        new CustomSay()
                        {
                            who = Deck.shard.Key(),
                            Text = "New magic word!",
                            loopTag = "stoked"
                        },
                    }
                }
            }
        };
        DB.story.all[$"WeDidOverFiveDamage_{randall}_1"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisTurn = 6,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "We'll win soon at this rate.",
                    loopTag = "neutral"
                },
                new SaySwitch()
                {
                    lines = new (){
                        new CustomSay()
                        {
                            who = Deck.hacker.Key(),
                            Text = "You sound very confident.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.goat.Key(),
                            Text = "Let's finish them off.",
                            loopTag = "neutral"
                        },
                    }
                }
            }
        };
        DB.story.all[$"WeDidOverFiveDamage_{randall}_2"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisTurn = 6,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "That's gotta hurt.",
                    loopTag = "neutral"
                },
                new SaySwitch()
                {
                    lines = new (){
                        new CustomSay()
                        {
                            who = Deck.dizzy.Key(),
                            Text = "Wouldn't want to be them.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.peri.Key(),
                            Text = "And we have more hurt to deal.",
                            loopTag = "neutral"
                        },
                    }
                }
            }
        };
        DB.story.all[$"WeDidOverFiveDamage_{randall}_3"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { randall },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisTurn = 6,
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Big damage.",
                    loopTag = "neutral"
                },
                new SaySwitch()
                {
                    lines = new (){
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "And we're not done.",
                            loopTag = "sly"
                        },
                    }
                }
            }
        };
    }
}
