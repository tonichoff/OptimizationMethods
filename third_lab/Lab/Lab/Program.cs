using System;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace Lab
{
    class Program
    {
        static void Define(int deminsion, out Matrix<double> A, out Vector<double> b)
        {
            A = Matrix<double>.Build.DenseOfArray(new double[,] {
                {8.6303, 1.6692, 1.6716, 1.8487, 2.1649, 1.7010},
                {1.6692, 7.5939, 1.2113, 1.3713, 1.5321, 1.0893},
                {1.6716, 1.2113, 8.1740, 1.3232, 1.5848, 1.1371},
                {1.8487, 1.3713, 1.3232, 7.4952, 1.7708, 1.3272},
                {2.1649, 1.5321, 1.5848, 1.7708, 8.5094, 1.6433},
                {1.7010, 1.0893, 1.1371, 1.3272, 1.6433, 7.5422}
            });
            b = Vector<double>.Build.DenseOfArray(new double[] {
                 0.6160, 0.4733, 0.3517, 0.8308, 0.5853, 0.5497
            });
        }

        static void DefineRandom(int deminsion, out Matrix<double> A, out Vector<double> b)
        {
            A = Matrix<double>.Build.RandomPositiveDefinite(deminsion);
            b = Vector<double>.Build.Random(deminsion);
        }

        static void Main(string[] args)
        {
            var deminsion = 6;
            var randomMode = false;
            Matrix<double> A = null;
            Vector<double> b = null;
            if (randomMode)
            {
                DefineRandom(deminsion, out A, out b);
            }
            else
            {
                Define(deminsion, out A, out b);
            }
            var random = new Random();
            var x0 = Vector<double>.Build.Random(deminsion);
            var r = random.Next(2, 100) / 10;
            var x = (-1) * A.Inverse() * b;
            var f = (x - x0).L2Norm() - r;
            if (f <= 0)
            {
                Console.WriteLine("f(x0) <= 0");
                Console.WriteLine(x0);
            }
            else
            {
                //var y = (d - c.ToRowMatrix() * A.Inverse() * b) / (c.ToRowMatrix() * A.Inverse() * c);
                //var x = (-1) * A.Inverse() * (b + y[0] * c);
                Console.WriteLine(x);
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}

