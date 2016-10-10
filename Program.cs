using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace lab_02
{
    public class Program
    {
        private static readonly Random RandomInstance = new Random();

        public static void Main(string[] args)
        {
            var output = "";
            var filePath = GetLongTextPath();
            PrintLongFileContents(filePath);

            var comment = ReadComment();
            Console.Clear();
            Console.WriteLine(output += "\n" + new string('-', 33));

            var everyWordCapitalized = ToTitleCase(comment);
            output += "\n" + "а. Кожне слово починається з великої літери " + everyWordCapitalized;
            PrintLastLine(output);

            var vowelsToRandomInts = ReplaceVowels(comment);
            output += "\n" + "b. Кожна голосна літера замінена рандомним числом від 0 до 9 " + vowelsToRandomInts;
            PrintLastLine(output);

            var doubledUpperCaseLetters = DoubleUpperCaseLetters(comment);
            output += "\n" + "с. Літери, які були введені користувачем в UpperCase здубльовані " +
                      doubledUpperCaseLetters;
            PrintLastLine(output);

            var withoutThirdWord = RemoveThirdWord(comment);
            output += "\n" + "d Третє слово видалене " + withoutThirdWord;
            PrintLastLine(output);

            var withMiracle = InsertMiracle(comment);
            output += "\n" + "e Між п`ятим і шостим словом вставлене слово - miracle " + withMiracle;
            PrintLastLine(output);

            var joined78 = Join78(comment);
            output += "\n" + "f 7 i 8 слово об'єднанні разом " + joined78;
            PrintLastLine(output);

            output += "\n" + new string('*', 5);
            PrintLastLine(output);

            output += "\n" + new string('=', 10) + "Статистика" + new string('=', 10);
            PrintLastLine(output);

            var statistics = BuildCommentStatistic(comment);
            output += "\n" + statistics;
            PrintLastLine(output);

            File.WriteAllText(filePath, output);
        }

        private static void PrintLastLine(string text)
        {
            Console.WriteLine(text.Split('\n').Last());
        }

        private static string BuildCommentStatistic(string comment)
        {
            var statsBuilder = new StringBuilder();

            statsBuilder.AppendLine($"{comment.Count(char.IsDigit)} - цифр");
            statsBuilder.AppendLine($"{comment.Count(char.IsLetter)} - букв");
            statsBuilder.AppendLine($"{comment.Count(char.IsLetterOrDigit)} - букв і цифр");
            statsBuilder.AppendLine($"{comment.Count(char.IsLower)} - символів в LowerCase");
            statsBuilder.AppendLine($"{comment.Count(char.IsUpper)} - символів в UpperCase");
            statsBuilder.AppendLine($"{comment.Count(char.IsPunctuation)} - розділових знаків");
            statsBuilder.AppendLine($"{comment.Count(char.IsSeparator)} - знаків розділювачів");
            statsBuilder.AppendLine($"{Math.Min(comment.IndexOf('a'), comment.IndexOf('A'))} - index of A|a");
            statsBuilder.AppendLine($"Vote index - {comment.LastIndexOfAny(new[] {'a', 'e', 'i', 'o', 'u'})} - "
                                    + "порядковий номер голосної з кінця оригінального тексту");

            return statsBuilder.ToString();
        }

        private static void PrintLongFileContents(string filePath)
        {
            var fileContent = File.ReadLines(filePath);

            Console.WriteLine("File content: ");
            foreach (var line in fileContent)
            {
                Console.WriteLine(line);
            }
        }

        private static string GetLongTextPath()
        {
            Console.WriteLine("Please enter path to LongText.txt");
            return Path.Combine(Console.ReadLine(), "LongText.txt");
        }

        private static string ReadComment()
        {
            var comment = "";

            Console.WriteLine("Please enter your comment");
            while (true)
            {
                var line = Console.ReadLine();
                if (line != null && line.Length == 0)
                {
                    continue;
                }
                var pressedCtrlE = line != null && line.ToCharArray()[0] == 5;
                if (pressedCtrlE)
                {
                    break;
                }
                comment += line + '\n';
            }

            return comment;
        }

        private static string ToTitleCase(string text)
        {
            return Regex.Replace(text, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
        }

        private static int GetRandomInt()
        {
            return RandomInstance.Next(0, 9);
        }

        private static string ReplaceVowels(string text)
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

        private static string DoubleUpperCaseLetters(string text)
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

        private static string RemoveThirdWord(string text)
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

        private static string InsertMiracle(string text)
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

        private static string Join78(string text)
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