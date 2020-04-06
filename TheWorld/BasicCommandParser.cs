using System;
using System.Collections.Generic;
using System.Linq;

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
			"go", "look", "help", "quit", "examine", "fight", "played_time" 
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
            else if (cmdWord.Equals("help"))
            {
                // TODO:  Implement this to show a new player how to use commands!
            }

            // TODO: Many Achievements
            // Implement more commands like "use" and "get" and "talk"
		}


        /// <summary>
        /// TODO:  Write this Method
        /// Several Achievements inside.
        /// </summary>
        /// <param name="parts"></param>
        private static void ProcessHelpCommand(string[] parts)
        {
            if(parts.Length == 1)
            {
                // TODO:  Easy Achievement (1):
                // the whole command is just "help".  Print a generic help message that
                // tells the player what valid command words are and how to formulate them
                //
                // TODO:  Easy Achievement (2):
                // Print a helpful example that shows the Player an example command that
                // will work in the current Area.  (e.g. "look [something]" where that
                // something is a valid thing to look at in the CurrentArea.
            }
            if(parts.Length == 2)
            {
                // TODO: Moderate Achievement (3):
                // In this case, the user is looking for help with a specific command, so
                // you should verify that the second word in the string is a valid command word
                // then for each possible valid command word, print a useful help message that
                // explains what the command does and an example of how to use it.
                // If the second word is not a valid command, make sure your message is clearly
                // an Error message (Use the PrintWarning() method to make it obvious).
            }
        }

		/// <summary>
		/// Enter Combat mode.
		/// </summary>
		/// <param name="parts">Command as typed by the user split into individual words.</param>
		private static void ProcessFightCommand(string[] parts)
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

                    // TODO: Part of a larger achievement
                    // After defeating an Enemy, they should drop their Inventory
                    // into the CurrentArea so that the player can then PickUp those Items.
					break;
				case CombatResult.Lose:
					PrintLineDanger("You lose!");
                    // TODO:  Easy Achievement:
                    // What happens when you die?  Deep questions.
					break;
				case CombatResult.RunAway:
					// TODO: Moderate Achievement
					// Handle running away.  What happens if you run from a battle?
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
                    //this will catch all other errors (besides the player adding an extra word or two after 
                    PrintLineDanger(e.Message);
                }
            }
        }
    }
}
