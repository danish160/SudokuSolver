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
    public class SudokuHelperTests
    {
        [TestMethod]
        public void Test_IsBoardSolved_Solved()
        {
            int[,] array = new int[9, 9]
            {
                {5, 1, 9, 2, 3, 7, 8, 4, 6},
                {3, 7, 4, 6, 8, 1, 9, 2, 5},
                {2, 8, 6, 5, 9, 4, 3, 1, 7},
                {9, 4, 1, 3, 6, 8, 7, 5, 2},
                {8, 2, 7, 1, 4, 5, 6, 3, 9},
                {6, 5, 3, 9, 7, 2, 4, 8, 1},
                {1, 9, 8, 7, 5, 3, 2, 6, 4},
                {7, 3, 2, 4, 1, 6, 5, 9, 8},
                {4, 6, 5, 8, 2, 9, 1, 7, 3}
            };

            SudokoBoard board = SudokoBoard.CreateBoard(array);

            bool isSolved = SudokuHelper.IsBoardSolved(board);

            Assert.IsTrue(isSolved);
        }

        [TestMethod]
        public void Test_IsBoardSolved_UnSolved()
        {
            int[,] array = new int[9, 9]
            {
                {5, 1, 9, 2, 3, 7, 8, 4, 6},
                {0, 7, 4, 6, 8, 1, 9, 2, 5},
                {2, 8, 6, 5, 9, 4, 3, 1, 7},
                {9, 4, 1, 3, 6, 8, 7, 5, 2},
                {8, 2, 7, 1, 4, 5, 6, 3, 9},
                {6, 5, 3, 9, 7, 2, 4, 8, 1},
                {1, 9, 8, 7, 5, 3, 2, 6, 4},
                {7, 3, 2, 4, 1, 6, 5, 9, 8},
                {4, 6, 5, 8, 2, 9, 1, 7, 3}
            };

            SudokoBoard board = SudokoBoard.CreateBoard(array);

            bool isSolved = SudokuHelper.IsBoardSolved(board);

            Assert.IsFalse(isSolved);
        }

        [TestMethod]
        public void Test_IsBoardValid_Valid()
        {
            int[,] array = new int[9, 9]
            {
                {5, 1, 9, 2, 3, 7, 8, 4, 6},
                {0, 7, 4, 6, 8, 1, 9, 2, 5},
                {2, 8, 6, 5, 9, 4, 3, 1, 7},
                {9, 4, 1, 3, 6, 8, 7, 5, 2},
                {8, 2, 7, 1, 4, 5, 6, 3, 9},
                {6, 5, 3, 9, 7, 2, 4, 8, 1},
                {1, 9, 8, 7, 5, 3, 2, 6, 4},
                {7, 3, 2, 4, 1, 6, 5, 9, 8},
                {4, 6, 5, 8, 2, 9, 1, 7, 3}
            };

            SudokoBoard board = SudokoBoard.CreateBoard(array);

            bool isSolved = SudokuHelper.IsBoardValid(board);

            Assert.IsTrue(isSolved);
        }

        [TestMethod]
        public void Test_IsBoardValid_Invalid()
        {
            int[,] array = new int[9, 9]
            {
                {5, 1, 1, 2, 3, 7, 8, 4, 6},
                {5, 7, 4, 6, 8, 1, 9, 2, 5},
                {2, 8, 6, 5, 9, 4, 3, 1, 7},
                {9, 4, 1, 3, 6, 8, 7, 5, 2},
                {8, 2, 7, 1, 4, 5, 6, 3, 9},
                {6, 5, 3, 9, 7, 2, 4, 8, 1},
                {1, 9, 8, 7, 5, 3, 2, 6, 4},
                {7, 3, 2, 4, 1, 6, 5, 9, 8},
                {4, 6, 5, 8, 2, 9, 1, 7, 3}
            };

            SudokoBoard board = SudokoBoard.CreateBoard(array);

            bool isSolved = SudokuHelper.IsBoardValid(board);

            Assert.IsFalse(isSolved);
        }
    }
}
