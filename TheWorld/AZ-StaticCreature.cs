using System;
using System.Collections.Generic;

namespace TheWorld
{
    public partial class Creature
    {
        //final boss "Big Rona"
        public static Creature BigRona => new Creature()
        {
            Name = "Big Rona",
            Description = "This is the final boss. Defeat Big Rona to win the game.",
            Stats = new StatChart()
            {
                Atk = new Dice("1d20+5"),
                Def = new Dice("1d12+10"),
                Exp = 1000,
                HPs = 1000,
                Level = 100,
                MaxHPs = 1000,
            }
        };

        //Level 1 COVID
        public static Creature COVID1 => new Creature()
        {
            Name = "COVID 1",
            Description = "It's one of Rona's minions. You'll be seeing a lot more of these. They also look fairly small. I wonder why...",
            Stats = new StatChart()
            {
                Atk = new Dice("1d4"),
                Def = new Dice("1d4"),
                Exp = 5,
                HPs = 8,
                Level = 1,
                MaxHPs = 8, //is it ok that HPs=MaxHPs?
            }
        };

        //Level 2 COVID 
        public static Creature COVID2 => new Creature()
        {
            Name = "COVID 5",
            Description = "Yet another one of Rona's minions. This one looks a little meaner and bigger than COVID 1. Let's fight!",
            Stats = new StatChart()
            {
                Atk = new Dice("1d4+3"),
                Def = new Dice("1d6"),
                Exp = 8,
                HPs = 12,
                Level = 2,
                MaxHPs = 12, //is it ok that HPs=MaxHPs? and if it's ok, what's the point of having both HPs and MaxHps
            }
        };

        //Level 3 COVID
        public static Creature COVID3 => new Creature()
        {
            Name = "COVID 3",
            Description = "You see the minion. You pull out your Purell and prepare to fight...",
            Stats = new StatChart()
            {
                Atk = new Dice("1d4+5"),
                Def = new Dice("1d6"),
                Exp = 10,
                HPs = 15,
                Level = 3,
                MaxHPs = 15, 
            }
        };

        //Level 5 COVID
        public static Creature COVID5 => new Creature()
        {
            Name = "COVID 5",
            Description = "This one is definately bigger. Looks like I need more Purell to defend myself.",
            Stats = new StatChart()
            {
                Atk = new Dice("1d6+5"),
                Def = new Dice("1d8"),
                Exp = 15,
                HPs = 20,
                Level = 5,
                MaxHPs = 20, 
            }
        };

        //Level 10 COVID
        public static Creature COVID10 => new Creature()
        {
            Name = "COVID 1",
            Description = "This one's fierce and almost the same size as me. (I'm almost 6 feet btw).",
            Stats = new StatChart()
            {
                Atk = new Dice("1d6+10"),
                Def = new Dice("1d8+2"),
                Exp = 25,
                HPs = 40,
                Level = 10,
                MaxHPs = 40, 
            }
        };

        //Level 20 COVID
        public static Creature COVID20 => new Creature()
        {
            Name = "COVID 20",
            Description = "It's taller than you by a head. You cower under its glare. As you shudder underneath it's shadow, your hand shakily reaches for the Lysol spray....",
            Stats = new StatChart()
            {
                Atk = new Dice("1d8+10"),
                Def = new Dice("1d8+5"),
                Exp = 40,
                HPs = 80,
                Level = 20,
                MaxHPs = 80, 
            }
        };

        public static List<Creature> COVIDCluster => new List<Creature>() { COVID1, COVID2, COVID3, COVID5, COVID10, COVID20 };
    }
}
