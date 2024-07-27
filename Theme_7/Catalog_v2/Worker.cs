using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_v2
{
    struct Worker
    {
        #region Поля
        //ID
        private int _id;

        //Дата и время добавления записи
        private DateTime _createdDateTime;

        //Ф.И.О.
        private string _fullName;

        //Возраст
        private uint _age;

        //Рост
        private uint _height;

        //Дата рождения
        private DateTime _birthDate;

        //Место рождения
        private string _birthPlace;

        #endregion

        #region Свойства

        /// <summary>
        /// ID сотрудника
        /// </summary>
        public int Id { get { return _id; } set { _id = value; } }

        /// <summary>
        /// Дата и время создания
        /// </summary>
        public DateTime CreatedDateTime { get { return _createdDateTime; } private set { _createdDateTime = value; } }

        /// <summary>
        /// Фамилия Имя Отчество
        /// </summary>
        public string FullName { get { return _fullName; } set { _fullName = value; } }  

        /// <summary>
        /// Возраст
        /// </summary>
        public uint Age { get { return _age; }  set { _age = value; } }

        /// <summary>
        /// Рост
        /// </summary>
        public uint Height { get { return _height;} set { _height = value; } }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get { return _birthDate; } set { _birthDate = value; } }

        /// <summary>
        /// Место рождения
        /// </summary>
        public string BirthPlace { get { return _birthPlace; } set { _birthPlace = value; } }

        #endregion

        #region Методы

        /// <summary>
        /// Метод позволяющий получить полную информацию о работнике, в формате строки
        /// </summary>
        /// <returns>Строку содержащую всю информацию о работнике</returns>
        public string Print()
        {
            return $"{_id,5} | {_createdDateTime,30} | {_fullName,35} | {_age,7} | {_height, 4} | {_birthDate.ToShortDateString(),13} | {_birthPlace, 14}";
        }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор позволяющий создать экземпляр структуры Worker.
        /// Поля _age и _createdDateTime - Заполняются автоматически, внутри метода. 
        /// </summary>
        /// <param name="ID">ID сотрудника</param>
        /// <param name="FullName">Фамилия Имя Отчество</param>
        /// <param name="Height">Рост </param>
        /// <param name="BirthDate">Дата рождения</param>
        /// <param name="BirthPlace">Место рождения</param>
        public Worker(int ID, string FullName, uint Height, DateTime BirthDate, string BirthPlace)
        {
            _id = ID;
            _fullName = FullName;
            _height = Height;
            _birthDate = BirthDate;
            _birthPlace = BirthPlace;
            _createdDateTime = DateTime.Now;
            _age = Convert.ToUInt32(((_createdDateTime - BirthDate).Days / 365));
        }

        /// <summary>
        /// Конструктор позволяющий создать экземпляр структуры Worker.
        /// </summary>
        /// <param name="ID">ID сотрудника</param>
        /// <param name="CreatedDateTime">Время создание записи сотрудника</param>
        /// <param name="FullName">Фамилия Имя Отчество</param>
        /// <param name="Age">Возраст</param>
        /// <param name="Height">Рост</param>
        /// <param name="BirthDate">Дата рождения</param>
        /// <param name="BirthPlace">Место рождения</param>
        public Worker(int ID, DateTime CreatedDateTime ,string FullName, uint Age ,uint Height, DateTime BirthDate, string BirthPlace)
        {
            _id = ID;
            _fullName = FullName;
            _height = Height;
            _birthDate = BirthDate;
            _birthPlace = BirthPlace;
            _createdDateTime = CreatedDateTime;
            _age = Age;
        }

        #endregion

    }
}
