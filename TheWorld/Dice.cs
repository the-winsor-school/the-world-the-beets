using System;

namespace TheWorld
{
    /// <summary>
    /// Represents a Dice for a DnD style game!
    /// </summary>
	public class Dice
	{
        #region Static Dice Methods

        /// <summary>
        /// The random number generator for the dice!
        /// </summary>
        private static Random rand = new Random();

		/// <summary>
		/// Type of Die to throw.
		/// Enums can be used to limit the number of possible values for a variable.
		/// In this case, the enum will be treated like an Integer, but only the defined values
		/// are allowed.
		/// </summary>
		public enum Type
		{
			Coin = 2,
			D4 = 4,
			D6 = 6,
			D8 = 8,
			D10 = 10,
			D12 = 12,
			D20 = 20
		}

		/// <summary>
		/// Roll a number of the specified type of dice.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="count">numbre of dice to throw.</param>
		/// <returns>The Sum of all dice thrown</returns>
		public static int Roll( Dice.Type type, int count = 1, int modifier = 0 )
		{
			int total = 0;
			for(int i = 0; i < count; i++)
			{
				total += rand.Next((int)type) + 1;
			}
			return total + modifier;
		}

        /// <summary>
        /// Shortcut for Roll(D20, 1).
        /// </summary>
        /// <returns></returns>
        public static int Roll20() => Roll(Type.D20);

        #endregion

        /// <summary>
        /// Type of Dice(e.g. Coin, D6, D20 etc)
        /// </summary>
        public Type DiceType { get; protected set; }

        /// <summary>
        /// Number of Dice rolled.
        ///(e.g. 3d10)
        /// </summary>
        public int Count { get; protected set; }

        /// <summary>
        /// Modifier added to the Dice roll
        ///(e.g. 4d6 +5)
        /// </summary>
        public int Modifier { get; protected set; }


        /// <summary>
        /// Initialize a Dice type for you to roll
        /// for example 3d6 would be:
        /// new Dice(3, Type.D6)
        /// 
        /// and a 4d4+5 would be:
        /// new Dice(4, Type.D4, 5);
        /// 
        /// the modifier can also be negative:
        /// 3d10-4 =>  new Dice(3, Type.D10, -4)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="count"></param>
        /// <param name="modifier"></param>
        public Dice(int count, Type type, int modifier = 0)
        {
            DiceType = type;
            Count = count;
            Modifier = modifier;
        }

        public int Roll() => Dice.Roll(DiceType, Count) + Modifier;
    }
}

