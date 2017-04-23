using System.Collections.Generic;
using System.Linq;

namespace SudokuSolverConsole
{
    public static class Extensions
    {
        public static string ToCSV(this List<int> list)
        {
            return string.Join(",", list);
        }

        public static int GetDistinctSum(this List<int> list)
        {
            return list.Distinct().Sum();
        }
    }
}