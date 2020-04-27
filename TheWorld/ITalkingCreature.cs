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


    /// VM: Summary!
    /// This class implements the ITalkingCreature interface
    /// the Talk method for this class allows the player to talk to the intern with a complex (branching) talking system
    /// all these branches either end in fighting, awkwardness, or just friendliness :p
    public class Intern : Creature, ITalkingCreature
    {
        double talkBranch = 0;
        //this variable is an easy way to keep track of which conversation branch the player is on -> changes with each choice of word the player talks with

        public static bool convoIntern = false;
        //above; this bool officially "initiates" a conversation with the player and the intern
        //it is for the main purpose of making it easier for the player to talk with the intern
            //instead of writing "talk *intern* word", now the player just needs to type "talk word"

        readonly string[] internArray = new string[] { "fight", "intern" };
        //above; this is mostly for the purposes of when the player is initiating fights with the intern
        //basically it's like a mock parts[] array in BasicCommandParser.cs 
        //this array will be used in the place of parts[] as the parameters for the ProcessFightCommand method

        public void Talk(string word)
        {
            switch (talkBranch)
            {
                case 0:
                    TextFormatter.PrintLinePositive("You: Hi intern guy!");
                    TextFormatter.PrintLineWarning("Intern: Hello! You need anything? :D");
                    //below line introduces the player to how to use the talk command -> "talk [one of the offered choices]"
                    TextFormatter.PrintLineSpecial("'talk ____'");
                    //below line (along with all similar lines) is structured to give the player multiple choices to talk about
                    //it's not the same set of words each time because we want some ~variety~ to the player's conversation
                    TextFormatter.PrintLineSpecial("[JOKE] [THREAT] [CHITCHAT] [QUESTION] [GOODBYE]");
                    convoIntern = true;
                    talkBranch = 1;
                    break;
                case 0.5:
                    //this case, 0.5, is specifically created to serve the purpose of talking to the intern again
                    //while at the same time (most importantly), making it seem like you're really talking to the intern AGAIN 
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
                            TextFormatter.PrintLineWarning("Intern: or else... what? It sounds like you're just going to fight me either way...");
                            TextFormatter.PrintLineSpecial("[APOLOGIZE] [TAUNT] [THREAT] [GOODBYE]");
                            talkBranch = 3;
                            break;
                        case "chitchat":
                            TextFormatter.PrintLinePositive("You: Nope, don't need anything, thanks. How's life going?");
                            TextFormatter.PrintLineWarning("Intern: Stuff's going good! Loving this job so far!");
                            TextFormatter.PrintLineSpecial("[AWKWARD] [GOODBYE]");
                            talkBranch = 4;
                            break;
                        case "question":
                            TextFormatter.PrintLinePositive("You: What do you exactly do here, again? ");
                            TextFormatter.PrintLineWarning("Intern: Uh...I'm your lab assistant. You tell me to do stuff. Also, by the way, the tub of Purell you asked for is ready in the storage room. Let me know if you need any help with it.");
                            TextFormatter.PrintLineSpecial("[PURELL] [LIFE] [QUEST] [GOODBYE]");
                            talkBranch = 5;
                            break;
                        case "goodbye":
                            TextFormatter.PrintLinePositive("You: GOODBYE!");
                            TextFormatter.PrintLineWarning("Intern: uh, ok... bye...");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        default:
                            TextFormatter.PrintLineWarning("You're all for free speech, but you think it's for the best if you just stick to regular conversation stuff.");
                            //this is most normal way to say: don't try using anything besides the many multiple choices offered to you
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
                            talkBranch = 6;
                            break;
                        case "joke":
                            TextFormatter.PrintLinePositive("You: Here's another one for ya then: why did the bike fall dow-");
                            TextFormatter.PrintLineWarning("Intern: because it was two-tired.");
                            TextFormatter.PrintLineSpecial("[CRY] [SCREAM] [LAUGH] [GOODBYE]");
                            talkBranch = 7;
                            break;
                        case "laugh":
                            TextFormatter.PrintLinePositive("You: AHAHAHAHAHHAAHAHA!!!");
                            TextFormatter.PrintLineWarning("Intern: Are you really laughing at your own joke?");
                            TextFormatter.PrintLineSpecial("[CONFRONT] [LAUGH] [GOODBYE]");
                            talkBranch = 8;
                            break;
                        case "goodbye":
                            TextFormatter.PrintLinePositive("You: Hmmph! GOODBYE THEN.");
                            TextFormatter.PrintLineWarning("Intern: Bye! :D");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        default:
                            TextFormatter.PrintLineWarning("You're all for free speech, but you think it's for the best if you just stick to regular conversation stuff.");
                            break;
                    }
                    break;
                case 3:
                    switch (word)
                    {
                        case "apologize":
                            TextFormatter.PrintLinePositive("You: i'm sorry...you're right...that was weird and dumb of me...violence is never okay...T-T");
                            TextFormatter.PrintLineWarning("Intern: Haha, it's all good, boss. I'm always here if you need someone to talk to :3");
                            TextFormatter.PrintLineSpecial("[PURELL] [LIFE] [QUEST] [GOODBYE]");
                            talkBranch = 0.5;
                            break;
                        case "taunt":
                            TextFormatter.PrintLinePositive("You: YOU SCARED?");
                            TextFormatter.PrintLineWarning("Intern: nope. just confused why you're suddenly trying to fight people now...");
                            TextFormatter.PrintLineSpecial("You: HYAAA! TAKE THAT!");
                            TheGame.ProcessFightCommand(internArray);
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        case "threat":
                            TextFormatter.PrintLinePositive("You: YEAH. MAYBE I AM GOING TO FIGHT YOU EITHER WAY. YOU SHOULD BE SCARED RIGHT NOW, KID. >:(");
                            TextFormatter.PrintLineWarning("Intern: I'M NOT SCARED. LET'S FIGHT.");
                            TextFormatter.PrintLinePositive("You: TAKE THAT THEN! SUPRISE ATTACK!");
                            TheGame.ProcessFightCommand(internArray);
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        case "goodbye":
                            TextFormatter.PrintLinePositive("You: ...goodbye...");
                            TextFormatter.PrintLineWarning("Intern: ...");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        default:
                            TextFormatter.PrintLineWarning("You're all for free speech, but you think it's for the best if you just stick to regular conversation stuff.");
                            break;
                    }
                    break;
                case 4:
                    switch (word)
                    {
                        case "awkward":
                            TextFormatter.PrintLinePositive("You:...uh, I'm not great at casual chitchat so I think it's best we make a mutual agreement to end the conversation and for me to just shuffle away. Good with you?");
                            TextFormatter.PrintLineWarning("Intern: yeah...this is awkward...see ya boss.");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        case "goodbye":
                            TextFormatter.PrintLinePositive("You: goodbye. :|");
                            TextFormatter.PrintLineWarning("Intern: goodbye. :|");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        default:
                            TextFormatter.PrintLineWarning("You're all for free speech, but you think it's for the best if you just stick to regular conversation stuff.");
                            break;
                    }
                    break;
                case 5:
                    switch (word)
                    {
                        case "purell":
                            TextFormatter.PrintLinePositive("You: Where's the tub of Purell, again?");
                            TextFormatter.PrintLineWarning("Intern: Just in the backroom! To the south of us! Also, if you were thinking about bathing in it... do not bathe in it.");
                            TextFormatter.PrintLinePositive("You: Wh-");
                            TextFormatter.PrintWarning("Intern: Just... don't. I still get shivers down my spine when I think about it. It's not as nice as you think.");
                            TextFormatter.PrintLinePositive("You: Okay...bye then...thanks for your...advice... 0____________0");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        case "life":
                            TextFormatter.PrintLinePositive("You: What's the meaning of life?");
                            TextFormatter.PrintLineWarning("Intern: To wash your hands constantly. 20 seconds! Don't forget! ;)");
                            TextFormatter.PrintLinePositive("You: Okay never mind...you're hopeless. Bye intern guy.");
                            TextFormatter.PrintWarning("Intern: Byeeee :P");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        case "quest":
                            TextFormatter.PrintLinePositive("You: Hey...intern guy?");
                            TextFormatter.PrintLineWarning("Intern: Yeah...?");
                            TextFormatter.PrintLinePositive("You: I dunno... I guess I'm just nervous about this whole QUEST thing. I don't have any CLUE what I'm supposed to do. I don't even know if I'm going to see this lab again...");
                            TextFormatter.PrintWarning("Intern: Aw boss, it's going to be okay. That's for sure. Let your ANGER TOWARDS BIG 'RONA FUEL YOUR DECISION MAKING. Also, you know Bertha is upstairs, right? I've been visiting her a lot lately. She's a really good listener!");
                            TextFormatter.PrintLinePositive("You: Thanks, intern guy. I feel a lot better now. I hope I'll see you again...someday...");
                            TextFormatter.PrintWarning("Hope to see you again, too!");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        case "goodbye":
                            TextFormatter.PrintLinePositive("You: Cool! See you then!");
                            TextFormatter.PrintLineWarning("Intern: *mumble* *mumble* (took me 10 hours and 500 bottles of mini Purell. MINI BOTTLES. BECAUSE THAT'S THE ONLY KIND WE HAD AT THE LAB. But do I get a 'thank you'? or even a 'bye FRIEND'?... no i do not. >:( )");
                            TextFormatter.PrintLineWarning("Intern:...see you...boss friend... T-T");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        default:
                            TextFormatter.PrintLineWarning("You're all for free speech, but you think it's for the best if you just stick to regular conversation stuff.");
                            break;
                    }
                    break;

                case 6:
                    switch (word)
                    {
                        case "why":
                            TextFormatter.PrintLinePositive("You: Why.");
                            TextFormatter.PrintLineWarning("Intern: Because they don't KOALA-fy!");
                            TextFormatter.PrintLinePositive("You: ...goodbye.");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        case "scientific":
                            TextFormatter.PrintLinePositive("You: ACTUALLY, koala's aren't bears at all. They are marsupials.");
                            TextFormatter.PrintLineWarning("Intern: ...that was way worse than saying the actual punchline of the joke.");
                            TextFormatter.PrintLinePositive("You: I know. Goodbye intern man.");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        case "punchline":
                            TextFormatter.PrintLinePositive("You: Because they don't KOALA-fy!!");
                            TextFormatter.PrintLineWarning("Intern: ...that was mean and hurtful...goodbye.");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        case "punch":
                            TextFormatter.PrintLinePositive("You: TAKE THIS!");
                            TextFormatter.PrintLineWarning("Intern: wha-");
                            TheGame.ProcessFightCommand(internArray);
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        case "laugh":
                            TextFormatter.PrintLinePositive("You: HAHAHAHAHAHA!");
                            TextFormatter.PrintLineWarning("Intern: Okay, there's no punchline yet...this is weird...goodbye...?");
                            TextFormatter.PrintLinePositive("You: hehehehe.");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        case "goodbye":
                            TextFormatter.PrintLinePositive("No, I'm leaving. This is a toxic relationship and a dangerous environment. Goodbye.");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        default:
                            TextFormatter.PrintLineWarning("You're all for free speech, but you think it's for the best if you just stick to regular conversation stuff.");
                            break;
                    }
                    break;
                case 7:
                    switch (word)
                    {
                        case "cry":
                            TextFormatter.PrintLinePositive("You: *crying uncontrollably* T-T");
                            TextFormatter.PrintLineWarning("Intern: Aw it's going to be okay, boss :3 *pats you on back*");
                            TextFormatter.PrintLinePositive("You: *staggers away, still sobbing*");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        case "scream":
                            TextFormatter.PrintLinePositive("You: AHHHHHHHHHHHHHHH!");
                            TextFormatter.PrintLineWarning("Intern: O___o goodbye then...");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        case "laugh":
                            TextFormatter.PrintLinePositive("You: hahahahaha...*walks away w/o saying goodbye*");
                            TextFormatter.PrintLineWarning("Intern: *staring from a distance* why is boss still laughing? weird....");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        case "goodbye":
                            TextFormatter.PrintLinePositive("You: GOODBYE. >:O");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        default:
                            TextFormatter.PrintLineWarning("You're all for free speech, but you think it's for the best if you just stick to regular conversation stuff.");
                            break;
                    }
                    break;
                case 8:
                    switch (word)
                    {
                        case "confront":
                            TextFormatter.PrintLinePositive("You: YES I AM AND I'M NOT ASHAMED OF IT. GOODBYE.");
                            TextFormatter.PrintLineWarning("Intern: Whatever you say, boss!");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        case "laugh":
                            TextFormatter.PrintLinePositive("You: AHAHHAHAHAHAHAHA.");
                            TextFormatter.PrintLineWarning("Intern: You're scaring me. Bye! *ducks and covers under his desk*");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        case "goodbye":
                            TextFormatter.PrintLinePositive("You: heh...bye intern boy...");
                            TextFormatter.PrintLineWarning("Intern: bye boss boy...");
                            convoIntern = false;
                            talkBranch = 0.5;
                            break;
                        default:
                            TextFormatter.PrintLineWarning("You're all for free speech, but you think it's for the best if you just stick to regular conversation stuff.");
                            break;
                    }
                    break;
                default:
                    TextFormatter.PrintLineWarning("You're all for free speech, but you think it's for the best if you just stick to regular conversation stuff.");
                    convoIntern = false;
                    talkBranch = 0.5;
                    break;
            }

        }
    }

}



