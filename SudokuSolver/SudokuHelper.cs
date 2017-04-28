using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    /// <summary>
    /// General utitlity methods for helping with solving sudoku boards
    /// </summary>
    public class SudokuHelper
    {
        /// <summary>
        /// Checks whether a sudoku board is solved or not based on column, row or box sums
        /// </summary>
        /// <param name="board">Sudoku board to be tested for solution</param>
        /// <returns>True if input board is solved</returns>
        public static bool IsBoardSolved(SudokoBoard board)
        {
            if (board.Rows.Any(row => row.GetDistinctSum() != 45))
            {
                return false;
            }
            else if (board.Columns.Any(col => col.GetDistinctSum() != 45))
            {
                return false;
            }
            else
            {
                var lists = board.GetBoxValuesList();
                if (lists.Any(list => list.GetDistinctSum() != 45))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks whether a sudoku board is valid or not based on repetition of values in a column, row or box
        /// </summary>
        /// <param name="board">Sudoku board to be tested for solution</param>
        /// <returns>True if input board is valid</returns>
        public static bool IsBoardValid(SudokoBoard board)
        {
            if (board.Rows.Any(lst => lst.Count(x => x != 0) != lst.Where(x => x != 0).Distinct().Count()))
            {
                return false;
            }
            if (board.Columns.Any(lst => lst.Count(x => x != 0) != lst.Where(x => x != 0).Distinct().Count()))
            {
                return false;
            }

            var boxValuesList = board.GetBoxValuesList();
            if (boxValuesList.Any(lst => lst.Count(x => x != 0) != lst.Where(x => x != 0).Distinct().Count()))
            {
                return false;
            }

            return true;
        }
    }
}
