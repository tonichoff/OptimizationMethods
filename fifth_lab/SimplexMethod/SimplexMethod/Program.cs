using System;

namespace SimplexMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] table = { {25, -3,  5},
                                {30, -2,  5},
                                {10,  1,  0},
                                { 6,  3, -8},
                                { 0, -6, -5} };
            double[] result = new double[2];
            double[,] tableResult;
            SimplexMethod method = new SimplexMethod(table);
            tableResult = method.Calculate(result);
            Console.WriteLine("Решенная симплекс-таблица:");
            for (int i = 0; i < tableResult.GetLength(0); i++)
            {
                for (int j = 0; j < tableResult.GetLength(1); j++)
                {
                    Console.Write(tableResult[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Решение:");
            Console.WriteLine("X[1] = " + result[0]);
            Console.WriteLine("X[2] = " + result[1]);
            Console.ReadLine();
        }
    }
}
