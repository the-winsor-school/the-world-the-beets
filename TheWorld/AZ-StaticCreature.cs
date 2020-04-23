using System;
namespace TheWorld
{
    public partial class Creature
    {
        //final boss "Big Rona"
        public static Creature BigRona => new Creature()
        {
            Name = "Big Rona"
            Description = "This is the final boss. Defeat Big Rona to win the game.",
            Stats = new StatChart()
            {
                Atk = new Dice("1d20+5"),
                Def = new Dice("1d12+10"),
                Exp = 1000
                Hps = 1000
                Level = 100
                Max
            }
        }


    }
}
