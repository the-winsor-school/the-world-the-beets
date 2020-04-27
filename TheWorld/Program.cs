using System;
using System.Collections.Generic;
using System.Threading;

// this allows me to use the static methods defined in TextFormatter without typing "TextFormatter." every time.
using static TheWorld.TextFormatter;

namespace TheWorld
{
	public static partial class TheGame
	{
		public static Area CurrentArea;
		public static Player Player;

        // TODO: Moderate Achievement
        //
        // Implement a property called PlayedTime which is of type TimeSpan
        // and a private DateTime StartTime.
        // PlayedTime should be Calculated as DateTime.Now - StartTime.
        // You can use this to tell the Player how long they have been playing.
        //
        // Implement a command "played_time" which displays the current played time
        // in hh:mm:ss format.

        //SUMMARY:
        //I created and implemented the property, PlayedTime, that is type TimeSpan and it is a measure of how long the player has been playing the game.
        //I also created the property, StartTime, that is of type DateTime and it is the time that you start the game.
        //The "played_time" command is implemented in BasicCommandParser.cs (this command tells the player how long they've been playing in hh:mm:ss format.)

        private static DateTime StartTime;
        public static TimeSpan PlayedTime;

		public static void Main(string[] args)
        {
            // Initialization
            StartTime = DateTime.Now;
			PrintPositive("What is your name?  ");
			Player = new Player(Console.ReadLine());

            // Check out that second parameter?!?! WAHT!@
            int hps = Dice.Roll(Dice.Type.D6, modifier: 4);  // roll 1d6+4



            Player.Stats = new StatChart()
            {
                Level = 1,
                MaxHPs = hps,
                HPs = hps,
                Atk = new Dice(Dice.Type.D6),  // 1d6
                Def = new Dice(Dice.Type.D4),  // 1d4 with a modifier
                Exp = 0
			};

            // Create the World from the WorldBuilder class.
			CurrentArea = WorldBuilder.BuildWorld();

			string command = "";

            // TODO: VM Easy Achievement:
            // Script an Intro Sequence setting the stage for your game.
            // Start your storyline off with a little more than just dropping
            // the player into the world with no idea what is going on....
            // ... or maybe that's what you want, nobody told me what was going on
            // when I landed in this world.

            PrintLineWarning("Welcome!");
            Thread.Sleep(2000);
            PrintLineWarning("You are a scientist here in this world.");
            Thread.Sleep(2000);
            PrintLineWarning("It is a blessing and a curse.");
            Thread.Sleep(2000);
            PrintLineWarning("Sitting at your desk, quarantined in your lab, you notice a face mask blow by in the wind.");
            Thread.Sleep(4000);
            PrintLineWarning("You reminisce to the old days before Coronavirus (aka Big ‘Rona) ruled the world and everyone suffered its oppression.");
            Thread.Sleep(3000);
            PrintLineWarning("Back when you could high five your friends and have a meeting without worrying about wifi.");
            Thread.Sleep(4000);
            PrintLineWarning("Suddenly, you know that now, at this moment, it is your destiny to defeat Big ‘Rona.");
            Thread.Sleep(5000);
            PrintLineWarning("A long journey to defeat Big ‘Rona and its minions lies ahead.");
            Thread.Sleep(4000);
            PrintLineWarning("You’ll need to defend yourself with masks, toilet paper, and tubs of Purell.");
            Thread.Sleep(2000);
            PrintLineWarning("You need to be quick.");
            Thread.Sleep(1000);
            PrintLineWarning("You need to outwit your enemies.");
            Thread.Sleep(1000);
            PrintLineWarning("And if necessary, you’ll need BRUTE FORCE to survive.");
            Thread.Sleep(4000);
            PrintLineWarning("As the sun creeps through the cobwebs and through the musty air of your lab, you feel its warmth.");
            Thread.Sleep(3500);
            PrintLineWarning("And you realize this may be the last sunrise you’ll ever see.");
            Thread.Sleep(3500);
            PrintLineWarning("But you know you’re doing the right thing, and if you succeed, the world will remember your name and your story for generations to come.");
            Thread.Sleep(5000);
            PrintLineWarning("And so your story begins. Right here. Right now.");
            Thread.Sleep(3000);

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

                // TODO: You can spice up the game by having things happen randomly
                // Add things here to insert them in between the users commands.
			}

			#endregion // THE GAME

			PrintLinePositive("Bye!");
		}
	}
}
