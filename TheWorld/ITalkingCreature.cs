using System;
namespace TheWorld
{
    /// <summary>
    /// A Creature that can TALK
    /// Maybe it's Humanoid, maybe it isn't!  That's up to you!
    ///
    /// TODO:  Hard Achievement (1) VM
    /// Requires:  "talk" command
    /// Write a class that Extends Creature and Implements this Interface
    /// Add a creature that you can "talk" to in The World
    ///
    /// TODO:  Hard Achievement (2) VM
    /// Requires previous achievement complete
    /// Add an NPC with complex dialog that responds differently to different
    /// input by the player.
    ///
    /// For example, if I give the command "talk name hello" the creature should
    /// respond differently than if I give "talk name go away!"
    ///
    /// (There's some extra hard parts there because the stuff after "talk name"
    /// might have important spaces in it!
    /// 
    /// </summary>
    public interface ITalkingCreature
    {
        /// <summary>
        /// Initiate or sustain dialog with this creature.
        /// You may or may not have different outcomes based on what the player Says.
        /// The return type is VOID so you dont have to return anything particular.
        /// But, you could do other interesting things like cause this creature
        /// to interact somehow (Check out that SurpriseAttack method in Combat.cs)
        /// </summary>
        /// <param name="playerInput"></param>

        void Talk(string playerInput = default);
    }


        public class SleepingScientist : Creature, ITalkingCreature
        {

            readonly Creature sleepingScientist = new Creature()
            {
                Name = "Sleeping scientist",
                Description = "A fellow scientist who is sleeping on the floor. He cuddles with a roll of toilet paper like it's a teddy bear. It's honestly a pretty sad scene to be looking at.",
                Stats = new StatChart() { Level = 1, MaxHPs = 10, HPs = 10, Atk = new Dice(Dice.Type.D4), Def = new Dice(Dice.Type.D4), Exp = 3 }
            };
            // "scientist"



            public void Talk(string word)
            {
                TextFormatter.PrintLinePositive("ZzZZzzZZZzzZZzzz....");
            }

            }
        }



