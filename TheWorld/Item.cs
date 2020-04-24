using System;
using System.Collections.Generic;

namespace TheWorld
{
	/// <summary>
	/// A generic item in the world
	/// </summary>
	public class Item
	{
        /// <summary>
        /// Name of the Item
        /// </summary>
		public string Name
		{
			get;
			set;
		}

        /// <summary>
        /// A description of the item.
        /// </summary>
		public string Description
		{
			get;
			set;
		}

        /// <summary>
        /// How much is the Item worth?
        /// </summary>
		public Money Value
		{
			get;
			set;
		}
	}

    // TODO: VM Moderate Achievement
    // Build a "Book" class which is an Item that is both Carryable and Useable.
    // The Use method should print a short bit of text which is the "Story" or 
    // maybe some Plot element in your game.

    public class Book : Item, ICarryableItem, IUseableItem
    {
        //these two lines below fully implement the ICarryableItem interface
        public int Weight { get; set; }
        public int UseCount { get; set; }

        //these two functions, Use() and Use(ref object target), fully implement the IUseableItem interface
        public void Use()
        {
            TextFormatter.PrintLinePositive("You see your own scrawled, messy handwriting.");
            TextFormatter.PrintLineWarning("4/1/20");
            //just FYI this is the intro sequence and I just added it as a reminder as to what the player is doing and who the player is supposed to be :)
            TextFormatter.PrintLineWarning("To anyone who finds this book: I am a scientist here in this world. It is a blessing and a curse. One day, as I was sitting at my desk, quarantined in my lab, I noticed a face mask blow by in the wind. I reminisced to the old days before Big 'Rona ruled the world and everyone suffered her oppression. Back when I could high-five my friends, and have a meeting without worrying about wifi. Suddenly, in that moment, I realized that it was my destiny to defeat Big ‘Rona. I saw that a long journey to defeat Big ‘Rona and her minions lay ahead, and I understood the dangers I would be facing. I made sure to fully prepare myself with masks, toilet paper, and Purell. Tubs of purell. I wrote this as my final note to my love if anything ever happened to me. Bertha, my love, my pet cow, my life, I hope I will be able to see you again. Wish me luck! <3");
        }

        //since we can't use a book ON something, we'll just laugh at the player for a sec and then just make them read the book's contents
        public void Use(ref object target) 
        {
            TextFormatter.PrintWarning("You try to use the book on the {0}... So you fling your book at the {0}...", target);
            TextFormatter.PrintWarning("It doesn't work, of course, and you, growing red in the face, walk over and pick up your book and finally read it.", target);
            Use(); 
        }

    }


    /// <summary>
    /// An Item that you can put in the world and then use it!
    ///
    /// TODO: Moderate Achievement
    /// Requires:  Implement the Use Command.
    /// Add this Item to the world somewhere and then USE it!
    /// </summary>
    public class SurpriseBox : Item, IUseableItem
    {
        private bool UsedOnce;

        public SurpriseBox()
        {
            UsedOnce = false;
        }

        public void Use()
        {
            if (!UsedOnce)
            {
                TheGame.CurrentArea.AddCreature(new Creature()
                {
                    Name = "Giant Dragonfly",
                    Description = "Holy shit how did that thing fit in that box! It's like 10 feet long!",
                    Stats = new StatChart()
                    {
                        Atk = new Dice(Dice.Type.D10, 2, 5),
                        Def = new Dice(Dice.Type.D8, modifier: 2),
                        HPs = 36,
                        Exp = 1000,
                        Level = 10,
                        MaxHPs = 36
                    }
                },
                    "giant_dragonfly"
                );

                UsedOnce = true;

                TextFormatter.PrintLineSpecial("A Giant Dragonfly crawls out of the box.{0}You notice in the back of your mind that the box seems way too small to have contained this thing.... (press Enter to coninue)");
                Console.ReadLine();

                TheGame.SurpriseFight("giant_dragonfly");
            }
        }

        /// <summary>
        /// It's a surprise!  It doesn't matter if you use it /on/ something or not.
        /// </summary>
        /// <param name="target"></param>
        public void Use(ref object target) => Use();
    }

    /// <summary>
    /// A Prototype Healing Item that might come in Handy.
    ///
    /// Notice that this Extends the Item class, AND implements both
    /// ICarryable and IUseableItem
    ///
    /// TODO:  Moderate Achievement
    ///
    /// Implement this Item in TheWorld
    /// Requires:  Get command, Use command
    ///
    /// Place this item somewhere in the world, let the Player find it, and use it.
    /// 
    /// </summary>
    public class HealingPotion : Item, ICarryableItem, IUseableItem
    {
        /// <summary>
        /// How much does this thing weigh?
        /// What does that even mean?
        /// </summary>
        public int Weight { get; set; }

        public int UseCount { get; set; }

        /// <summary>
        /// How much does this potion Heal you by?
        /// </summary>
        public int HealValue { get; set; }

        /// <summary>
        /// Use this potion to heal yourself.
        /// </summary>
        public void Use()
        {
            TheGame.Player.Stats.HPs += HealValue;

            if(--UseCount <= 0) // short hand, subtract one from UseCount and then compare.
                throw new ItemDepletedException(string.Format("Your {0} has run out.", this.Name), this);
        }


        /// <summary>
        /// Use this potion to heal a Creature.
        /// </summary>
        /// <param name="target">target must be of type Creature.</param>
        public void Use(ref object target)
        {
            if(target is Creature)
            {
                Creature creature = (Creature)target;
                creature.Stats.HPs += HealValue;
                if (--UseCount <= 0)
                    throw new ItemDepletedException(string.Format("Your {0} has run out.", this.Name), this);
            }
            else
            {
                throw new WorldException(string.Format("You can't use this item on {0}...", this.Name), target);
            }
        }
    }
    public class Mask : Item, ICarryableItem, IEquippableItem
    {
        public int Weight { get; set; }

        public int UseCount { get; set; }

        public int ThisBuff { get; set; }

        public StatChart Stats { get; set; }

        public bool IsEquipped { get; set; }

       // public Dice Def { get; set; }

        public void Equip()
        {
            if (IsEquipped == false)
            {
                //TheGame.Player.Stats.DefBuff=ThisBuff; //added a modifier in the program file called defbuff and thats equal to the buff of a give item

                TheGame.Player.Stats.Def.Modifier += ThisBuff;

                IsEquipped = true;
                TextFormatter.PrintLineSpecial("Equipped {0}! It adds {1} to your defense", this.Name, this.ThisBuff);
            }
            else
            {
                throw new WorldException(string.Format("You already equipped that {0}", this.Name), this); //can't equip something that's already equipped
            }

        }
    }
        
}

