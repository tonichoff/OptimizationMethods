using System;

using MathNet.Numerics.LinearAlgebra;

namespace Methods
{
    class Program
    {
        const double Error = 10e-5;
        const double MaxSteps = 500;

        static double Function(Vector<double> x, Matrix<double> A, Vector<double> b)
        {
            return 0.5 * (x.ToRowMatrix() * A * x)[0] + (b.ToRowMatrix() * x)[0];
        }

        static Vector<double> FD(Vector<double> x, Matrix<double> A, Vector<double> b)
        {
            return 0.5 * ((A + A.Transpose()) * x) + b;
        }

        static Matrix<double> SD(Matrix<double> A)
        {
            return 0.5 * (A + A.Transpose());
        }

        static Vector<double> NewtonMethod(Vector<double> x0, Matrix<double> A, Vector<double> b)
        {
            var x = x0;
            for (int i = 0; i < MaxSteps; ++i)
            {
                var xk = x - SD(A).Inverse() * FD(x, A, b);
                if ((xk - x).L2Norm() < Error)
                {
                    Console.WriteLine($"Newton method stoped work on {i} iteration");
                    return xk;
                }
                xk.CopyTo(x);
            }
            Console.WriteLine($"Newton method stoped work on {MaxSteps - 1} iteration");
            return x;
        }

        static Vector<double> GradientMethod(Vector<double> x0, Matrix<double> A, Vector<double> b)
        {
            var x = x0;
            var l = 10e-4;
            for (int i = 0; i < MaxSteps; ++i)
            {
                var xk = x - l * FD(x, A, b);
                if ((xk - x).L2Norm() < Error)
                {
                    Console.WriteLine($"Gradient method stoped work on {i} iteration");
                    return xk;
                }
                xk.CopyTo(x);
            }
            Console.WriteLine($"Gradient method stoped work on {MaxSteps - 1} iteration");
            return x;
        }

        static void Main(string[] args)
        {
            var n = 6;
            var x = Vector<double>.Build.Dense(n, 1);
            var A = Matrix<double>.Build.RandomPositiveDefinite(n);
            var b = Vector<double>.Build.Random(n);

            Console.WriteLine(NewtonMethod(x, A, b));
            Console.WriteLine(GradientMethod(x, A, b));

            Console.ReadKey();
        }
    }
}
