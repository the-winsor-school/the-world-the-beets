using System;
using System.Collections.Generic;

using static TheWorld.TextFormatter;

namespace TheWorld
{
	public partial class MainClass
	{
		private static Area currentArea;
		private static Player player;

		/// <summary>
		/// The command words.
		/// These are all the words that the game will accept as commands.
		/// You will need to add more words to make the game more interesting!
		/// </summary>
		private static List<string> CommandWords = new List<string>()
        {
			"go", "look", "help", "quit", "examine", "fight"
		};

		public static void Main(string[] args)
		{
            // Initialization
			PrintPositive("What is your name?  ");
			player = new Player(Console.ReadLine());

            // Check out that second parameter?!?! WAHT!@
            int hps = Dice.Roll(Dice.Type.D6, modifier: 4);

            player.Stats = new StatChart() {
                Level = 1,
                MaxHPs = hps,
                HPs = hps,
                Atk = new Dice(1, Dice.Type.D6),
                Def = new Dice(1, Dice.Type.D4),
                Exp = 0
			};

            // Create the World from the WorldBuilder class.
			currentArea = WorldBuilder.BuildWorld();

			string command = "";

			Console.WriteLine(currentArea);

			while(!command.ToLower().Equals("quit"))
			{
				// Do the Game Loop!
				Console.Write(">> ");
				command = Console.ReadLine();

				ParseCommand(command.ToLower());
			}

			PrintLinePositive("Bye!");
			Console.ReadKey();
		}

		/// <summary>
		/// Parses the command and do any required actions.
		/// </summary>
		/// <param name="command">Command as typed by the user.</param>
		private static void ParseCommand(string command)
		{
			string[] parts = command.Split(' ');
			string cmdWord = parts [0];

			if(!CommandWords.Contains(cmdWord))
			{
				PrintLineWarning("I don't understand...(type \"help\" to see a list of commands I know.)");
				return;
			}

			if(cmdWord.Equals("look"))
			{
				ProcessLookCommand(parts);
			} 
			else if(cmdWord.Equals("go"))
			{
				ProcessGoCommand(parts);
			}
            else if(cmdWord.Equals("fight"))
            {
                ProcessFightCommand(parts);
            }
		}

        private static void ProcessFightCommand(string[] parts)
        {
            Creature creature;
            try
            {
                creature = currentArea.GetCreature(parts[1]);
            }
            catch(WorldException e)
            {
                if(currentArea.HasItem(parts[1]))
                    PrintLineWarning("You can't fight with that...");
                else
                    PrintLineDanger(e.Message);
                return;
            }

            CombatResult result = DoCombat(ref creature);
            
            switch(result)
            {
                case CombatResult.Win: PrintLinePositive("You win!");
                    player.Stats.Exp += creature.Stats.Exp;
                    currentArea.RemoveCreature(parts[1]);
                    break;
                case CombatResult.Lose: PrintLineDanger("You lose!");
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
			if(parts.Length == 1)
				Console.WriteLine(currentArea.LookAround());
			else
			{
                // try to find the thing that the user is looking at.
				try
				{
                    // if it is there, print the appropriate description.
					Console.WriteLine(currentArea.LookAt(parts [1]));
				}
				catch(WorldException e)
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
			if(parts.Length == 1)
				PrintLineWarning("Go where?");
			else
			{
                // try to find the neigbor the user has indicated.
				try
				{
                    // move to that area if the command is understood.
					currentArea = currentArea.GetNeighbor(parts[1]);
				}
				catch(WorldException e)
				{
                    // if GetNeighbor throws and exception, print the explanation.
					PrintLineDanger(e.Message);
				}
			}
		}


	}
}
