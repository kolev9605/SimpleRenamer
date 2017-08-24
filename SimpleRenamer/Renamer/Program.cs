using System;
using System.Collections.Generic;
using System.Text;

namespace Renamer
{
    public class Program
    {
        private static Dictionary<string, string> CirilycToLatin = new Dictionary<string, string>()
        {
            { "а", "a"},
            { "б", "b"},
            { "в", "v"},
            { "г", "g"},
            { "д", "d"},
            { "е", "e"},
            { "ж", "j"},
            { "з", "z"},
            { "и", "i"},
            { "й", "i"},
            { "к", "k"},
            { "л", "l"},
            { "м", "m"},
            { "н", "n"},
            { "о", "o"},
            { "п", "p"},
            { "р", "r"},
            { "с", "s"},
            { "т", "t"},
            { "у", "u"},
            { "ф", "f"},
            { "х", "h"},
            { "ц", "c"},
            { "ч", "ch"},
            { "ш", "sh"},
            { "щ", "sht"},
            { "ъ", "u"},
            { "ь", ""},
            { "ю", "yu"},
            { "я", "q"},
        };

        public static void Main()
        {
            while (true)
            {
                string input = Console.ReadLine();
                string output = Rename(input);

                Console.WriteLine(output);
            }
        }

        private static string Rename(string input)
        {
            StringBuilder output = new StringBuilder();
            foreach (var letter in input)
            {
                bool isUppercase = char.IsUpper(letter);
                if (CirilycToLatin.ContainsKey(letter.ToString().ToLower()))
                {
                    if (isUppercase)
                    {
                        output.Append(CirilycToLatin[letter.ToString().ToLower()].ToUpper());
                    }
                    else
                    {
                        output.Append(CirilycToLatin[letter.ToString().ToLower()]);
                    }
                }
                else
                {
                    output.Append(letter.ToString());
                }                
            }

            return output.ToString();
        }
    }
}
