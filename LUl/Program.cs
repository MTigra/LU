//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LUl
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {

//            double[,] A = new double[,] { { 10, -7, 0 }, { -3, 6, 2 }, { 5, -1, 5 } };
//            double[,] L = new double[3, 3];
//            double[,] U = new double[3, 3];



//            ////1. при i = 0
//            //for (int j = 0; j < U.GetLength(1); j++)
//            //{
//            //    U[0, j] = A[0, j];
//            //}
//            //for (int i = 0; i < L.GetLength(0); i++)
//            //{
//            //    //if (U[0, 0] != 0.0)
//            //        L[i, 1] = A[i, 0] / U[0, 0];
//            //}
//            //Console.WriteLine("U\n");
//            //ShowMatr(U);

//            //// при i = 1...n
//            //for (int i = 1; i < U.GetLength(0); i++)
//            //{
//            //    for (int j = 0; j < U.GetLength(1); j++)
//            //    {
//            //        double d = 0;
//            //        for (int k = 0; k < i - 1; k++)
//            //        {
//            //            d += L[i, k] * U[k, j];
//            //        }
//            //        U[i, j] = A[i, j] - d;

//            //        double d1 = 0;
//            //        for (int k = 0; k < i - 1; k++)
//            //        {
//            //            d1 += L[j, k] * U[k, i];
//            //        }
//            //        L[j, i] = 1 / U[i, i] * (A[j, i] - d1);
//            //    }

//            //}

//            //Console.WriteLine("L\n");
//            //ShowMatr(L);

//            for (int k = 0; k < A.GetLength(1) - 1; k++)
//            {
//                for (int i = k + 1; i < A.GetLength(0); i++)
//                {
//                    L[i, k] = U[i, k] / U[k, k];
//                    for (int j = k; j < A.GetLength(1); j++)
//                        U[i, j] = U[i, j] - L[i, k] * U[k, j];
//                }
//            }
//            Console.WriteLine("L\n");
//            ShowMatr(L);

//            Console.WriteLine("U\n");
//            ShowMatr(U);
//            double[] rightPart = { 7, 13, 23 };


//            // find solution of Ly = b
//            int n = 2;
//            double[] y = new double[n];
//            y[0] = rightPart[0] / L[0, 0];
//            for (int i = 1; i < L.GetLength(1); i++)
//            {
//                double a = 0;
//                for (int k = 0; k < i - 1; k++)
//                {
//                    a += L[i, k] * y[k];
//                }

//                y[i] = 1 / L[i, i] * (rightPart[i] - a);
//            }



//            // dind solution of Ux=y;
//            double[] x = new double[n];
//            x[n - 1] = y[n - 1];
//            for (int i = x.Length - 2; i < 0; i++)
//            {
//                double sum = 0;
//                for (int k = i; k < n; k++)
//                {
//                    sum += U[i, k] * x[k];
//                }
//                x[i] = y[i] - sum;
//            }

//            Array.ForEach(x, Console.WriteLine);
//        }



//        public static void ShowMatr(double[,] matr)
//        {
//            for (int i = 0; i < matr.GetLength(0); i++)
//            {
//                for (int j = 0; j < matr.GetLength(1); j++)
//                {
//                    Console.Write($"{matr[i, j]} ");
//                }
//                Console.WriteLine();
//            }
//        }
//    }
//}
using System;

/*
 * Ученик:  Рябичев Алексей Михайлович
 * Ггруппа: БПИ176 (11)
 * Дата:    2017.mm.dd
 * Задача:  
 */

namespace LU
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            do
            {
                try
                {
                    SystemOfLinearEquations lu = new SystemOfLinearEquations();
                    int n = 3;
                    double[,] matrix = { { 10, -7, 0 }, { -3, 6, 2 }, { 5, -1, 5 } };
                    double[] rightPart = { 7, 13, 23, 41 };
                    double[] ans = lu.SolveUsingLU(matrix, rightPart, n);
                    foreach (double tmp in ans) Console.WriteLine(tmp);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.WriteLine("Press Esc to exit or another button to continue");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }

    public class SystemOfLinearEquations
    {
        public double[] SolveUsingLU(double[,] matrix, double[] rightPart, int n)
        {
            // decomposition of matrix
            double[,] lu = new double[n, n];
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    sum = 0;
                    for (int k = 0; k < i; k++)
                        sum += lu[i, k] * lu[k, j];
                    lu[i, j] = matrix[i, j] - sum;
                }

                for (int j = i + 1; j < n; j++)
                {
                    sum = 0;
                    for (int k = 0; k < i; k++)
                        sum += lu[j, k] * lu[k, i];
                    lu[j, i] = 1 / lu[i, i] * (matrix[j, i] - sum);
                }
            }

            ShowMatr(lu);
            // find solution of Ly = b
            double[] y = new double[n];
            for (int i = 0; i < n; i++)
            {
                sum = 0;
                for (int k = 0; k < i; k++)
                    sum += lu[i, k] * y[k];
                y[i] = rightPart[i] - sum;
            }

            // find solution of Ux = y
            double[] x = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                sum = 0;
                for (int k = i + 1; k < n; k++)
                    sum += lu[i, k] * x[k];
                x[i] = 1 / lu[i, i] * (y[i] - sum);
            }

            return x;

            //TODO: добавить ввывод матриц
            //Code for Wolfram: LinearSolve[{{8, -7, 2, -3}, {3, -8, -9, -6}, {-6, -1, -4, -4}, {-2, -8, -9, -4}}, {7, 13, 23, 41}]
        }

        public static void ShowMatr(double[,] matr)
        {
            for (int i = 0; i < matr.GetLength(0); i++)
            {
                for (int j = 0; j < matr.GetLength(1); j++)
                {
                    Console.Write($"{matr[i, j]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
    