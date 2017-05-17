using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver;

namespace SudokuSolverTests
{
    [TestClass]
    public class ISudokoSolverTests
    {
        [TestMethod]
        public void Test_SolveBoard_Solved()
        {
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
            ISudokoSolver solver = new SudokoSolver(board);
            solver.SolveBoard();
            board = solver.GetBoard();

            bool isSolved = SudokuHelper.IsBoardSolved(board);

            Assert.IsTrue(isSolved);
        }
    }
}
