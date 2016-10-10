using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace lab_02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var output = "";
            var filePath = GetLongTextPath();
            PrintLongFileContents(filePath);

            var comment = ReadComment();
            Console.Clear();
            Console.WriteLine(output += "\n" + new string('-', 33));

            var everyWordCapitalized = TextTransform.ToTitleCase(comment);
            output += "\n" + "а. Кожне слово починається з великої літери " + everyWordCapitalized;
            PrintLastLine(output);

            var vowelsToRandomInts = TextTransform.ReplaceVowels(comment);
            output += "\n" + "b. Кожна голосна літера замінена рандомним числом від 0 до 9 " + vowelsToRandomInts;
            PrintLastLine(output);

            var doubledUpperCaseLetters = TextTransform.DoubleUpperCaseLetters(comment);
            output += "\n" + "с. Літери, які були введені користувачем в UpperCase здубльовані " +
                      doubledUpperCaseLetters;
            PrintLastLine(output);

            var withoutThirdWord = TextTransform.RemoveThirdWord(comment);
            output += "\n" + "d Третє слово видалене " + withoutThirdWord;
            PrintLastLine(output);

            var withMiracle = TextTransform.InsertMiracle(comment);
            output += "\n" + "e Між п`ятим і шостим словом вставлене слово - miracle " + withMiracle;
            PrintLastLine(output);

            var joined78 = TextTransform.Join78(comment);
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
    }
}