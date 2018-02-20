using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUl
{
    class Program
    {
        static void Main(string[] args)
        {

            double[,] A = new double[4, 4];
            double[,] L = new double[4, 4];
            double[,] U = new double[4,4];



            //1. при i = 0
            for (int j = 0; j < U.GetLength(1); j++)
            {
                U[0, j] = A[0, j];
            }
            for (int i = 0; i < L.GetLength(0);i++)
            {
               if(U[0,0]!=0.0) L[i, 1] = A[i, 0] / U[0, 0];
            }


            // при i = 1...n
            for (int i = 1; i < U.GetLength(0); i++)
            {
                for (int j = 0; j < U.GetLength(1); j++)
                {
                    double d = 0;
                    for (int k = 0; k < i-1; k++)
                    {
                        d += L[i, k] * U[k, j];
                    }
                    U[i, j] = A[i, j] - d;

                    double d1 = 0;
                    for (int k = 0; k < i-1; k++)
                    {
                        d1+=L[j, k] * U[k, i];
                    }
                    L[j, i] = 1 / U[i, i]*(A[j, i] - d1);
                }

            }

            double[] rightPart = { 7, 13, 23, 41 };

            double sum;
            // find solution of Ly = b
            int n = 4;
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


        }
    }
}
