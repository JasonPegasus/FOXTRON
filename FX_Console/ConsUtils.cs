using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX_Console
{
    public class ConsUtils
    {
        public static ConsoleColor defaultColor = ConsoleColor.White;
        public static ConsoleColor infoTitleColor = ConsoleColor.DarkCyan;
        public static ConsoleColor infoColor = ConsoleColor.Cyan;
        public static ConsoleColor titleColor = ConsoleColor.Yellow;
        public static ConsoleColor subtitleColor = ConsoleColor.DarkYellow;
        public static ConsoleColor userError = ConsoleColor.Red;
        public static ConsoleColor programError = ConsoleColor.DarkRed;
        public static ConsoleColor successColor = ConsoleColor.Green;
        public static ConsoleColor successSubColor = ConsoleColor.DarkGreen;

        public static void empty() { Console.WriteLine(); }

        public static void print(string text, bool inline = false) { print(text, defaultColor, inline); }
        public static void print(string text, ConsoleColor color, bool inline = false)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            if (inline) { Console.Write(text); }
            else { Console.WriteLine(text); }
            Console.ForegroundColor = oldColor;
        }

        public void resetColor()
        { Console.ForegroundColor = defaultColor; }

        public static int askInt()
        {
            int num;
            while (!int.TryParse(Console.ReadLine(), out num))
            { print("Input must be a valid Integer!", userError); }
            return num;
        }

        public static int askIntPositive() 
        {
            int num = askInt();
            if (num < 0)
            { print("Input must be a non-negative number", userError); return askIntPositive(); }
            return num;
        }

        public static int askIntRanged(int min, int max)
        {
            int num = askInt();
            if (num < min || num > max)
            { 
                print($"Input must be a number between {min} and {max}", userError); 
                return askIntRanged(min, max); 
            }
            return num;
        }
    }
}
