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
                    TextFormatter.PrintLineWarning("ZzZZzzZZZzzZZzzz....");
                    talkCount += 1;
                    break;
                case 1:
                    TextFormatter.PrintLineWarning("ZZzzZz...w- what? go away... i'm trying to sleep...");
                    talkCount += 1;
                    break;
                case 1.5:
                    TextFormatter.PrintLineWarning("zZzzZz...w- why are you waking me up again? just...go away... i'm still want to sleep...");
                    talkCount += 0.5;
                    break;
                case 2:
                    TextFormatter.PrintLineWarning("what do you want? it's not work hours...");
                    talkCount += 1;
                    break;
                case 3:
                    TextFormatter.PrintLineWarning("ZzzZZz...take that, big 'rona.. ZZzzZ.. ka'pow!");
                    talkCount += 1;
                    break;
                case 4:
                    TextFormatter.PrintLineWarning("ah! just let me sleep!");
                    talkCount += 1;
                    break;
                case 5:
                    TextFormatter.PrintLineWarning("ZZzZzz...i'm just gonna block you out for the next couple hours...ZZzZZzZZz...");
                    talkCount += 1;
                    break;
                default:
                    TextFormatter.PrintLineWarning("ZZZzZZzZZZzzZZZZ...");
                    break;
            }
            
        }
    }

    public class Intern : Creature, ITalkingCreature
    {
        double talkBranch = 0;
        public static bool convoIntern = false;

        public void Talk(string word)
        {
            switch (talkBranch)
            {
                case 0:
                    TextFormatter.PrintLinePositive("You: Hi intern guy!");
                    TextFormatter.PrintLineWarning("Intern: Hello! You need anything? :D");
                    TextFormatter.PrintLineSpecial("'talk...'");
                    TextFormatter.PrintLineSpecial("[JOKE] [THREAT] [CHITCHAT] [QUESTION] [GOODBYE]");
                    convoIntern = true;
                    talkBranch = 1;
                    break;
                case 0.5:
                    TextFormatter.PrintLinePositive("You: Hi, again, intern guy.");
                    TextFormatter.PrintLineWarning("Intern: Hello! Suprised to see you again so quick, you still need anything?");
                    TextFormatter.PrintLineSpecial("'talk ____'");
                    TextFormatter.PrintLineSpecial("[JOKE] [THREAT] [CHITCHAT] [QUESTION] [GOODBYE]");
                    convoIntern = true;
                    talkBranch = 1;
                    break;
                case 1:
                    switch (word)
                    {
                        case "joke":
                            TextFormatter.PrintLinePositive("You: What did the clock do when it was hungry?");
                            TextFormatter.PrintLineWarning("Intern: Went back for seconds.");
                            TextFormatter.PrintLineSpecial("[REPRIMAND] [JOKE] [LAUGH] [GOODBYE]");
                            talkBranch = 2;
                            break;
                        case "threat":
                            TextFormatter.PrintLinePositive("You: FIGHT ME OR ELSE!!!");
                            TextFormatter.PrintLineWarning("Intern: or else... what? It sounds like you're just going to fight me either way lol...");
                            TextFormatter.PrintLineSpecial("[APOLOGIZE] [TAUNT] [THREAT] [GOODBYE]");
                            talkBranch = 3;
                            break;
                        case "chitchat":
                            TextFormatter.PrintLinePositive("You: Nope, don't need anything, thanks. How's life going?");
                            TextFormatter.PrintLineWarning("Intern: Stuff's going good! Loving this job so far!");
                            TextFormatter.PrintLineSpecial("[WORK] [PERSONAL] [FAMILY] [GOODBYE]");
                            talkBranch = 4;
                            break;
                        case "question":
                            TextFormatter.PrintLinePositive("You: What do you exactly do here, again? ");
                            TextFormatter.PrintLineWarning("Intern: Uh...I'm your lab assistant. You tell me to do stuff. Also, by the way, the tub of Purell you asked for is ready in the storage room. Let me know if you need any help with it!");
                            TextFormatter.PrintLineSpecial("[PURELL] [LIFE] [QUEST] [GOODBYE]");
                            talkBranch = 5;
                            break;
                        case "goodbye":
                            TextFormatter.PrintLinePositive("You: GOODBYE.");
                            TextFormatter.PrintLineWarning("Intern: uh, ok... bye...");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        default:
                            TextFormatter.PrintLineDanger("You're all for free speech, but you think twice about saying that to the new intern...");
                            break;
                    }
                    break;

                    //result of "joke"
                case 2:
                    switch (word)
                    {
                        case "reprimand":
                            TextFormatter.PrintLinePositive("You: LISTEN YOUNG'UN, YOU SHOULD KNOW BETTER THAN TO SPOIL THE PUNCHLINE OF A GOOD OL' JOKE!");
                            TextFormatter.PrintLineWarning("Intern: Okay, then I'll tell you a joke. Why aren't koalas considered as bears?");
                            TextFormatter.PrintLineSpecial("[WHY] [SCIENTIFIC] [PUNCHLINE] [PUNCH] [LAUGH] [GOODBYE]");
                            talkBranch = 0;
                            break;
                        case "joke":
                            TextFormatter.PrintLinePositive("You: Here's another one for ya then: why did the bike fall dow-");
                            TextFormatter.PrintLineWarning("Intern: because it was two-tired.");
                            TextFormatter.PrintLineSpecial("[CRY] [SCREAM] [LAUGH] [GOODBYE]");
                            talkBranch = 0;
                            break;
                        case "laugh":
                            TextFormatter.PrintLinePositive("You: AHAHAHAHAHHAAHAHA!!!");
                            TextFormatter.PrintLineWarning("Intern: Are you really laughing at your own joke?");
                            TextFormatter.PrintLineSpecial("[CONFRONT] [LAUGH] [GOODBYE]");
                            talkBranch = 0;
                            break;
                        case "goodbye":
                            TextFormatter.PrintLinePositive("You: Hmmph! GOODBYE THEN.");
                            TextFormatter.PrintLineWarning("Intern: Bye! :D");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        default:
                            TextFormatter.PrintLineDanger("You're all for free speech, but you think twice about saying that to the new intern...");
                            break;
                    }
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                default:
                    break;
            }

        }
    }

}



