using System;
using System.Collections.Generic;

namespace TheWorld
{
	/// <summary>
	/// A Creature that lives in the World.
	/// </summary>
	public class Creature
	{
        /// <summary>
        /// Creature's name
        /// </summary>
		public string Name
		{
			get;
			set;
		}

        /// <summary>
        /// Creature's description.
        /// </summary>
		public string Description
		{
			get;
			set;
		}

        /// <summary>
        /// Stats of the creature.
        /// </summary>
		public StatChart Stats
		{
			get;
			set;
		}
	}
}

