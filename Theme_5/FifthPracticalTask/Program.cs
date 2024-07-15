using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifthPracticalTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите текст: ");
            string text = Console.ReadLine();
            string[] split = SplitText(text);
            PrintStringArray(split);
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

            for (int i = 0, j = 0 ; i < text.Length && j != wordCount; i++, j++)
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
        /// Выводит на экран консоли элементы массива,каждый с новой строки
        /// </summary>
        /// <param name="arr">Массив строк выводимый в консоль</param>
        static void PrintStringArray(string[] arr)
        {
            if (arr.Length == 0)
            {
                Console.WriteLine("Вы ничего не ввели!");
                return;
            }
            foreach (string str in arr)
            {
                Console.WriteLine(str);
            }
        }
    }
}
