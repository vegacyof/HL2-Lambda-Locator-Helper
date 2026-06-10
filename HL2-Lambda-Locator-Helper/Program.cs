using System;
using System.Globalization;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;

namespace HL2LambdaLocatorHelper
{
    class Program
    {
        static void Main()
        {
            DisplayMenu();
        }

        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("HL2 Lambda Locator Helper");
            Console.WriteLine("\nEnter your hex string (For help, write \"?\")");
            string input = Console.ReadLine();
            InputHandler(input);
        }
        
        static void InputHandler(string input)
        {
            var pattern = new Regex(@"^(0x)[0-9A-Fa-f]{16}$");
            bool isHex = pattern.IsMatch(input);

            if (isHex == true)
            {
                HexHandler(input);
            }

            else
            {
                switch (input)
                {
                    case "":
                        Environment.Exit(0);
                        break;

                    case "?":
                        DisplayHelp();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("You did not enter a HEX string");
                        Console.ReadKey();
                        DisplayMenu();
                        break;
                }
            }
        }

        static void DisplayHelp()
        {
            Console.Clear();
            Console.WriteLine("Open the file gamestate.txt from the steamapps\\common\\Half-Life 2\\hl2_complete folder. \nFind the string \"id\" \"86\" in it. \nJust below, find \"data\" \"0x0000????????????\", Where is \"?\" - numbers and letters. \nCopy your value \"0x0000????????????\" without quotes. This will be your HEX string.\nPress any key to return");
            Console.ReadKey();
            DisplayMenu();
        }

        static void HexHandler(string hex)
        {
            long number = Convert.ToInt64(hex, 16);
            string reservedBinary = new string(Convert.ToString(number, 2).Reverse().ToArray());
            int i = 1;
            bool allCollected = !reservedBinary.Contains('0');

            if (allCollected == true)
            {
                Console.WriteLine("All collected!");
            }
            else
            {
                StringBuilder lambdas = new StringBuilder();
                foreach (var bit in reservedBinary)
                {
                    if (bit == '0')
                    {
                        lambdas.AppendLine($"{i}: Missing");
                    }

                    i++;
                }

                Console.WriteLine(lambdas);
            }

            Console.ReadKey();
            DisplayMenu();
        }
    }
}