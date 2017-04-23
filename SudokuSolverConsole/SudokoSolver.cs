using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolverConsole
{
    public class SudokoSolver : ISudokoSolver
    {
        private SudokoBoard Board { get; set; }
        public SudokoBoard GetBoard()
        {
            return Board;
        }

        public bool IsSolved { get; private set; }

        public SudokoSolver(SudokoBoard board)
        {
            Board = board;
        }

        public void SolveBoard()
        {
            ReducePossibleValues(Board);

            var linearIndices = Enumerable.Range(0, 9)
                                .SelectMany(i => Enumerable.Range(0, 9)
                                    .Select(j => new { Row = i, Col = j }));

            var list = linearIndices
                .Select(t => new { SudoCell = Board.SudoArray[t.Row, t.Col], RowIndex = t.Row, ColumnIndex = t.Col })
                .Where(x => x.SudoCell.CellValue == 0 && x.SudoCell.PossibleValues != null)
                .Select(x => new { x.SudoCell, x.RowIndex, x.ColumnIndex, PVCount = x.SudoCell.PossibleValues.Count })
                .OrderBy(x => x.PVCount)
                .ToList();

            // this looks like a bad logic (iterating over every single possible value and running the algorithm)
            // but in reality the board will be solved much earlier ... needs more testing though
            foreach (var a in list)
            {
                foreach (var pv in a.SudoCell.PossibleValues)
                {
                    var newBoard = Board.CreateCopy();
                    newBoard.SolveCell(pv, a.RowIndex, a.ColumnIndex);
                    bool isSolved = newBoard.IsSolved();
                    if (isSolved)
                    {
                        IsSolved = true;
                        Board = newBoard;
                        return;
                    }
                }
            }
        }

        private static void ReducePossibleValues(SudokoBoard pBoard)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    SudokuCell cell = pBoard.SudoArray[i, j];

                    if (cell.CellValue == 0)
                    {
                        //cell.PossibleValues = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                        List<int> boxValues = null;

                        foreach (var pv in cell.PossibleValues.ToList())
                        {
                            if (pBoard.Rows[i].Contains(pv))
                                cell.PossibleValues.Remove(pv);
                            else if (pBoard.Columns[j].Contains(pv))
                                cell.PossibleValues.Remove(pv);
                            else
                            {
                                if (boxValues == null)
                                    boxValues = pBoard.GetBoxValues(i, j); // new List<int>();

                                if (boxValues.Contains(pv))
                                    cell.PossibleValues.Remove(pv);
                            }
                        }

                        if (cell.PossibleValues.Count == 1)
                        {
                            cell.CellValue = cell.PossibleValues[0];
                            pBoard.Rows[i].Add(cell.CellValue);
                            pBoard.Columns[j].Add(cell.CellValue);
                            cell.PossibleValues = null;
                        }
                    }
                }
            }
        }
    }
}
