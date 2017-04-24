using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolverConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] array = new int[9, 9]
            {
                {6, 5, 0, 0, 0, 0, 0, 9, 7},
                {0, 0, 4, 0, 2, 0, 0, 0, 0},
                {0, 0, 0, 6, 5, 7, 4, 0, 0},
                {0, 9, 0, 8, 0, 0, 0, 2, 3},
                {0, 0, 6, 3, 0, 9, 1, 0, 0},
                {3, 1, 0, 0, 0, 2, 0, 7, 0},
                {0, 0, 3, 2, 8, 4, 0, 0, 0},
                {0, 0, 0, 0, 9, 0, 8, 0, 0},
                {9, 8, 0, 0, 0, 0, 0, 4, 1},
            };

            SudokoBoard board = SudokoBoard.CreateBoard(array);
            Console.WriteLine(" ************ Before solution ************ ");
            Console.WriteLine();
            board.Print();
            //board.PrintRows();
            //board.PrintColumns();

            ISudokoSolver solver = new SudokoSolver(board);
            solver.SolveBoard();

            board = solver.GetBoard();

            Console.WriteLine();

            Console.WriteLine(" ************ After solution ************ ");
            Console.WriteLine();
            board.Print();

            //board.PrintPossibleValues(false);

            Console.WriteLine();

            Console.Read();
        }
    }
}
