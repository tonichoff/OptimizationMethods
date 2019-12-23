using System;
using System.Collections.Generic;

namespace SimplexMethod
{
    // https://vscode.ru/prog-lessons/simpleks-metod-realizatsiya.htmls
    class SimplexMethod
    {
        double[,] table;

        int m;
        int n;

        List<int> basis;

        public SimplexMethod(double[,] sourceTable)
        {
            m = sourceTable.GetLength(0);
            n = sourceTable.GetLength(1);
            table = new double[m, n + m - 1];
            basis = new List<int>();
            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < table.GetLength(1); ++j)
                {
                    if (j < n)
                    {
                        table[i, j] = sourceTable[i, j];
                    }
                    else
                    {
                        table[i, j] = 0;
                    }
                }
                if ((n + i) < table.GetLength(1))
                {
                    table[i, n + i] = 1;
                    basis.Add(n + i);
                }
            }
            n = table.GetLength(1);
        }

        public double[,] Calculate(double[] result)
        {
            int mainCol, mainRow;
            while (!IsItEnd())
            {
                mainCol = FindMainCol();
                mainRow = FindMainRow(mainCol);
                basis[mainRow] = mainCol;
                double[,] newTable = new double[m, n];
                for (int j = 0; j < n; ++j)
                {
                    newTable[mainRow, j] = table[mainRow, j] / table[mainRow, mainCol];
                }
                for (int i = 0; i < m; ++i)
                {
                    if (i == mainRow)
                    {
                        continue;
                    }
                    for (int j = 0; j < n; ++j)
                    {
                        newTable[i, j] = table[i, j] - table[i, mainCol] * newTable[mainRow, j];
                    }
                }
                table = newTable;
            }
            for (int i = 0; i < result.Length; ++i)
            {
                int k = basis.IndexOf(i + 1);
                result[i] = (k != -1) ? table[k, 0] : 0;
            }
            return table;
        }

        private bool IsItEnd()
        {
            bool flag = true;
            for (int j = 1; j < n; ++j)
            {
                if (table[m - 1, j] < 0)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        private int FindMainCol()
        {
            int mainCol = 1;
            for (int j = 2; j < n; ++j)
            {
                if (table[m - 1, j] < table[m - 1, mainCol])
                {
                    mainCol = j;
                }
            }
            return mainCol;
        }

        private int FindMainRow(int mainCol)
        {
            int mainRow = 0;
            for (int i = 0; i < m - 1; ++i)
            {
                if (table[i, mainCol] > 0)
                {
                    mainRow = i;
                    break;
                }
            }
            for (int i = mainRow + 1; i < m - 1; ++i)
            {
                if ((table[i, mainCol] > 0) && ((table[i, 0] / table[i, mainCol]) < (table[mainRow, 0] / table[mainRow, mainCol])))
                {
                    mainRow = i;
                }
            }
            return mainRow;
        }
    }
}
