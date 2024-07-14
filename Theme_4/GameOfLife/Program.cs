using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Program
    {

        /* Символы для вывода изображения в консоль
         * blackBox - пустой симво, клетка не жива
         * whiteBox - белый сплошной символ, клетка жива
         */
        private static char blackBox = ' ';
        private static char whiteBox = '\x2588';

        // Размеры игрового поля и окна консоли
        private static int height = 60;
        private static int width = 180;

        static void Main(string[] args)
        {            
            //Устанавливаем размеры окна и буфера консоли, отключаем видимость курсора
            Console.SetWindowSize(width, height+1);
            Console.SetBufferSize(width, height+1);
            Console.CursorVisible = false;
            
            //Объявляем и инициализируем двумерный массив, который будет выполнять роль игрвого поля
            //Использован булевый массив для уменьшения затрат памяти
            bool[,] field = new bool[height, width];

            FillField(field, 2000);

            while (true)
            {
                UpdateField(ref field);
                PrintField(field);

                if(Console.KeyAvailable)
                {
                    break;
                }
            }

        }

        /// <summary>
        ///	Заполняет поле живыми клетками в случайом месте.
        /// Заполняет двумерный массив field значениями true, позиции выбираются случайным образом
        /// </summary>
        /// <param name="field"> Заполняемый двумерный массив.</param>
        /// <param name="startCellNum"> Количество вставляемых значений.</param>
        private static void FillField(bool[,] field, int startCellNum)
        {
            Random random = new Random();

            for (int i = 0; i < startCellNum; i++)
            {
                int line = random.Next(0, height);
                int column = random.Next(0, width);
                if (field[line, column])
                {
                    i--;
                    continue;
                }
                else field[line, column] = true;
            }
        }


        /// <summary>
        ///	Выводит игровое поле на экран консоли
        /// </summary>
        /// <param name="field"> Двумерный массив.</param>
        private static void PrintField(bool[,] field)
        {
            
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    Console.Write(field[i,j] ? whiteBox:blackBox);
                }
                
            }

        }


        /// <summary>
        ///	Обновляет состояние поля на один кадр. Проверяет состояние каждой клетки, 
        ///	вызывая Функцию IsCellLive, и изменяет состояние согласно правилам
        /// </summary>
        /// <param name="field"> Игрвое поле.</param>
        private static void UpdateField(ref bool[,] field)
        {
            bool[,] newField = new bool[field.GetLength(0), field.GetLength(1)];

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (IsCellLive(field, i, j)) newField[i, j] = true;
                    else newField[i, j] = false;
                }
            }
            field = newField;
        }

        /// <summary>
        ///	Проверяет состояние текущей игровой ячейки.
        ///	Если в ячейки должна родиться или остаться клетка - возвращает true;
        ///	Если в ячейки клетка умирает или не рождается - возвращает false;
        /// </summary>
        /// <param name="field"> Игрвое поле.</param>
        /// <param name="line"> Номер строки, в которой находится проверяемая ячейка.</param>
        /// <param name="column"> Номер столбца, в котором находится проверяемая ячейка.</param>
        /// <returns> Результат проверки.</returns>
        private static bool IsCellLive(bool[,] field, int line, int column)
        {
            int num = LiveNeighborCellsNum(field,line,column);
            if (field[line, column]) return (num == 3 || num == 2);
            else return num == 3 ;
        }

        /// <summary>
        ///	Вычисляет количество живых клеток вокруг текущей ячейки.
        /// </summary>
        /// <param name="field"> Игрвое поле.</param>
        /// <param name="line"> Номер строки, в которой находится проверяемая ячейка.</param>
        /// <param name="column"> Номер столбца, в котором находится проверяемая ячейка.</param>
        /// <returns> Число живых клеток вокруг текущей ячейки.</returns>
        private static int LiveNeighborCellsNum(bool[,] field, int line, int column)
        {
            int count = 0;
            int rightIndx = column != field.GetLength(1)-1 ? column+1 : column;
            int leftIndx = column != 0 ? column-1 : column;
            int topIndx = line != 0 ? line-1 : line;
            int bottomIndx = line != field.GetLength(0)-1 ? line+1 : line;

            for (int i = topIndx; i <= bottomIndx; i++)
            {
                for (int j = leftIndx; j <= rightIndx; j++)
                {
                    if (i == line && j == column) continue;
                    if (field[i, j]) count++;
                }
            }

            return count;
        }
    }
}
