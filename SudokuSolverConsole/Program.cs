using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SudokuSolver;

namespace SudokuSolverConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[,] array = new int[9, 9]
            //{
            //    {6, 5, 0, 0, 0, 0, 0, 9, 7},
            //    {0, 0, 4, 0, 2, 0, 0, 0, 0},
            //    {0, 0, 0, 6, 5, 7, 4, 0, 0},
            //    {0, 9, 0, 8, 0, 0, 0, 2, 3},
            //    {0, 0, 6, 3, 0, 9, 1, 0, 0},
            //    {3, 1, 0, 0, 0, 2, 0, 7, 0},
            //    {0, 0, 3, 2, 8, 4, 0, 0, 0},
            //    {0, 0, 0, 0, 9, 0, 8, 0, 0},
            //    {9, 8, 0, 0, 0, 0, 0, 4, 1},
            //};

            int[,] array = new int[9, 9]
            {
                {0, 0, 9, 0, 0, 0, 0, 0, 0,},
                {3, 0, 4, 0, 8, 1, 0, 2, 0,},
                {0, 8, 6, 5, 0, 0, 0, 0, 0,},
                {9, 0, 1, 3, 0, 0, 0, 0, 2,},
                {0, 0, 0, 0, 4, 0, 0, 0, 0,},
                {6, 0, 0, 0, 0, 2, 4, 0, 1},
                {0, 0, 0, 0, 0, 3, 2, 6, 0},
                {0, 3, 0, 4, 1, 0, 5, 0, 8},
                {0, 0, 0, 0, 0, 0, 1, 0, 0,},
            };

            SudokoBoard board = SudokoBoard.CreateBoard(array);
            Console.WriteLine(" ************ Before solution ************ ");
            Console.WriteLine();
            PrintBoard(board);
            //PrintRows(board);
            //PrintColumns(board);

            if (SudokuHelper.IsBoardValid(board))
            {
                ISudokoSolver solver = new SudokoSolver(board);
                solver.SolveBoard();

                board = solver.GetBoard();

                Console.WriteLine();

                Console.WriteLine(" ************ After solution ************ ");
                Console.WriteLine();
                PrintBoard(board);

                //board.PrintPossibleValues(false);

                Console.WriteLine();
            }
            else
            {
                //throw new InvalidOperationException("The sudoku board has some invalid cells");
                Console.WriteLine("Error: The sudoku board has some invalid cells");
            }

            Console.Read();
        }

        public static void PrintBoard(SudokoBoard pBoard)
        {
            // print board
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (pBoard.SudoArray[i, j].CellValue == 0)
                        Console.Write("_" + "  ");
                    else
                        Console.Write(pBoard.SudoArray[i, j].CellValue + "  ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            Console.WriteLine();
        }
        public static void PrintPossibleValues(SudokoBoard pBoard, bool showCellValues)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (pBoard.SudoArray[i, j].CellValue == 0)
                        Console.Write(pBoard.SudoArray[i, j].PossibleValues.ToCSV() + "\t  ");
                    else
                        Console.Write(showCellValues ? (pBoard.SudoArray[i, j].CellValue + "\t  ") : "\t  ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
        public static void PrintRows(SudokoBoard pBoard)
        {
            foreach (List<int> t in pBoard.Rows)
            {
                foreach (var x in t)
                    Console.Write(x + "  ");
                Console.WriteLine();
            }

            Console.WriteLine("******* ** ******* ** ******* ** *******");
            Console.WriteLine();
        }
        public static void PrintColumns(SudokoBoard pBoard)
        {
            foreach (List<int> t in pBoard.Columns)
            {
                foreach (var x in t)
                    Console.Write(x + "  ");
                Console.WriteLine();
            }

            Console.WriteLine("******* ** ******* ** ******* ** *******");
            Console.WriteLine();
        }
    }
}
