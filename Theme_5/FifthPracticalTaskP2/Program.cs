using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifthPracticalTaskP2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите текст: ");
            string text = Console.ReadLine();
            Console.WriteLine(ReversWords(text));
        }

        /// <summary>
        /// Разделяет text на массив string, разделителем является пробельный символ.
        /// </summary>
        /// <param name="text"> Разделяемая строка</param>
        /// <returns> Возвращает полученный в результате деления массив строк</returns>
        static string[] SplitText(string text)
        {
            int wordCount = 0;
            foreach (char c in text)
            {
                if (c == ' ') wordCount++;
            }
            wordCount = wordCount == text.Length ? 0 : wordCount + 1;

            string[] result = new string[wordCount];

            for (int i = 0, j = 0; i < text.Length && j != wordCount; i++, j++)
            {
                string temp = "";
                while (i < text.Length && text[i] != ' ')
                {
                    temp += text[i];
                    i++;
                }
                result[j] = temp;
            }
            return result;
        }

        /// <summary>
        /// Переставляет слова в строке в обратном порядке
        /// </summary>
        /// <param name="inputPhrase"> Входная строка</param>
        /// <returns>Измененная строка</returns>
        static string ReversWords(string inputPhrase)
        {
            string[] arr = SplitText(inputPhrase);
            string result = "";
            for (int i = 1; i <= arr.Length; i++)
            {
                result += arr[arr.Length - i] + " ";
            }

            return result;
        }
    }
}
