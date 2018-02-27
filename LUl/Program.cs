using System;


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
                    LUSolver lu = new LUSolver();
                    //размерность
                    int n = 4;
                    double[,] matrix = { { 8, -7, 2, -3 }, { 3, -8, -9, -6 }, { -6, -1, -4, -4 }, { -2, -8, -9, -4 } };
                    double[] rightPart = { 7, 13, 23, 41 };
                    //answers
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

    public class LUSolver
    {
        /// <summary>
        /// Solve system of linear equation via LU-decomposing
        /// </summary>
        /// <param name="matrix">Matrix of SoLE</param>
        /// <param name="rightPart">B-part of SoLE</param>
        /// <param name="n">matrix dimension</param>
        /// <returns>Array of X-s. Array of roots.</returns>
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

        

        /// <summary>
        /// Display matrix in console.
        /// </summary>
        /// <param name="matr"></param>
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
