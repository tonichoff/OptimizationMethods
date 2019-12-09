using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace lab1
{
    class Program
    {
        const double Error = 10e-5;
        const double MaxSteps = 100;
        const int n = 6;
        static readonly Vector<double> EV = Vector<double>.Build.Dense(n, Error);

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
            var xk = x - SD(A).Inverse() * FD(x, A, b);
            xk.CopyTo(x);
            return x;
        }

        static bool Less(Vector<double> a, Vector<double> b)
        {
            for (var i = 0; i < n; ++i)
            {
                if (a[i] < b[i])
                {
                    return true;
                }
                else if (a[i] > b[i])
                {
                    return false;
                }
            }
            return false;
        }

        static Vector<double> ABS(Vector<double> a)
        {
            for (var i = 0; i < n; ++i)
            {
                a[i] = Math.Abs(a[i]);
            }
            return a;
        }

        static double ModGrad(Vector<double> x)
        {
            var result = 0.0;
            foreach (var xk in x)
            {
                result += xk * xk;
            }
            return Math.Sqrt(result);
        }

        static Vector<double> GradientMethod(Vector<double> x0, Matrix<double> A, Vector<double> b)
        {
            var x = x0;
            double l = ((double)1 / 60);
            for (int i = 0; i < MaxSteps; ++i)
            {
                var fd = FD(x, A, b);
                var xk = x - l * fd;
                if ((xk - x).L2Norm() <= Error)
                {
                    Console.WriteLine($"Gradient method stoped work on {i + 1} iteration");
                    return xk;
                }
                Console.WriteLine($"Gradient. Step {i + 1}. x = {xk}");
                xk.CopyTo(x);
            }
            Console.WriteLine($"Gradient method stoped work on {MaxSteps} iteration");
            return x;
        }

        static void Main(string[] args)
        {
            var x = Vector<double>.Build.Dense(n, 1);
            var A = Matrix<double>.Build.RandomPositiveDefinite(n);
            Console.WriteLine(A);
            Console.ReadKey();
            var b = Vector<double>.Build.Random(n);

            var watch1 = System.Diagnostics.Stopwatch.StartNew();
            var newtonResult = NewtonMethod(x, A, b);
            watch1.Stop();
            Console.WriteLine(newtonResult);
            Console.WriteLine($"Метод Ньютона отработал за {watch1.ElapsedMilliseconds} милисекунд");

            Console.WriteLine(A);
            Console.ReadKey();

            var watch2 = System.Diagnostics.Stopwatch.StartNew();
            var gradientResult = GradientMethod(x, A, b);
            watch2.Stop();
            Console.WriteLine(gradientResult);
            Console.WriteLine($"Градиентный метод отработал за {watch2.ElapsedMilliseconds} милисекунд");

            Console.WriteLine("Идеальный резльтат");
            Console.WriteLine((-1) * A.Inverse() * b);

            Console.ReadKey();
        }
    }
}
