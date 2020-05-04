using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    /// <summary>
    /// Represents a money value in TheWorld
    /// There isn't much to add to this class, but it might come in handy for other things
    /// </summary>
    public class Money
    {
        /// <summary>
        /// 1000G = 1P
        /// </summary>
        public uint Platinum { get; set; }
        /// <summary>
        /// 100S = 1G
        /// </summary>
        public uint Gold { get; set; }
        /// <summary>
        /// 100C = 1S
        /// </summary>
        public uint Silver { get; set; }
        /// <summary>
        /// the lowest denomination of coinage.
        /// </summary>
        public uint Copper { get; set; }

        public Money()
        {
            /// nothing needs to go here.  Set the properties individually.
        }

        /// <summary>
        /// TODO: VM Hard Achievement
        /// Convert a String like "400g 34c" into a Money object.
        /// You should be able to Enter large numbers, but not negative numbers.
        /// (after all, these properties are "unsigned integers")
        ///
        /// So, "2841924c" is a valid string
        /// but, "-24p" is not.
        ///
        /// "10p 249g 24s 842c" is valid but not in simplest terms, so it is
        /// up to you how you will handle the difference.
        /// 
        /// </summary>
        /// <param name="displayString"></param>
        public Money(string displayString)
        {
            if (!displayString.Contains('-'))
            {
                //we split the string entered as a parameter into multiple parts (if any)
                //these splitparts then are added to create an array
                string[] moneyParts = displayString.Split(' ');

                //then we run a loop that goes through each part
                //each time we loop, we check whether the monetary amount in in units of c, s, g, or p 
                    //player will recieve separate warnings for having mixed units (ex: 25 cg), having no units (ex: 25), or negative money amounts (ex: -25c)
                for (int i = 0; i == moneyParts.Length - 1; i++)
                {
                    if (moneyParts[i].Contains('c'))
                    {
                        moneyParts[i] = moneyParts[i].Remove('c');
                        if (!moneyParts[i].Contains('s') && !moneyParts[i].Contains('g') && !moneyParts[i].Contains('p'))
                        {
                            Copper = Convert.ToUInt16(moneyParts[i], 16);
                        }
                        else
                        {
                            TextFormatter.PrintLineWarning("Hmm...mixed units of money...? Only one unit per number please.");
                        }

                    }
                    else if (moneyParts[i].Contains('s'))
                    {
                        moneyParts[i] = moneyParts[i].Remove('s');
                        if (!moneyParts[i].Contains('c') && !moneyParts[i].Contains('g') && !moneyParts[i].Contains('p'))
                        {
                            Silver = Convert.ToUInt16(moneyParts[i], 16);
                        }
                        else
                        {
                            TextFormatter.PrintLineWarning("Hmm...mixed units of money...? Only one unit per number please.");
                        }
                    }
                    else if (moneyParts[i].Contains('g'))
                    {
                        moneyParts[i] = moneyParts[i].Remove('g');
                        if (!moneyParts[i].Contains('c') && !moneyParts[i].Contains('s') && !moneyParts[i].Contains('p'))
                        {
                            Gold = Convert.ToUInt16(moneyParts[i], 16);
                        }
                        else
                        {
                            TextFormatter.PrintLineWarning("Hmm...mixed units of money...? Only one unit per number please.");
                        }
                    }
                    else if (moneyParts[i].Contains('p'))
                    {
                        moneyParts[i] = moneyParts[i].Remove('p');
                        if (!moneyParts[i].Contains('c') && !moneyParts[i].Contains('s') && !moneyParts[i].Contains('g'))
                        {
                            Platinum = Convert.ToUInt16(moneyParts[i], 16);
                        }
                        else
                        {
                            TextFormatter.PrintLineWarning("Hmm...mixed units of money...? Only one unit per number please.");
                        }
                    }
                    else
                    {
                        TextFormatter.PrintLineWarning("Remember to add monetary units at the end of your money amounts (ex: Make sure to write 5p and not just 5).");
                    }
                }
            }
            else
            {
                TextFormatter.PrintLineWarning("Negative money is bad! Don't even think about typing negative signs!!!");
            }
        }

        /// <summary>
        /// returns a string representation of this amount of money.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            this.NormalizeCoinage();

            string output = "";
            bool addSpace = false;

            if(Platinum > 0)
            {
                output += string.Format("{0}P", Platinum);
                addSpace = true;
            }
            if(Gold > 0)
            {
                output += string.Format("{0}{1}G", addSpace ? " " : "", Gold);
                addSpace = true;
            }
            if(Silver > 0)
            { 
                output += string.Format("{0}{1}S", addSpace ? " " : "", Silver);
                addSpace = true;
            }
            if(Copper > 0 || !addSpace)
                output += string.Format("{0}{1}C", addSpace ? " " : "", Copper);
            
            return output;
        }

        /// <summary>
        /// Normalize the members of this Instance so that you have the minimum number of "coins" of each value.
        /// </summary>
        public void NormalizeCoinage()
        {
            while(this.Copper > 100)
            {
                this.Silver++;
                this.Copper -= 100;
            }

            while(this.Silver > 100)
            {
                this.Gold++;
                this.Silver -= 100;
            }

            while(this.Gold > 1000)
            {
                this.Platinum++;
                this.Gold -= 1000;
            }
        }

        /// <summary>
        /// Add together two money pouches.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Money operator +(Money a, Money b)
        {
            Money sum = a;

            sum.Silver   +=(sum.Copper + b.Copper) / 100;
            sum.Copper   +=(sum.Copper + b.Copper) % 100;

            sum.Gold     +=(sum.Silver + b.Silver) / 100;
            sum.Silver   +=(sum.Silver + b.Silver) % 100;

            sum.Platinum +=(sum.Gold + b.Gold) / 1000;
            sum.Gold     +=(sum.Gold + b.Gold) % 1000;
            sum.Platinum += b.Platinum;

            //VM: just making sure we return a nice, normalized money object with no more than 100 of each money unit
            sum.NormalizeCoinage();
            return sum;
        }

        /// <summary>
        /// VM: This function converts an object's money into just copper units, and returns a uint value.
        /// </summary>
        /// <returns>The copper.</returns>
        public uint ToCopper()
        {
            this.Copper = (10000000*this.Platinum) + (10000*this.Gold) + (100*this.Silver) + this.Copper;
            return this.Copper;
        }

        /// <summary>
        /// Subtract b from a.
        /// Throw an exception if the b is larger than a.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Money operator -(Money a, Money b)
        {
            //(writing this differently from the addition Money operator)

            //these two lines below are basically converting all the money "contained" in the properties of each a and b
            //and turning them into just one uint value: the total amount of copper
            //just like you would take all your dollars and checks and nickels and dimes and lump them all in terms of pennies!
            uint aCopper = a.ToCopper();
            uint bCopper = b.ToCopper();

            if (aCopper >= bCopper)
            {
                //this is our new object, our Money diff, which we plan to return when we are finished
                Money diff = new Money();
                diff.Copper = aCopper - bCopper;

                //here, we are dividing by the biggest (untouched) unit possible 

                //first, divide by # of c per p
                diff.Platinum = diff.Copper / 10000000;
                //change value of c accordingly to what is remaining after p(s) are "removed" from the total c amount
                diff.Copper = diff.Copper % 10000000;

                //repeat twice more until finished with all units!
                diff.Gold = diff.Copper / 10000;
                diff.Copper = diff.Copper % 10000;

                diff.Silver = diff.Copper / 100;
                diff.Copper = diff.Copper % 100;

                //then, finally, we return our new Money object whose properties are the differences in monetary values between Money a and Money b!
                return diff;

            }
            else
            {
                //the only other scenario is that a's monetary value is less than b's monetary value
                //which means b is less than a in this scenario
                //so we throw an exception that basically says the argument (the data passed in this function's parameters) are out of range for this function's purposes
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
