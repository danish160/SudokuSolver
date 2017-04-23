using System.Collections.Generic;

namespace SudokuSolverConsole
{
    public class SudokuCell
    {
        public int CellValue { get; set; }
        public List<int> PossibleValues { get; set; }
    }
}