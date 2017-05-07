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
    public class SudokoBoardTests
    {
        [TestMethod]
        public void Test_GetBoxValues()
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

            // 3   0    0
            // 0    4   4
            // 0    0   2

            List<int> box5 = board.GetBoxValues(3, 3);
            List<int> box5Expected = new List<int> { 3, 0, 0, 0, 4, 0, 0, 0, 2 };

            var differnce = box5.Except(box5Expected).ToList();

            Assert.IsTrue(differnce.Count == 0);
        }

        [TestMethod]
        public void Test_GetBoxValuesList()
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

            // 3   0    0
            // 0    4   4
            // 0    0   2

            var boxes = board.GetBoxValuesList();


            List<int> box5 = boxes.SelectMany(list => list).ToList();

            List<int> box5Expected = new List<int>
            {
                0,0,9,3,0,4,0,8,6,0,0,0,0,8,1,5,0,0,0,0,0,0,2,0,0,0,0,
                9,0,1,0,0,0,6,0,0,3,0,0,0,4,0,0,0,2,0,0,2,0,0,0,4,0,1,
                0,0,0,0,3,0,0,0,0,0,0,3,4,1,0,0,0,0,2,6,0,5,0,8,1,0,0,
            };

            var differnce = box5.Except(box5Expected).ToList();

            Assert.IsTrue(differnce.Count == 0);
        }

    }
}
