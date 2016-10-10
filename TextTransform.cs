using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace lab_02
{
    public class TextTransform
    {
        private static readonly Random RandomInstance = new Random();

        public static string ToTitleCase(string text)
        {
            return Regex.Replace(text, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
        }

        public static int GetRandomInt()
        {
            return RandomInstance.Next(0, 9);
        }

        public static string ReplaceVowels(string text)
        {
            var characters = text.ToCharArray();
            for (var i = 0; i < characters.Length; i++)
            {
                var c = characters[i];
                if ("aeiou".Contains(char.ToLower(c)))
                {
                    characters[i] = GetRandomInt().ToString().ToCharArray()[0];
                }
            }
            return new string(characters);
        }

        public static string DoubleUpperCaseLetters(string text)
        {
            var characters = text.ToCharArray().Select(ch => ch.ToString()).ToArray();

            for (var i = 0; i < characters.Length; i++)
            {
                var c = characters[i];
                if (char.IsUpper(c.ToCharArray()[0]))
                {
                    characters[i] = c + c;
                }
            }

            return string.Join("", characters);
        }

        public static string RemoveThirdWord(string text)
        {
            var words = text.Split(' ');
            for (var i = 0; i < words.Length; i++)
            {
                if (i == 2)
                {
                    words[i] = "";
                }
            }

            return string.Join(" ", words);
        }

        public static string InsertMiracle(string text)
        {
            var words = text.Split(' ');
            for (var i = 0; i < words.Length; i++)
            {
                if (i == 4)
                {
                    words[i] = words[i] + " miracle ";
                }
            }

            return string.Join(" ", words);
        }

        public static string Join78(string text)
        {
            var words = text.Split(' ');
            for (var i = 0; i < words.Length; i++)
            {
                if (i == 6 && words.Length > 6)
                {
                    words[i] = string.Join("Joined", words[i], words[i + 1]);
                }
            }

            return string.Join(" ", words);
        }
    }
}