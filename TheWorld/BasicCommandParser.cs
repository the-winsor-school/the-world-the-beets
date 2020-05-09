using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TheWorld
{
    // this allows me to use the static methods defined in TextFormatter without typing "TextFormatter." every time.
	using static TheWorld.TextFormatter;


    /// <summary>
    /// You might notice that this class has the same name as the one in
    /// Program.cs as well as in Combat.cs
    ///
    /// This is allowed because the class has the "partial" attribute.  This
    /// means that the class has parts spread across multiple files because
    /// it is large and breaking it up into chunks makes it easier to follow.
    ///
    /// This file contains the methods and properties that are only relevant to
    /// processing game commands.
    ///
    /// The fact is, the entire game is really made up of just a bunch of calls
    /// to the ParseCommand method.  At least until you spice things up a bit!
    /// </summary>
    public static partial class TheGame
    {


        /// <summary>
        /// The command words.
        /// These are all the words that the game will accept as commands.
        /// You will need to add more words to make the game more interesting!
        /// </summary>
        private static List<string> CommandWords = new List<string>()
        {
            "go", "look", "help", "quit", "examine", "fight", "played_time", "talk", "equip", "use", "get"
        };

        //This below CommandWordsFormats list was created for the purpose of improving the Help command a few lines below
        //By making a list here instead of just typing it in PrintLineSpecial("....."); is for convenience in the future
        //if we ever need to add new command words or change usages of existing command words
        private static List<string> CommandWordsFormats = new List<string>()
        {
            "go [direction]", "look", "look [item or creature]", "help", "help [command word]", "quit", "examine", "examine [item or creature]", "fight [creature]", "played_time"
        };

        /// <summary>
        /// TODO:  Easy Achievement
        /// Improve the readability of other code by completing this method.
        ///
        /// This should return True if and only if the CommandWords list contains
        /// the give cmdWord.
        ///
        /// Implement this method in appropriate places such as the ParseCommand method.
        /// 
        /// </summary>
        /// <param name="cmdWord"></param>
        /// <returns></returns>
        private static bool IsValidCommandWord(string cmdWord) => throw new NotImplementedException();

        /// <summary>
        /// Parses the command and do any required actions.
        /// </summary>
        /// <param name="command">Command as typed by the user.</param>
        private static void ParseCommand(string command)
        {
            // Break apart the command into individual words:
            // This is why command words and unique names for objects cannot contain spaces.
            string[] parts = command.Split(' ');
            // The first word is the command.
            string cmdWord = parts.First();


            if (!CommandWords.Contains(cmdWord))
            {
                PrintLineWarning("I don't understand...(type \"help\" to see a list of commands I know.)");
                return;
            }

			if (cmdWord.Equals("look"))
			{
				ProcessLookCommand(parts);
			}
            else if (cmdWord.Equals("go"))
			{
				ProcessGoCommand(parts);
			}
			else if (cmdWord.Equals("fight"))
			{
				ProcessFightCommand(parts);
			}
            else if (cmdWord.Equals("played_time"))
            {
                ProcessPlayedTimeCommand(parts);
            }
            else if (cmdWord.Equals("use"))

            {
				ProcessUseCommand(parts);
			}
			else if (cmdWord.Equals("get"))
			{
				ProcessGetCommand(parts);
			}
			else if (cmdWord.Equals("help"))
            {
                // TODO:  Implement this to show a new player how to use commands! VM
                ProcessHelpCommand(parts);
                //This line takes us to the ProcessHelpCommand method a couple lines below :)
            }
            else if (cmdWord.Equals("equip"))
            {
				ProcessEquipCommand(parts);
            }

            // TODO: Many Achievements
            // Implement more commands like "use" and "get" and "talk"

            // ~TODO~: VM
            //"talk" command implemented
            else if (cmdWord.Equals("talk"))
            {
                ProcessTalkCommand(parts);
            }
        }

        private static void ProcessUseCommand(string[] parts)
        {
            if (parts.Length == 1)
            {
				PrintLineWarning("Please specify which item.");
				return;
			}

            if (parts.Length == 2)
            {
				CurrentArea.GetItem(parts[1]);
				IUseableItem itemToUse = CurrentArea.GetItem(parts[1]) as IUseableItem;
				if (itemToUse != null)
				{
					try
					{
						((IUseableItem)itemToUse).Use();
						PrintLinePositive("Neat thing!");
					}
					catch (ItemDepletedException ide)
					{
						PrintLineSpecial(ide.Message);
					}
					catch (WorldException we)
					{
						PrintLineDanger(we.Message);
					}
				}
                else
                {
                    PrintLineWarning("This item cannot be used...");
					return;
				}

            }
            
			string targetName = parts[3];
			object target;

			if (CurrentArea.HasItem(targetName))

				target = CurrentArea.GetItem(targetName);

			else if (CurrentArea.CreatureExists(targetName))

				target = CurrentArea.GetCreature(targetName);
			else
			{
				PrintLineWarning("I don't see any of that around here...");
				return;
			}
		}

		private static void ProcessGetCommand(string[] parts)
		{
			if (parts.Length == 1)
			{

			}
		}

		/// <summary>
		/// TODO: VM Write this Method
		/// Several Achievements inside.
		/// </summary>
		/// <param name="parts"></param>
        private static void ProcessHelpCommand(string[] parts)
        {
            if (parts.Length == 1)
            {
                // TODO:  Easy Achievement (1): VM
                // the whole command is just "help".  Print a generic help message that
                // tells the player what valid command words are and how to formulate them

                // TODO:  Easy Achievement (2): VM
                // Print a helpful example that shows the Player an example command that
                // will work in the current Area.  (e.g. "look [something]" where that
                // something is a valid thing to look at in the CurrentArea.

                PrintLinePositive("The available command words are: ");

                foreach (string cw in CommandWordsFormats)
                {
                    PrintLinePositive("=> " + cw);
                }
                //This foreach loop prints each string in the CommandWordsFormats list (see a couple lines above) on a separate line
                //this was specifically done for pleasant visual effect/formatting purposes

                PrintLinePositive("For example, you could use the 'look [item or creature]' command by typing 'look boulder'!");

            }

            else
            {
                // try to find the thing that the user is looking at.
                try
                {
                    if (parts.Length == 2)
                    {
                        // TODO: Moderate Achievement (3): VM
                        // In this case, the user is looking for help with a specific command, so
                        // you should verify that the second word in the string is a valid command word
                        // then for each possible valid command word, print a useful help message that
                        // explains what the command does and an example of how to use it.
                        // If the second word is not a valid command, make sure your message is clearly
                        // an Error message (Use the PrintWarning() method to make it obvious).

                        //below I am basically checking to see which command word the player wants help on 
                        //-> I could make another list of of explanatory sentences for each type of command word
                        //but at this point, because the sentences are so long anyway, it would just be easier writing the code this way with the sentences in the code right there
                        if (parts[1].Equals("go"))
                        {
                            PrintLinePositive("You can use the 'go' command to travel to other areas on the map. To use the command, type it in the format: 'go [direction].' For example, you could type 'go north' to go north.");
                        }
                        else if (parts[1].Equals("look"))
                        {
                            PrintLinePositive("You can use the 'look' command to look around you and to look at specific things nearby. To use the command, either type in 'look' or 'look [item or creature].' For example, you could type 'look grass' to look at the grass.");
                        }
                        else if (parts[1].Equals("help"))
                        {
                            PrintLinePositive("You can use the 'help' command to get general help on which commands to available, and specific help on how to use a specific command. To use the command, either type in 'help' (for general help) or 'help [command word]' (for specific help). For example, you could type 'help fight' for information on how the 'fight' command works.");
                        }
                        else if (parts[1].Equals("quit"))
                        {
                            PrintLinePositive("You can use the 'quit' command to quit the game. To use the command (though strongly not reccomended because this game is super awesome and top-notch quality and you'll definitely miss out on lots of fun), type 'quit.'");
                        }
                        else if (parts[1].Equals("examine"))
                        {
                            PrintLinePositive("*THIS COMMAND IS CURRENTLY UNDER CONSTRUCTION. NOT CURRENTLY WORKING (not sure where the examine code is, and it's definitely not working when I try to use it)*");
                        }
                        else if (parts[1].Equals("fight"))
                        {
                            PrintLinePositive("You can use the 'fight' command to fight creatures nearby. To use the command, type in 'fight [creature].' For example, you could type 'fight bunny' to fight a bunny (but please don't because they're adorable little creatures).");
                        }
                        else if (parts[1].Equals("played_time"))
                        {
                            PrintLinePositive("You can use the 'played_time' command to find out how long you've been playing the game. To use the command, type in 'played_time.'");
                        }
                        else
                        {
                            //considering we can only have two typed words and the first one is definitely a valid, "help," and that we've checked for all other command words
                            //the only possibillity left here is that the second word the player typed is not a command word (and we will tell the player that in the lines below)
                            PrintLineWarning("Second command word not found.");
                            PrintLinePositive("Please make sure you are typing a valid command word after 'help.' If you would like to see the list of command words again, please type 'help' alone.");
                        }

                    }
                    else
                    {
                        //this catches the possibillity that the player is trying to get help with multiple command words, or maybe just a specific command word but they're typing the target (ex: item) too
                        if (parts.Length >= 3)
                        {
                            PrintLinePositive("Please only use 'help [command word]' without typing any target uses or other extraneous words (ex: Do not type 'help look [item or creature]'. Instead, type 'help look'");
                        }
                    }

                }

                catch (WorldException e)
                {
                    // this will catch all other possible errors so the program won't crash
                    PrintLineDanger(e.Message);
                }
            }
        }
		private static void ProcessEquipCommand(string[] parts) //write to equip from backpack 
		{
			string itemName = parts[1];
			
				Item item;
			if (CurrentArea.HasItem(parts[1]))
			{

				item = CurrentArea.GetItem(parts[1]);// turn into backpack

				if (item is IEquippableItem)
				{
					try
					{
						((IEquippableItem)item).Equip();
					}
					catch (WorldException)
                    {
						return;
					}
				}
				else
				{
					throw new WorldException("You can't equip that!");
				}
			} else
			{
				throw new WorldException("That's not in your backpack!");
			}
		}

		   

        /// <summary>
        /// Enter Combat mode.
        /// </summary>
        /// <param name="parts">Command as typed by the user split into individual words.</param>
        public static void ProcessFightCommand(string[] parts)
        {
            Creature creature;
            try
            {
                creature = CurrentArea.GetCreature(parts[1]);
            }
            catch (WorldException e)
            {
                if (CurrentArea.HasItem(parts[1]))
                    PrintLineWarning("You can't fight with that...");
                else
                    PrintLineDanger(e.Message);
                return;
            }

            // This method is part of the MainClass but is defined in a different file.
            // Check out the Combat.cs file.
            CombatResult result = DoCombat(ref creature);

            switch (result)
            {
                case CombatResult.Win:
                    PrintLinePositive("You win!");
                    Player.Stats.Exp += creature.Stats.Exp;
                    CurrentArea.RemoveCreature(parts[1]);
                    // TODO: Part of a larger achievement
                    // After you gain Exp, how do you improve your stats?
                    // there should be some rules to how this works.
                    // But, you are the god of this universe.  You make the rules.

                    // TODO: VM Part of a larger achievement
                    // After defeating an Enemy, they should drop their Inventory
                    // into the CurrentArea so that the player can then PickUp those Items.
                    //for (int i = 0; i <= creature.CreatureInventory.Length(); i++)
                    //CurrentArea.AddItem();
					break;
				case CombatResult.Lose:
                    // TODO: VM Easy Achievement:
                    // What happens when you die?  Deep questions.

                    //below is basically just an overly-dramatic narrative about your DEMISE. Enjoy!
                    PrintLineDanger("As the {0} deals the fatal blow, you see your life flash before your eyes.", parts[1]);
                    //Thread.Sleep(#OfMilliSeconds) allows us to pause the program for a specified amount of time
                    Thread.Sleep(4000);
                    PrintLineDanger("You see the best parts of your childhood...");
                    Thread.Sleep(3000);
                    PrintLineDanger("Your friends. The ice-cream truck. Your school's robotics club. The 2-ply toilet paper.");
                    Thread.Sleep(4000);
                    PrintLineDanger("But you also see your arch-nemesis, BIG 'RONA, cackling away at your demise.");
                    Thread.Sleep(4000);
                    PrintLineDanger("And you slowly feel yourself fading away into the light...");
                    Thread.Sleep(5000);
                    PrintLineWarning("THE END");
                    Thread.Sleep(5000);
                    //this line below allows us to force the player to quit our game and start a new one (basically emphasises the whole-death-part of this all...)
                    System.Environment.Exit(1);
                    break;

				case CombatResult.RunAway:
                    // TODO: VM Moderate Achievement
                    // Handle running away.  What happens if you run from a battle?

                    PrintLineWarning("You feel your heart jump into your throat. You are scared that you're going to die here. That you're going to die to the {0}", parts[1]);
                    Thread.Sleep(2000);
                    PrintLineWarning("So you run away as fast and as far as you can.");
                    Thread.Sleep(2000);
                    PrintLineWarning("You scream: AAAAAaaaaaAAAAAaaaAAAAAAaaAAAAAAAaaAAAAAaaaAAAAAAAHHHHHHhhhhhHHHHhhHhhhhhhHHHHHHHHHHHH!!!!!");
                    Thread.Sleep(4000);
                    PrintLineWarning("...It's not a dignified retreat...");
                    Thread.Sleep(3000);
                    PrintLineDanger("And as you flee, the {0} takes one, last, nearly-deadly, swipe at you.", parts[1]);
                    Thread.Sleep(2000);
                    PrintLineDanger("You are down to 1 HP.");
					break;

				default: break;
			}
		}

        /// <summary>
        /// What happens when the user types "look" as the command word.
        /// </summary>
        /// <param name="parts">Command Parts.</param>
        private static void ProcessLookCommand(string[] parts)
        {
            // If you just type "look" then LookAround()
            if (parts.Length == 1)
            {
                Console.WriteLine(CurrentArea.LookAround());
            }
            else
            {
                // try to find the thing that the user is looking at.
                try
                {
                    // if it is there, print the appropriate description.
                    Console.WriteLine(CurrentArea.LookAt(parts[1]));
                }
                catch (WorldException e)
                {
                    // otherwise, print an appropriate error message.
                    PrintLineDanger(e.Message);
                }
            }
        }

        /// <summary>
        /// Processes the go command.
        /// </summary>
        /// <param name="parts">Parts.</param>
        private static void ProcessGoCommand(string[] parts)
        {
            // If the user has not indicated where to go...
            if (parts.Length == 1)
                PrintLineWarning("Go where?");
            else
            {
                // try to find the neighbor the user has indicated.
                try
                {
                    // move to that area if the command is understood.
                    CurrentArea = CurrentArea.GetNeighbor(parts[1]);
                }
                catch (WorldException e)
                {
                    // if GetNeighbor throws and exception, print the explanation.
                    PrintLineDanger(e.Message);
                }
            }
        }

        /// SUMMARY (VM)
        /// Processes the played_time command
        /// this command will tell the player how long they have been playing the game since they have started
        /// it gives the amount of time in hh:mm:ss
        private static void ProcessPlayedTimeCommand(string[] parts)
        {
            // This is where the played_time command runs successfully; aka where the only command is "played_time" and there aren't any extra words added afterwards
            if (parts.Length == 1)
            {
                PlayedTime = DateTime.Now - StartTime;
                //this string below is just converting the PlayedTime format to just hh/mm/ss
                //otherwise, as I have tested, the PlayedTime will also contain fractions of a second, which is not needed for our purposes
                //also the fractions of a second printed might confuse the player
                //added the "(hh:mm:ss)" for the sake of clarification
                string pt = "You have played for " + PlayedTime.ToString(@"hh\:mm\:ss") + " (hh:mm:ss).";
                PrintLinePositive(pt);
            }
            //using "PrintLineSpecial" for this specific text color so we can keep consistency 
            //(ex: I wouldn't want to use "PrintLineWarning" because the user hasn't had an error in commands -> they just want their playing time!)
            else
            {
                try
                {
                    //this is catching the most likely error of the player accidentally typing a command with more than one word: 'played_time'
                    if (parts.Length >= 1)
                        PrintLinePositive("In order for the 'played_time' command to work, please don't type any extra characters after 'played_time'");
                    //Now we're using "PrintLineWarning" because the user has made an error with this "played_time" command
                    //and we just want to correct them so they can use the "played_time" command properly
                }
                catch (WorldException e)
                {
                    //this will catch all other errors (besides the player adding an extra word or two after)
                    PrintLineDanger(e.Message);
                }
            }
        }

        //VM: Ultimately, this talk command allows you to talk to creatures with the ITalkingCreature interface implemented
            //And funny stuff happens when you try to talk to unvalid things, like talking to no one and talking to boulders
        private static void ProcessTalkCommand(string[] parts)
        {
            // This is where the talk command runs not quite so successfully
            //this is the scenario where you kinda just talk to yourself (since there's no target creature you're talking with)
            if (parts.Length == 1)
            {

                PrintLinePositive("You BELLOW at the top of your lungs.");
                PrintLinePositive("Since no one knows who you're talking to, no one responds...");
            }

            //this is when the player types more than 2 words, and we're just letting them know that they shouldn't do that
            //there are two scenarios: the player typed in multiple REAL creatures, or AT LEAST ONE creature that ISN'T REAL
            //this block of code below tests which of the two cases (listed in line directly above) is true :)
            else if (parts.Length > 2)
            {
                bool validCreatures = true;
                int i = 1;
                while (validCreatures == true && i <= parts.Length - 1)
                {
                    validCreatures = CurrentArea.CreatureExists(parts[i]);
                    i++;
                }
                if (validCreatures == true)
                {
                    PrintLinePositive("You try to talk to multiple creatures, which are all real, thankfully. But it's hard to keep up a conversation with even one, so you decide it's best to try to talk to just one creature at a time!");
                }
                else
                {
                    PrintLinePositive("You try to talk to multiple creatures. Some weren't real. Even hearing you address just 1 imaginary creature scared off all the other, real creatures...");
                }
            }

            //This block below allows us to have a continual conversation with the intern (there's always a "goodbye" option though)
            //this continual conversation aspect makes it easier for the player to talk
            //the player doesn't need to keep typing intern after talk and before the conversation word
            //also helps simulate a "real/normal" conversation with normal people haha ;p
            else if (parts.Length == 2 && Intern.convoIntern == true)
            {
                Creature creature = CurrentArea.GetCreature("intern"); 
                ((ITalkingCreature)creature).Talk(parts[1]);
            }

            //this is when the player's typed in two words, "talk" and parts[1], and we're going to check whether parts[1] is a valid creature that we can talk to.
            else if (parts.Length == 2)
            {
                try
                {
                    if (CurrentArea.CreatureExists(parts[1]))
                    {
                        Creature creature = CurrentArea.GetCreature(parts[1]);
                        if (creature is ITalkingCreature)
                        {
                            //SUCCESS!
                            //This is the scenario where the player has succesfully typed in a creature that they can actually talk to
                            //(checked to see whether the creature has the ITalkingCreature interface fully implemented)
                            //now we can call this specific creature's Talk method so we can engage in unique dialogue with them
                            ((ITalkingCreature)creature).Talk();
                        }
                        else
                        {
                            //we know that either the creature has the ITalkingCreature interface, or it doesn't
                            //this case below is the scenario that the player wants to talk to a NON-TALKING creature...
                            PrintLinePositive("...the {0} can't speak human...", parts[1]);
                        }
                    }
                    //this is when the player tries to talk to an item...
                    else if (CurrentArea.HasItem(parts[1]))
                    {
                        PrintLinePositive("You try to talk to the {0}... It doesn't respond, obviously. And then you realize that you're just growing slightly less sane by the second...", parts[1]);
                    }
                    else
                    {
                        PrintLinePositive("You try to talk to the '{0},' a creature that doesn't exist. Hmmm...is any of this actually real?", parts[1]);
                    }

                }
                catch (WorldException e)
                {
                    PrintLineDanger(e.Message);
                    return;
                }
            }


            }

        }
    }

