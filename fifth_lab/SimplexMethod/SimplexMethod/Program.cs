using System;

namespace SimplexMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            var table = new double[,] { {25, -3,  5},
                                        {30, -2,  5},
                                        {10,  1,  0},
                                        { 6,  3, -8},
                                        { 0, -6, -5} };
            var result = new double[2];
            var method = new SimplexMethod(table);
            var tableResult = method.Calculate(result);
            Console.WriteLine("Solution table:");
            for (var i = 0; i < tableResult.GetLength(0); i++)
            {
                for (var j = 0; j < tableResult.GetLength(1); j++)
                {
                    Console.Write(tableResult[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Solution:");
            Console.WriteLine("X[1] = " + result[0]);
            Console.WriteLine("X[2] = " + result[1]);
            Console.ReadLine();
        }
    }
}
