using System.Collections.Generic;

namespace SudokuSolver
{
    public class SudokuCell
    {
        public int CellValue { get; set; }
        public List<int> PossibleValues { get; set; }
    }
}