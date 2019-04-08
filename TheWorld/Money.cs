using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    /// <summary>
    /// Represents a money value in TheWorld
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
            // You write this method!!
            return new Money();
        }
    }
}
