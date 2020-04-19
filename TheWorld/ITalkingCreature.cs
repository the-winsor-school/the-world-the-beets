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

    //VM: Overall, I created a new class, SleepyScientist
    //I made a new creature, Sleepy Scientist (which you can look and talk with and technically fight with - but that's not nice...)
    //below, I added implemented the ITalkingCreature interface and connected it with the Talk command (in BasicCommandParser.cs)
    //now, you can talk with this scientist! yay!
    public class SleepyScientist : Creature, ITalkingCreature
    {
        double talkCount = 0;

        public void Talk(string word)
        {
            //this if statement allows us to cycle through the dialogue a second time
            //(b/c it's probably not practical to write a 100 sentences if we don't cycle)
            //by starting at 1.5, we can change the beginning of the cycle to "wake up again" instead of just "wake up" and thus add, ~VARIETY~
            if (talkCount > 5)
            {
                talkCount = 1.5;
            }

            //below, we are cycling through some semi-one-sided dialogue from the scientist
            //(using the talkCount variable, double type, so we can keep track of which dialogue sentence we want to use next)
            switch (talkCount)
            {
                case 0:
                    TextFormatter.PrintLinePositive("ZzZZzzZZZzzZZzzz....");
                    talkCount += 1;
                    break;
                case 1:
                    TextFormatter.PrintLinePositive("ZZzzZz...w- what? go away... i'm trying to sleep...");
                    talkCount += 1;
                    break;
                case 1.5:
                    TextFormatter.PrintLinePositive("zZzzZz...w- why are you waking me up again? just...go away... i'm still want to sleep...");
                    talkCount += 0.5;
                    break;
                case 2:
                    TextFormatter.PrintLinePositive("what do you want? it's not work hours...");
                    talkCount += 1;
                    break;
                case 3:
                    TextFormatter.PrintLinePositive("ZzzZZz...take that, big 'rona.. ZZzzZ.. ka'pow!");
                    talkCount += 1;
                    break;
                case 4:
                    TextFormatter.PrintLinePositive("ah! just let me sleep!");
                    talkCount += 1;
                    break;
                case 5:
                    TextFormatter.PrintLinePositive("ZZzZzz...i'm just gonna block you out for the next couple hours...ZZzZZzZZz...");
                    talkCount += 1;
                    break;
                default:
                    TextFormatter.PrintLinePositive("ZZZzZZzZZZzzZZZZ...");
                    break;
            }
            
        }
    }

    public class Intern : Creature, ITalkingCreature
    {
        public void Talk(string word)
        {
            TextFormatter.PrintLinePositive("TEST WORKS: YOU ARE TALKING TO THE INTERN");
            TextFormatter.PrintLineSpecial("'talk intern... :");
            TextFormatter.PrintLineSpecial("[TEASE] [THREATEN] [CHITCHAT] [QUESTION]");
        }
    }

}



