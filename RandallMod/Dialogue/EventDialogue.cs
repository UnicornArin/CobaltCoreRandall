using System.Linq;
using RandallMod;

namespace RandallMod;

internal static class EventDialogue
{
    private static ModInit Instance => ModInit.Instance;

    internal static void Inject()
    {
        string randall = ModInit.Instance.RandallDeck.Deck.Key();

        DB.story.GetNode("AbandonedShipyard")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = randall,
            Text = "I wonder if this is a trap.",
            loopTag = "thoughtful",
        });

        DB.story.GetNode("AbandonedShipyard_Repaired")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = randall,
            Text = "I normally don't like to spend time repairing the ship, but I'll take one for free.",
            loopTag = "neutral"
        });
        DB.story.all[$"ChoiceCardRewardOfYourColorChoice_{randall}"] = new()
        {
            type = NodeType.@event,
            oncePerRun = true,
            allPresent = new() { randall },
            bg = "BGBootSequence",
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "As expected.",
                    loopTag = "smug"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "You're kind of an insufferable know-it-all.",
                    loopTag = "squint"
                }
            }
        };
        
        DB.story.GetNode("CrystallizedFriendEvent")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = randall,
            Text = "If you bench me you better go and defeat the Cobalt, you hear?",
            loopTag = "accusatory"
        });
        DB.story.all[$"CrystallizedFriendEvent_{randall}"] = new()
        {
            type = NodeType.@event,
            oncePerRun = true,
            allPresent = new() { randall },
            bg = "BGCrystalizedFriend",
            lines = new()
            {
                new Wait()
                {
                    secs = 1.5
                },
                new CustomSay()
                {
                    who = randall,
                    Text = "Alright, let's finish this run.",
                    loopTag = "smug"
                }
            }
        };
        
        DB.story.GetNode("DraculaTime")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = randall,
            Text = "It's Drac, guys!",
            loopTag = "neutral"
        });
        DB.story.GetNode("GrandmaShop")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = randall,
            Text = "Chocolate cake?",
            loopTag = "glee"
        });
        DB.story.GetNode("LoseCharacterCard")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = randall,
            Text = "I hope we can toss something useless.",
            loopTag = "squint"
        });

        
        DB.story.all[$"LoseCharacterCard_{randall}"] = new()
        {
            type = NodeType.@event,
            oncePerRun = true,
            allPresent = new() { randall },
            bg = "BGSupernova",
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "I'll take the loss, it's whatever.",
                    loopTag = "misplay"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "Let's get going.",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.GetNode("Sasha_2_multi_2")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = randall,
            Text = "Sports, I suppose.",
            loopTag = "squint"
        });
        DB.story.all[$"ShopkeeperInfinite_{randall}_Multi_0"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "shopBefore" },
            allPresent = new() { randall },
            bg = "BGShop",
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Morning.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = "nerd",
                    Text = "Mornin'.",
                    loopTag = "neutral",
                    flipped = true
                },                
                new Jump()
                {
                    key = "NewShop"
                }
            }
        };
        DB.story.all[$"ShopkeeperInfinite_{randall}_Multi_1"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "shopBefore" },
            allPresent = new() { randall },
            bg = "BGShop",
            lines = new()
            {
                new CustomSay()
                {
                    who = "nerd",
                    Text = "How's it going?",
                    loopTag = "neutral",
                    flipped = true
                },
                new CustomSay()
                {
                    who = randall,
                    Text = "Could be better.",
                    loopTag = "neutral"
                },
                new Jump()
                {
                    key = "NewShop"
                }
            }
        };
        DB.story.all[$"ShopkeeperInfinite_{randall}_Multi_2"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "shopBefore" },
            allPresent = new() { randall },
            bg = "BGShop",
            lines = new()
            {
                new CustomSay()
                {
                    who = "nerd",
                    Text = "Hello!",
                    loopTag = "neutral",
                    flipped = true
                },
                new CustomSay()
                {
                    who = randall,
                    Text = "Good 'current time of day'",
                    loopTag = "neutral"
                },
                new Jump()
                {
                    key = "NewShop"
                }
            }
        };
        DB.story.GetNode("SogginsEscape_1")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = randall,
            Text = "Could you not be like that?",
            loopTag = "facepalm"
        });

        DB.story.all[$"{randall}_Intro_1"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { randall },
            once = true,
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    who = randall,
                    Text = "Morning!",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "Huh? No memory loss issues? Cryo sickness?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = randall,
                    Text = "Nope, I'm here because most modded crewmembers lean too heavily into aggression.",
                    loopTag = "explain"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "Modded crewmembers? What... Are you talking about? All of these people have been part of the Cobalt since the start.",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = randall,
                    Text = "Could you remind me how big the crew of the cobalt was meant to be?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "Like four...",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "...",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "Oh.",
                    loopTag = "squint"
                },
            }
        };
        /*
        DB.story.all[$"{randall}_introWithDizzy"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { randall, Deck.dizzy.Key() },
            once = true,
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "So you're saying that we're not only stuck in a time loop, but also within a simulation of sorts.",
                    loopTag = "intense"
                },
                new CustomSay()
                {
                    who = randall,
                    Text = "Huh? No memory loss issues? Cryo sickness?",
                    loopTag = "neutral"
                },
            }
        };*/
    }
}
