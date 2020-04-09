using System;

namespace TheWorld
{
	/// <summary>
	/// World builder is responsible for all World creation.  
	/// It is a static class because it is only used once at the beginning of the program to construct the world.
    ///
    /// TODO:  Create your own world!
    ///
    /// TODO:  Easy Achievement (1): //AZ
    /// Create 4 Areas which are linked together somehow.
    ///
    /// TODO:  Easy Achievement (2):
    /// Populate each of your Areas with at least 2 Objects that the player can interact with.
    ///
    /// TODO:  Easy Achievement (3):
    /// Create an Area which is accessible by a one-way entrance.
    /// (i.e. You fall through a hole and must find another way out)
    ///
    /// TODO:  Moderate Achievement (4):
    /// Draw a Map of your World on Paper or in a graphics program.
    /// Doesn't have to be crazy art work, just a visual representation of how
    /// the world is connected together.  Can be completely conceptual. 
    /// This is sometimes called "Story boarding"
    /// 
	/// </summary>
	public static class WorldBuilder
	{
		/// <summary>
		/// Builds the world. This is the method where you design your world.  Create Areas, Populate those Areas, and then link those areas together.
		/// If an area is particularly complicated, you may consider writing a helper method to break that part out.
		/// 
		/// This method returns the starting Area which is linked to the rest of the World.
		/// </summary>
		/// <returns>The starting area linked to the rest of the world.</returns>
		public static Area BuildWorld()
		{
			// This is going to be the area where the player starts.
			Area startLab = new Area() { Name = "The Lab", Description = "Blinding white walls surround every inch of you. There are no windows, only one door that leads out into the wild..." };

			// I can create a new Item and add it directly into the Area without having a separate variable for it!  Convenient!
			startLab.AddItem(new Item()
                {
				    Name = "Closet",
				    Description = "It's a closet. Go ahead and open it. Let's see what you find."
                    //make some interaction with the closet such that you can open it and find things to equip in it
			    },
                "closet"
            );

			// Doing it again--no separate variable for the new item.  It goes directly into the created area.
			startLab.AddItem(new Item()
                {
                    Name = "Floor",
                    Description = "The floor is white. Nothing special to see here.....or maybe not"
                },
				"floor"
            );

			// I can do that with any kind of object that I can create entirely in one command.
			// Don't forget that last word is the Unique Identifier.  So I can't have more than one thing in my area named "bunny"
			startLab.AddCreature(new Creature()
                {
				    Name = "ChiChi",
				    Description = "A cute hedgehog.  Actually it's not alive. It's just a stuffed animal. Hmmm",
				    //Stats = new StatChart() { Level = 1, MaxHPs = 10, HPs = 10, Atk = new Dice(Dice.Type.D4), Def = new Dice(Dice.Type.D4), Exp = 3 }
			    },
                "chichi"
            );

			startLab.AddItem(new Item()
                {
                    Name = "Facemask",
                    Description = "This lookes useful. Maybe I can put it on and see what it does."
                    //interaction with facemask??
                },
                "facemask" 
            );

			startLab.AddItem(new Item()
			{
				Name = "Lysol",
				Description = "Ooh, I can spray this."
			},
				"lysol"
			);

			startLab.AddItem(new Item()
			{
				Name = "Water",
				Description = "Oh look, it's water! Time to get hydrated."
				//interaction with water??
			},
				"water"
			);

			// Here's a second area.
			Area Park = new Area()
            {
				Name = "Park",
				Description = "A nice park, or it seems like one. I see a playground and a wide field. Let's go exploring."
			};

			// I will populate it with items and creatures in the same way...
			Park.AddItem(new Item()
                {
				    Name = "Cloud of Droplets",
				    Description = "I see a  cloud of droplets. It looks dangerous. Should I approach it?"
                    //"A funny lizard with a black stripe down its back.  It doesn't look intimidated by your presence," + " but it doesn't look very interested either. Upon closer inspection, it might not be alive..."
                    //what's the difference when there's two sections of quotation marks with the "+" in between?
			    },
                "cloudofdroplets"
            );

			Park.AddCreature(new Creature()
                {
				    Name = "COVID-19.0",
				    Description = "Look! A wild COVID-19.0! It looks ready to spread. Brace yourself...",
				    Stats = new StatChart() { MaxHPs = 10, HPs = 10, Atk = new Dice(Dice.Type.D6), Def = new Dice(Dice.Type.D4), Level = 1, Exp = 5 }
			    },
                "covid19.0"
            );

			//3rd area
			Area Supermarket = new Area() { Name = "Supermarket", Description = "Blinding white walls surround every inch of you. There are no windows, only one door that leads out into the wild..." };

			// These two lines LINK the two areas together.  Don't forget to go both ways or you'll end up with a dead end
			// and no way out!!!
			startLab.AddNeighbor(Park, "north");
			Park.AddNeighbor(startLab, "south");
			Supermarket.AddNeighbor(Supermarket, "east");

			// Go back to the Main method and tell it where to start the game!
			return startLab;
		}
	}
}

