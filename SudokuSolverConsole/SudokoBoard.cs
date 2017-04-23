using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolverConsole
{
    public class SudokoBoard
    {
        public SudokuCell[,] SudoArray { get; set; }
        public List<int>[] Rows { get; set; }
        public List<int>[] Columns { get; set; }

        private SudokoBoard()
        {
            // instantiation is only allowed through separate method 
            // because initial creation is through int array and after that create instances in the solver 
            // where we do not have the initial int array
        }

        public void Print()
        {
            // print board
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (this.SudoArray[i, j].CellValue == 0)
                        Console.Write("_" + "  ");
                    else
                        Console.Write(this.SudoArray[i, j].CellValue + "  ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            Console.WriteLine();
        }
        public void PrintPossibleValues(bool showCellValues)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (this.SudoArray[i, j].CellValue == 0)
                        Console.Write(this.SudoArray[i, j].PossibleValues.ToCSV() + "\t  ");
                    else
                        Console.Write(showCellValues ? (this.SudoArray[i, j].CellValue + "\t  ") : "\t  ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
        public void PrintRows()
        {
            foreach (List<int> t in this.Rows)
            {
                foreach (var x in t)
                    Console.Write(x + "  ");
                Console.WriteLine();
            }

            Console.WriteLine("******* ** ******* ** ******* ** *******");
            Console.WriteLine();
        }
        public void PrintColumns()
        {
            foreach (List<int> t in this.Columns)
            {
                foreach (var x in t)
                    Console.Write(x + "  ");
                Console.WriteLine();
            }

            Console.WriteLine("******* ** ******* ** ******* ** *******");
            Console.WriteLine();
        }

        public List<int> GetBoxValues(int i, int j)
        {
            List<int> boxValues = new List<int>();
            int a = i / 3;
            int b = j / 3;

            for (int l = 0; l < 3; l++)
            {
                for (int m = 0; m < 3; m++)
                {
                    SudokuCell cell2 = this.SudoArray[(a * 3) + l, (b * 3) + m];
                    if (cell2.CellValue != 0)
                    {
                        boxValues.Add(cell2.CellValue);
                    }
                }
            }

            return boxValues;
        }

        private List<int>[] GetBoxValuesList()
        {
            List<int>[] list = new List<int>[9];

            list[0] = GetBoxValues(0, 0);
            list[1] = GetBoxValues(0, 3);
            list[2] = GetBoxValues(0, 6);

            list[3] = GetBoxValues(3, 0);
            list[4] = GetBoxValues(3, 3);
            list[5] = GetBoxValues(3, 6);

            list[6] = GetBoxValues(6, 0);
            list[7] = GetBoxValues(6, 3);
            list[8] = GetBoxValues(6, 6);
            return list;
        }

        /// <summary>
        /// Solves a specific cell assuming a cell value and attempts to solve the complete board using recusrsion
        /// </summary>
        /// <param name="num">Initial assumed cell value</param>
        /// <param name="r">row number of the cell</param>
        /// <param name="c">column number of the cell</param>
        public void SolveCell(int num, int r, int c)
        {
            SudokuCell cell = this.SudoArray[r, c];

            if (cell.CellValue > 0)
                return; //base case

            cell.CellValue = num;
            cell.PossibleValues = null;

            this.Rows[r].Add(num);
            this.Columns[c].Add(num);

            for (int x = 0; x < 9; x++)
            {
                // remove current value from all possible values in current row
                var horizontalSibling = this.SudoArray[r, x];
                if (horizontalSibling.CellValue == 0 && horizontalSibling.PossibleValues != null)
                {
                    horizontalSibling.PossibleValues.Remove(num);
                    if (horizontalSibling.PossibleValues.Count == 1)
                        SolveCell(horizontalSibling.PossibleValues.First(), r, x);
                }

                // remove current value from all possible values in current column
                var vertialSibling = this.SudoArray[x, c];
                if (vertialSibling.CellValue == 0 && vertialSibling.PossibleValues != null)
                {
                    vertialSibling.PossibleValues.Remove(num);
                    if (vertialSibling.PossibleValues.Count == 1)
                        SolveCell(vertialSibling.PossibleValues.First(), x, c);
                }

                // remove current value from all possible values in current box
                int a = r / 3;
                int b = c / 3;

                for (int l = 0; l < 3; l++)
                {
                    for (int m = 0; m < 3; m++)
                    {
                        SudokuCell boxSibling = this.SudoArray[(a * 3) + l, (b * 3) + m];

                        if (boxSibling.CellValue == 0 && boxSibling.PossibleValues != null)
                        {
                            boxSibling.PossibleValues.Remove(num);
                            if (boxSibling.PossibleValues.Count == 1)
                                SolveCell(boxSibling.PossibleValues.First(), (a * 3) + l, (b * 3) + m);
                        }
                    }
                }
            }
        }

        public bool IsSolved()
        {
            if (this.Rows.Any(row => row.GetDistinctSum() != 45))
            {
                return false;
            }
            else if (this.Columns.Any(col => col.GetDistinctSum() != 45))
            {
                return false;
            }
            else
            {
                var lists = this.GetBoxValuesList();
                if (lists.Any(list => list.GetDistinctSum() != 45))
                {
                    return false;
                }
            }

            return true;
        }

        public static SudokoBoard CreateBoard(int[,] array)
        {
            SudokoBoard newBoard = new SudokoBoard
            {
                SudoArray = new SudokuCell[9, 9],
                Rows = new List<int>[9],
                Columns = new List<int>[9]
            };

            //populate the rows and columns arrays
            for (int i = 0; i < 9; i++)
            {
                newBoard.Rows[i] = new List<int>();
                for (int j = 0; j < 9; j++)
                {
                    if (array[i, j] != 0)
                        newBoard.Rows[i].Add(array[i, j]);
                }
            }
            for (int i = 0; i < 9; i++)
            {
                newBoard.Columns[i] = new List<int>();
                for (int j = 0; j < 9; j++)
                {
                    if (array[j, i] != 0)
                        newBoard.Columns[i].Add(array[j, i]);
                }
            }

            // fill board and possible values
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    newBoard.SudoArray[i, j] = new SudokuCell();
                    newBoard.SudoArray[i, j].CellValue = array[i, j];

                    // if the cell dosent't have a number
                    if (array[i, j] == 0)
                    {
                        //first PossibleValues array with all values from 1-9
                        newBoard.SudoArray[i, j].PossibleValues = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                    }
                }
            }

            return newBoard;
        }

        public SudokoBoard CreateCopy()
        {
            SudokoBoard newBoard = new SudokoBoard
            {
                SudoArray = new SudokuCell[9, 9],
                Rows = new List<int>[9],
                Columns = new List<int>[9]
            };

            // Todo : optimize this method

            //populate the rows and columns arrays
            for (int i = 0; i < 9; i++)
            {
                newBoard.Rows[i] = new List<int>();
                for (int j = 0; j < 9; j++)
                {
                    if (this.SudoArray[i, j].CellValue != 0)
                        newBoard.Rows[i].Add(this.SudoArray[i, j].CellValue);
                }
            }
            for (int i = 0; i < 9; i++)
            {
                newBoard.Columns[i] = new List<int>();
                for (int j = 0; j < 9; j++)
                {
                    if (this.SudoArray[j, i].CellValue != 0)
                        newBoard.Columns[i].Add(this.SudoArray[j, i].CellValue);
                }
            }

            // fill board and possible values
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    newBoard.SudoArray[i, j] = new SudokuCell();
                    newBoard.SudoArray[i, j].CellValue = this.SudoArray[i, j].CellValue;

                    var possibleValues = this.SudoArray[i, j].PossibleValues;
                    if (possibleValues != null)
                        newBoard.SudoArray[i, j].PossibleValues = possibleValues.Select(x => x).ToList();
                }
            }
            return newBoard;
        }
    }
}