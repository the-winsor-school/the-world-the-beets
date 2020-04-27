﻿using System;
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
            if (displayString.Contains('-'))
            {
                string[] moneyParts = displayString.Split(' ');
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
                            TextFormatter.PrintLineWarning("Hmm...more than more than one unit for a single money amount? No.");
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
                            TextFormatter.PrintLineWarning("Hmm...more than more than one unit for a single money amount? No.");
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
                            TextFormatter.PrintLineWarning("Hmm...more than more than one unit for a single money amount? No.");
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
                            TextFormatter.PrintLineWarning("Hmm...more than more than one unit for a single money amount? No.");
                        }
                    }
                    else
                    {
                        TextFormatter.PrintLineWarning("Remember to add monetary units at the end of your amounts (ex: Make sure to write 5p and not just 5).");
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

            return sum;
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
            //TODO: VM You write this method!!

            return new Money();
        }
    }
}
