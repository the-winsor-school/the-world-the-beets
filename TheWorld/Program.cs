using System;
using System.Collections.Generic;

// this allows me to use the static methods defined in TextFormatter without typing "TextFormatter." every time.
using static TheWorld.TextFormatter;

namespace TheWorld
{
	public partial class MainClass
	{
		private static Area CurrentArea;
		private static Player Player;


		public static void Main(string[] args)
		{
            // Initialization
			PrintPositive("What is your name?  ");
			Player = new Player(Console.ReadLine());

            // Check out that second parameter?!?! WAHT!@
            int hps = Dice.Roll(Dice.Type.D6, modifier: 4);

            Player.Stats = new StatChart() {
                Level = 1,
                MaxHPs = hps,
                HPs = hps,
                Atk = new Dice(1, Dice.Type.D6),
                Def = new Dice(1, Dice.Type.D4),
                Exp = 0
			};

            // Create the World from the WorldBuilder class.
			CurrentArea = WorldBuilder.BuildWorld();

			string command = "";

            // TODO:  Easy Achievement:
            // Script an Intro Sequence setting the stage for your game.
            // Start your storyline off with a little more than just dropping
            // the player into the world with no idea what is going on....
            // ... or maybe that's what you want, nobody told me what was going on
            // when I landed in this world.

            // Display the short information about the Starting Area
			Console.WriteLine(CurrentArea);

            #region The Entire Game Happens HERE

            while (!command.ToLowerInvariant().Equals("quit"))
			{
				// Do the Game Loop!
				PrintSpecial(">> ");
				command = Console.ReadLine();

                // This command.ToLowerInvariant() is why all names for things must be in all lowercase.
				ParseCommand(command.ToLowerInvariant());
			}

			#endregion // THE GAME

			PrintLinePositive("Bye!");
		}
	}
}
