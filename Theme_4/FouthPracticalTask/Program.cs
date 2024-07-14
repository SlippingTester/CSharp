using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FouthPracticalTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region FirstPoint
            Console.Write("Введите количество строк: ");
            int lines = Int32.Parse(Console.ReadLine());
            Console.Write("Введите количество столбцов: ");
            int columns = Int32.Parse(Console.ReadLine());
            
            int[,] matrixA = new int[lines, columns];
            Random rnd = new Random();
            int sum = 0;

            Console.WriteLine("Матрица А");
            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrixA[i, j] = rnd.Next(-100,100);
                    sum += matrixA[i, j];
                    Console.Write($"{matrixA[i,j]}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Сумма всех элементов матрицы: {sum}\n");

            #endregion

            #region SecondPoint

            int[,] matrixB = new int[lines, columns];

            Console.WriteLine("Матрица В");
            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrixB[i, j] = rnd.Next(-100, 100);
                    Console.Write($"{matrixB[i, j]}\t");
                }
                Console.WriteLine();
            }

            int[,] matrixC = new int[lines, columns];

            Console.WriteLine("\nМатрица C - сумма матриц А и В");
            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrixC[i, j] = matrixA[i, j]+matrixB[i,j];
                    Console.Write($"{matrixC[i, j]}\t");
                }
                Console.WriteLine();
            }


            #endregion
        }
    }
}
