using System;
using System.Collections.Generic;

namespace SimplexMethod
{
    // Ref: https://vscode.ru/prog-lessons/simpleks-metod-realizatsiya.htmls
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
            for (var i = 0; i < m; ++i)
            {
                for (var j = 0; j < table.GetLength(1); ++j)
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
            var mainCol = 0;
            var mainRow = 0;
            while (!IsItEnd())
            {
                mainCol = FindMainCol();
                mainRow = FindMainRow(mainCol);
                basis[mainRow] = mainCol;
                var newTable = new double[m, n];
                for (var j = 0; j < n; ++j)
                {
                    newTable[mainRow, j] = table[mainRow, j] / table[mainRow, mainCol];
                }
                for (var i = 0; i < m; ++i)
                {
                    if (i == mainRow)
                    {
                        continue;
                    }
                    for (var j = 0; j < n; ++j)
                    {
                        newTable[i, j] = table[i, j] - table[i, mainCol] * newTable[mainRow, j];
                    }
                }
                table = newTable;
            }
            for (var i = 0; i < result.Length; ++i)
            {
                var k = basis.IndexOf(i + 1);
                result[i] = (k != -1) ? table[k, 0] : 0;
            }
            return table;
        }

        private bool IsItEnd()
        {
            var flag = true;
            for (var j = 1; j < n; ++j)
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
            var mainCol = 1;
            for (var j = 2; j < n; ++j)
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
            var mainRow = 0;
            for (var i = 0; i < m - 1; ++i)
            {
                if (table[i, mainCol] > 0)
                {
                    mainRow = i;
                    break;
                }
            }
            for (var i = mainRow + 1; i < m - 1; ++i)
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