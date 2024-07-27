using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_v2
{
    internal class Repository
    {
        #region Поля

        /// <summary>
        /// Количество заполненных элементов массива _workers
        /// </summary>
        private int _index;

        /// <summary>
        /// Заголовок файла, содержащий информацию о названиях полей Worker
        /// </summary>
        private string _title;

        /// <summary>
        /// Массив Worker, используемый для хранения записей работников
        /// </summary>
        private Worker[] _workers;

        /// <summary>
        /// Путь к текстовому файлу, используемому для хранения и загрузки записей
        /// </summary>
        private string _path;

        /// <summary>
        /// Символ разделитель, используется для сохранения и загрузки записей
        /// </summary>
        private char _splitChar = '#';

        #endregion

        #region Свойства

        /// <summary>
        /// Количество заполненных элементов массива _workers
        /// </summary>
        public int Index { get { return _index; } }

        /// <summary>
        /// Заголовок файла, содержащий информацию о названиях полей Worker
        /// </summary>
        public string Title { get { return _title; } }

        #endregion

        #region Конструктор и Деструктор

        /// <summary>
        /// Создает экземпляр класса, проверяет существует ли файл по указанному пути,
        /// если нет, то создает, если существует загружает информацию из файла
        /// </summary>
        /// <param name="Path"> Путь к файлу</param>
        public Repository(string Path)
        {
            _path = Path;
            _workers = new Worker[100];
            _index = 0;
            if (File.Exists(_path)) LoadRepository();
            else CreateEmptyRepository();
        }

        /// <summary>
        /// По завершении приложения, сохраняет информацию в файл
        /// </summary>
        ~Repository()
        {
            SaveRepository();
        }

        #endregion

        #region Методы

        /// <summary>
        /// Метод позволяющий создать новый файл репозитория, инициализирует поле _title и записывает его в файл 
        /// </summary>
        private void CreateEmptyRepository()
        {
            _title = $"{"ID",5} | " +
                    $"{"Дата и время добавления записи",30} | " +
                    $"{"Фамилия Имя Отчество",35} | " +
                    $"{"Возраст",7} | " +
                    $"{"Рост",4} | " +
                    $"{"Дата рождения",13} | " +
                    $"{"Место рождения",14}";
            using (StreamWriter writer = File.CreateText(_path))
            {
                writer.WriteLine(_title);
            }
        }

        /// <summary>
        /// Метод загружающий информацию из файла, происходит заполнение _workers записями сохраненными в файле 
        /// </summary>
        private void LoadRepository()
        {
            using (StreamReader reader = File.OpenText(_path))
            {
                _title= reader.ReadLine();
                while(!reader.EndOfStream)
                {
                    string[] arr = reader.ReadLine().Split(_splitChar);
                    
                    if (_workers.Length <= _index) Array.Resize(ref _workers, _workers.Length * 2);

                    _workers[_index] = new Worker( 
                        Convert.ToInt32(arr[0]), 
                        Convert.ToDateTime(arr[1]), 
                        arr[2], 
                        Convert.ToUInt32(arr[3]),
                        Convert.ToUInt32(arr[4]),
                        Convert.ToDateTime(arr[5]),
                        arr[6]);

                    _index++;
                }

            }
        }

        /// <summary>
        /// Метод сохраняющий записи _workers в файл, в формате строки в которой поля worker разделены splitChar
        /// </summary>
        private void SaveRepository()
        {
            using (StreamWriter writer = new StreamWriter(_path, false))
            {
                writer.WriteLine(_title);
                for (int i = 0; i < _index; i++)
                {
                    writer.WriteLine(
                        $"{_workers[i].Id}#" +
                        $"{_workers[i].CreatedDateTime}#" +
                        $"{_workers[i].FullName}#" +
                        $"{_workers[i].Age}#" +
                        $"{_workers[i].Height}#" +
                        $"{_workers[i].BirthDate}#" +
                        $"{_workers[i].BirthPlace}");
                }
            }
        }

        /// <summary>
        /// Метод производящий поиск Worker с указанным ID в _workers
        /// </summary>
        /// <param name="Id">ID работника</param>
        /// <returns>Индекс элемента массива _workers, который содержит указанный ID, если работник 
        /// с указанным ID небыл найден возвращает -1</returns>
        private int FindIndexOfWorkerById(int Id)
        {
            for (int i = 0; i < _index; i++)
            {
                if (_workers[i].Id == Id)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Метод позволяющий получить Worker, с указанным ID, из _workers
        /// </summary>
        /// <param name="Id">ID работника</param>
        /// <returns>Возвращает экземпляр Worker из _workers, c указанным ID, 
        /// если такого экземпляра несуществует в _workers возвращает новый экземпляр с инициализироваными по умолчанию значениями поолей</returns>
        public Worker GetWorkerById(int Id)
        {
            int index = FindIndexOfWorkerById(Id);
            if (index != -1) return _workers[index];
            else return new Worker();
        }

        /// <summary>
        /// Метод позволяющий получить массив Worker[] содержащий все не пустые элементы _workers
        /// </summary>
        /// <returns>Массив Worker[] содержащий все не пустые элементы _workers</returns>
        public Worker[] GetAllWorkers()
        {
            Worker[] workers = new Worker[_index];
            Array.Copy(_workers, workers, _index);
            return workers;
        }

        /// <summary>
        /// Метод позволяющий получить массив Worker[] состоящий из записей,
        /// добавленных в указанном временном промежутке [dateFrom, dateTo]
        /// </summary>
        /// <param name="dateFrom">Начало временного промещутка</param>
        /// <param name="dateTo">Конец временного промежутка</param>
        /// <param name="count">Возвращаемый параметр, если записи в указанном временном промежутке были найдены
        /// равен количеству найденных записей, если нет равен 0</param>
        /// <returns>Возвращаейт массив Worker[] содержащий записи удовлетворяющие указанному временному промежутку</returns>
        public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo, out int count)
        {
            Worker[] result = new Worker[10];
            count = 0;

            for (int i = 0; i < _index; i++)
            {
                if (_workers[i].CreatedDateTime>= dateFrom && _workers[i].CreatedDateTime <= dateTo)
                {
                    if (count>=result.Length) Array.Resize(ref result, result.Length*2);
                    result[count] = _workers[i];
                    count++;
                }
            }
            return result;
        }

        /// <summary>
        /// Метод позволяющий добавить Worker в репозиторий
        /// </summary>
        /// <param name="worker">Worker добавляемый в репозиторий</param>
        public void AddWorker(Worker worker)
        {
            if (_index >= _workers.Length) Array.Resize(ref _workers, _workers.Length * 2);
            _workers[_index] = worker;
            _index++;
        }

        /// <summary>
        /// Метод позволяющий удалить из репозитория Worker с указанным ID
        /// </summary>
        /// <param name="id">ID работника</param>
        /// <returns>Если работник был найден и удалён - возвращает true, иначе - false</returns>
        public bool DeleteWorkerById(int id)
        {
            int index = FindIndexOfWorkerById(id);
            if (index == -1) return false;
            else if(index == _index-1)
            {
                _index--;
                _workers[index] = new Worker();
            }
            else
            {
                _index--;
                for (int i = index; i < _index; i++)
                {
                    _workers[i] = _workers[i + 1];
                }
            }
            SaveRepository();
            return true;
        }

        /// <summary>
        /// Метод позволяет получить следующий ID работника, основываясь на ID последней записи в репозитории
        /// </summary>
        /// <returns>следующий ID</returns>
        public int GetNextId()
        {
            if (_index == 0) return 1;
            else return _workers[_index - 1].Id + 1;
        }

        #endregion


    }
}
