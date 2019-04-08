using System;
using System.Collections.Generic;

namespace TheWorld
{
	/// <summary>
	/// The Player playing the game!
	/// </summary>
	public class Player
	{
        /// <summary>
        /// This player's name.
        /// </summary>
		public string Name
		{
			get;
			protected set;
		}

        /// <summary>
        /// This player's stats
        /// </summary>
		public StatChart Stats
		{
			get;
			set;
		}

        /// <summary>
        /// How much money does the player have?
        /// </summary>
        public Money MoneyPouch
        {
            get;
            protected set;
        }

		/// <summary>
		/// The items. In Stacks.  By Name.
		/// </summary>
		private Dictionary<string, List<Item>> Backpack;

		public Player( string name )
		{
			Name = name;
			Backpack = new Dictionary<string, List<Item>>();
		}

		/// <summary>
		/// Put the Item in your backpack.
		/// </summary>
		/// <param name="item">Item.</param>
		public void PickUp( Item item )
		{
			if(Backpack.ContainsKey(item.Name))
				Backpack [item.Name].Add(item);
			else
				Backpack.Add(item.Name, new List<Item>() { item });
		}
	}
}

