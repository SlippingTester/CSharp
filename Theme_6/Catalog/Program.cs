using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Catalog
{
    internal class Program
    {
        private static string fileName = "db.txt";
        private static string[] fieldName = {
            "ID:",
            "Время и дата записи:",
            "ФИО:",
            "Возраст:",
            "Рост:",
            "Дата рождения:",
            "Место рождения:"};
        private static char splitSymbol = '#';

        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в базу данных сотрудников!\n");
            while(true)
            {
                Console.WriteLine("Выберите действие: " +
                    "\n\t1. Показать базу данных сотрудников" +
                    "\n\t2. Внести данные сотрудника" +
                    "\n\t3. Закрыть приложение");
                if(Int32.TryParse(Console.ReadLine(), out int result))
                {
                    switch(result) 
                    {
                        case 1:
                            PrintDataBase();
                            Console.WriteLine("\nДля возврата нажмите на любую клавишу");
                            Console.ReadKey();
                            break;
                        case 2:
                            FillDataBase();
                            Console.WriteLine("\nДля возврата нажмите на любую клавишу");
                            Console.ReadKey();
                            break;
                        case 3:
                            return;
                    
                    }  
                }
                Console.Clear();
            }

        }

        /// <summary>
        /// Метод позволяющий внести нового сотрудника в базу данных
        /// </summary>
        static void FillDataBase()
        {
            string[] data = new string[fieldName.Length];
            
            Console.Clear();

            data[0] = $"{GetNextId()}";
            data[1] = DateTime.Now.ToString();

            Console.WriteLine("Введите данные сотрудника\n");
            for (int i = 0; i < fieldName.Length; i++)
            {
                if (i != 0 && i != 1)
                {
                    Console.Write($"{fieldName[i]} ");
                    data[i] = $"{Console.ReadLine()}";
                }
                else
                {
                    Console.WriteLine($"{fieldName[i]} {data[i]}");
                }
            }

            if (!File.Exists(fileName)) File.Create(fileName).Close();

            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if (i != data.Length - 1) writer.Write($"{data[i]}{splitSymbol}");
                    else writer.WriteLine($"{data[i]}");
                }
            }

            Console.WriteLine("Данные успешно занесены!");
        }

        /// <summary>
        /// Метод для вывода информации о сотрудниках из базы данных на экран консоли
        /// </summary>
        static void PrintDataBase()
        {
            if (!File.Exists(fileName)) File.Create(fileName).Close();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;

                Console.Clear();
                Console.WriteLine("Данные сотрудников");
                while ((line  = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(splitSymbol);
                    for (int i = 0; i < data.Length; i++)
                    {
                        Console.WriteLine($"{fieldName[i]} {data[i]}");
                    }
                    Console.WriteLine();
                }
            }

        }

        /// <summary>
        /// Метод определяющий новый ID сотрудника, на основании количества сотрудников внесенных в базу данных
        /// </summary>
        /// <returns>Число - ID для новой записи</returns>
        static int GetNextId()
        {
            int id = 1;
            using (StreamReader reader = new StreamReader(fileName))
            {
                while (reader.ReadLine() != null )
                {
                    id++;
                }
            }
            return id;
        }
    }
}
