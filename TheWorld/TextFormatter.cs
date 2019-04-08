using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    public static class TextFormatter
    {
        public static ConsoleColor Warning = ConsoleColor.Yellow;
        public static ConsoleColor Danger = ConsoleColor.Red;
        public static ConsoleColor Positive = ConsoleColor.Green;
        public static ConsoleColor Neutral = ConsoleColor.White;

        /// <summary>
        /// print a message in a particular color.
        /// </summary>
        /// <param name="color">Any color you want!</param>
        /// <param name="message"></param>
        /// <param name="stuff"></param>
        public static void PrintLine(ConsoleColor color, string message, params object[] stuff)
        {
            ConsoleColor prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message, stuff);
            Console.ForegroundColor = prev;
        }

        /// <summary>
        /// Print a message in the predefined Warning color.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stuff"></param>
        public static void PrintLineWarning(string message, params object[] stuff)
        {
            PrintLine(Warning, message, stuff);
        }

        /// <summary>
        /// Print a message in the predefined Danger color.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stuff"></param>
        public static void PrintLineDanger(string message, params object[] stuff)
        {
            PrintLine(Danger, message, stuff);
        }

        /// <summary>
        /// Print a message in the predefined Positive color.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stuff"></param>
        public static void PrintLinePositive(string message, params object[] stuff)
        {
            PrintLine(Positive, message, stuff);
        }

        /// <summary>
        /// print a message in a particular color.
        /// </summary>
        /// <param name="color">Any color you want!</param>
        /// <param name="message"></param>
        /// <param name="stuff"></param>
        public static void Print(ConsoleColor color, string message, params object[] stuff)
        {
            ConsoleColor prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(message, stuff);
            Console.ForegroundColor = prev;
        }

        /// <summary>
        /// Print a message in the predefined Warning color.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stuff"></param>
        public static void PrintWarning(string message, params object[] stuff)
        {
            Print(Warning, message, stuff);
        }

        /// <summary>
        /// Print a message in the predefined Danger color.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stuff"></param>
        public static void PrintDanger(string message, params object[] stuff)
        {
            Print(Danger, message, stuff);
        }

        /// <summary>
        /// Print a message in the predefined Positive color.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stuff"></param>
        public static void PrintPositive(string message, params object[] stuff)
        {
            Print(Positive, message, stuff);
        }
    }
}
