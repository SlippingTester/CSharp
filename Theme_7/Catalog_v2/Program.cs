using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog_v2
{
    internal class Program
    {
        private static string fileName = "data.txt";
        static void Main(string[] args)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            Repository rep = new Repository(path);
            Console.WriteLine("Добро пожаловать в каталог сотрудников!");
            while (true)
            {
                Console.WriteLine("Пожалуйста, выберите действие:\n" +
                    "\t1.Показать все записи в каталоге\n" +
                    "\t2.Отсортировать все записи в каталоге и показать\n" +
                    "\t3.Показать записи за указанный диапазон времени\n" +
                    "\t4.Поиск сотрудника по Id\n" +
                    "\t5.Добавить нового сотрудника в каталог\n" +
                    "\t6.Сгенерировать случайных работников и добавить в каталог\n" +
                    "\t7.Удалить работника по Id\n"+
                    "\t8.Выйти\n"
                    );

                if (Int32.TryParse(Console.ReadLine(), out int input))
                {
                    Console.Clear();
                    switch (input)
                    {
                        case 1:
                            PrintRepository(rep);
                            break;
                        case 2:
                            SortAndPrintAllWorkers(rep);
                            break;
                        case 3:
                            PrintWorkersByPeriod(rep); 
                            break;
                        case 4:
                            PrintWorkerById(rep);
                            break;
                        case 5:
                            if (CreateWorker(rep)) Console.WriteLine("Успешно!");
                            break;
                        case 6:
                            if(CreateRandomWorker(rep)) Console.WriteLine("Успешно!");
                            break;
                        case 7:
                            Console.Write("Введите ID сотрудника: ");
                            if (rep.DeleteWorkerById(Convert.ToInt32(Console.ReadLine())))
                            {
                                Console.WriteLine("Успешно!");
                            }
                            else Console.WriteLine("Работника с таким Id нет!");
                            break;
                        case 8:
                            return;
                    }
                    Console.WriteLine("Для возврата нажмите любую клавишу");
                    Console.ReadKey();
                    Console.Clear();
                }
            }


        }

        /// <summary>
        /// Метод позволяющий добавить нового работника в Repository.
        /// Данные работника заполняет пользователь, чтение данных производится из консоли.
        /// </summary>
        /// <param name="repository">Экземпляр класса Repository, в который производится добавление созданного работника</param>
        /// <returns>Возвращает true - если были указанны коректные данные работника, иначе - false</returns>
        private static bool CreateWorker(Repository repository)
        {
            string[] fields = { "Фамилия Имя Отчество: ", "Рост: ", "Дата рождения: ", "Место рождения: " };

            string[] input = new string[fields.Length];
            Console.WriteLine("Добавление нового сотрудника");
            for (int i = 0; i < fields.Length; i++)
            {
                Console.Write($"{fields[i],30}");
                input[i] = Console.ReadLine();
            }

            if (UInt32.TryParse(input[1],out uint height) && DateTime.TryParse(input[2], out DateTime birthDate))
            {
                repository.AddWorker( new Worker(repository.GetNextId(), input[0], height, birthDate, input[3]));
                return true;
            }
            else
            {
                Console.WriteLine("Введены некорректные значения!");
                return false;
            }

        }

        /// <summary>
        /// Метод позволяющий добавить в репозиторий желаемое количество случайно сгенерированных работников.
        /// Чтение числа работников производится внутри метода 
        /// </summary>
        /// <param name="repository">Экземпляр класса Repository, в который производится добавление сгенерированных работников</param>
        /// <returns>Возвращает true - если было указанно коректное количество добавляемых работников, иначе - false</returns>
        private static bool CreateRandomWorker(Repository repository)
        {
            Console.Write("Введите желаемое количество добавляемых сотрудников: ");
            if (Int32.TryParse(Console.ReadLine(), out int count))
            {
                string[] surnames = { "Иванов", "Петров", "Сидоров", "Кузнецов",
                                  "Смирнов", "Попов", "Лебедев", "Ковалев",
                                  "Новиков", "Морозов", "Соловьев", "Васильев",
                                  "Зайцев", "Тихонов", "Белов", "Григорьев",
                                  "Михайлов", "Федоров", "Сергеев", "Комаров"
            };
                string[] names = { "Александр", "Дмитрий", "Сергей", "Анатолий",
                               "Иван", "Максим", "Николай", "Евгений",
                               "Алексей", "Роман", "Владимир", "Антон",
                               "Тимур", "Станислав", "Денис", "Павел",
                               "Виктор", "Михаил", "Игорь", "Артем"
            };
                string[] middleNames = { "Иванович", "Петрович", "Сидорович", "Александрович",
                                    "Дмитриевич", "Сергеевич", "Анатольевич", "Максимович",
                                    "Николаевич", "Евгеньевич", "Алексеевич", "Романович",
                                    "Владимирович", "Антонович", "Тимурович", "Станиславович",
                                    "Денисович", "Павлович", "Викторович", "Михайлович"
            };
                string[] city = { "Москва", "Санкт-Петербург", "Новосибирск", "Екатеринбург",
                              "Нижний Новгород", "Казань", "Челябинск", "Омск",
                              "Самара", "Ростов-на-Дону", "Уфа", "Красноярск",
                              "Воронеж", "Пермь", "Волгоград", "Тюмень",
                              "Ижевск", "Барнаул", "Калуга", "Саратов"
            };
                Random rnd = new Random();

                for (int i = 0; i < count; i++)
                {
                    repository.AddWorker(new Worker(repository.GetNextId(),
                        $"{surnames[rnd.Next(surnames.Length)]} {names[rnd.Next(names.Length)]} {middleNames[rnd.Next(middleNames.Length)]}",
                        (uint)rnd.Next(150, 210),
                        Convert.ToDateTime($"{rnd.Next(1, 28)}.{rnd.Next(1, 12)}.{rnd.Next(1970, 2006)}"),
                        city[rnd.Next(city.Length)]));
                }
                return true;
            }
            else Console.WriteLine("Введено некорректное значение!");
            return false;

        }

        /// <summary>
        /// Метод позволяющий вывести информацию о работниках добавленных в репозиторий за указанный период.
        /// Чтение временного интервала из консоли производится внутри метода
        /// </summary>
        /// <param name="repository">Экземпляр класса Repository, в котором производится поиск сотрудников добавленных за указанный пероиод</param>
        private static void PrintWorkersByPeriod(Repository repository)
        {
            Console.Write("Пожалуйста введите дату начала временного периода: ");
            string firstInput = Console.ReadLine();
            Console.Write("Пожалуйста введите дату конца временного периода: ");
            string secondInput = Console.ReadLine();

            if(DateTime.TryParse(firstInput,out DateTime dateFrom) && DateTime.TryParse(secondInput, out DateTime dateTo))
            { 
                Worker[] workers = repository.GetWorkersBetweenTwoDates(dateFrom, dateTo, out int count);
                if(count == 0)
            {
                Console.WriteLine("За указанный временной промежуток записи не обнаружены!");
                return;
            }
                Console.WriteLine($"Найдено {count+1} записей: \n{repository.Title}");
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine(workers[i].Print());
                }
            }
            else Console.WriteLine("Введены некорректные значения временного промежутка!");
        }

        /// <summary>
        /// Метод позволяющий вывести на экран консоли информацию о всех работниках содержащихся в переданном репозитории
        /// </summary>
        /// <param name="repository">Экземпляр класса Repository из которого производится чтение информации о записаных работниках</param>
        private static void PrintRepository(Repository repository)
        {
            Console.WriteLine(repository.Title);
            for (int i = 0; i < repository.Title.Length; i++) Console.Write("-");
            Console.WriteLine();

            Worker[] workers = repository.GetAllWorkers();
            foreach(Worker w in workers)
            {
                Console.WriteLine(w.Print());
            }
        }

        /// <summary>
        /// Метод позволяющий вывести на экран консоли данные работника по его ID.
        /// Считывание ID из консоли производится внутри метода.
        /// </summary>
        /// <param name="repository">Экземпляр класса Repository, в котором производится поиск сотрудника с указанным ID</param>
        public static void PrintWorkerById(Repository repository)
        {
            Console.Write("Введите ID сотрудника: ");
            if (Int32.TryParse(Console.ReadLine(), out int id))
            {
                Worker w = repository.GetWorkerById(id);
                if (w.Id != 0) Console.WriteLine($"{repository.Title}\n{w.Print()}");
                else Console.WriteLine("Такого работника нет!");
            }
            else Console.WriteLine("Введено некорректное значение ID!");
        }

        /// <summary>
        /// Метод позволяющий вывести список работников на экран консоли, предварительно отсортировав его по выбраному параметру
        /// </summary>
        /// <param name="repository"> Экземпляр класса Repository, из которого будет взят массив Workers[], для последующих сортировки и вывода</param>
        private static void SortAndPrintAllWorkers(Repository repository)
        {
            Console.WriteLine("Выберите параметр для сортировки:\n " +
                    "\t1.ID\n" +
                    "\t2.Фамилия имя отчество\n" +
                    "\t3.Дата добавления записи\n" +
                    "\t4.Рост\n" +
                    "\t5.Дата рождения\n" +
                    "\t6.Возраст\n" +
                    "\t7.Место рождения\n"
                    );
            if (Int32.TryParse(Console.ReadLine(), out int result))
            {
                Worker[] workers = repository.GetAllWorkers();
                var orderdWorkers = workers.OrderBy(w => w.Id); ;
                switch (result)
                {
                    case 2:
                        orderdWorkers = workers.OrderBy(w => w.FullName);
                        break;
                    case 3:
                        orderdWorkers = workers.OrderBy(w => w.CreatedDateTime);
                        break;
                    case 4:
                        orderdWorkers = workers.OrderBy(w => w.Height);
                        break;
                    case 5:
                        orderdWorkers = workers.OrderBy(w => w.BirthDate);
                        break;
                    case 6:
                        orderdWorkers = workers.OrderBy(w => w.Age);
                        break;
                    case 7:
                        orderdWorkers = workers.OrderBy(w => w.BirthPlace);
                        break;
                }
                Console.WriteLine(repository.Title);

                for (int i = 0; i < repository.Title.Length; i++) Console.Write("-");
                Console.WriteLine();
                foreach (var w in orderdWorkers)
                {
                    Console.WriteLine(w.Print());
                }
            }
            else Console.WriteLine("Введено некорректное значение");
        }
    }
}
