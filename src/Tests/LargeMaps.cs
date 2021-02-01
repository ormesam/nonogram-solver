using Microsoft.VisualStudio.TestTools.UnitTesting;
using NonogramSolver;

namespace Tests {
    [TestClass]
    public class LargeMaps {
        public int[,] QuadMap => new int[15, 15] {
            { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1 },
            { 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 0 },
            { 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
            { 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1 },
            { 1, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 0 },
            { 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0 },
            { 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 0 },
            { 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 },
        };

        public int[][] QuadRowHints => new int[][] {
            new int[] { 2 },
            new int[] { 1, 1 },
            new int[] { 1, 1 },
            new int[] { 2 },
            new int[] { 4 },
            new int[] { 4, 3 },
            new int[] { 1, 4, 1 },
            new int[] { 4, 3, 2 },
            new int[] { 6, 4 },
            new int[] { 14 },
            new int[] { 2, 6, 3 },
            new int[] { 1, 2, 4, 2, 1 },
            new int[] { 1, 6, 1 },
            new int[] { 1, 1, 2, 1, 1 },
            new int[] { 2, 2 },
        };

        public int[][] QuadColumnHints => new int[][] {
            new int[] { 4 },
            new int[] { 4, 2 },
            new int[] { 3, 1, 1 },
            new int[] { 5, 1, 1 },
            new int[] { 1, 4, 2 },
            new int[] { 1, 5 },
            new int[] { 1, 5 },
            new int[] { 2, 1, 2, 5 },
            new int[] { 1, 5, 4 },
            new int[] { 1, 5, 2, 2 },
            new int[] { 2, 3, 2, 1, 1 },
            new int[] { 2, 1, 1 },
            new int[] { 4, 2 },
            new int[] { 5 },
            new int[] { 1, 1 },
        };

        [TestMethod]
        public void LargeMap_QuadBike() {
            Nonogram nonogram = new Nonogram(QuadRowHints, QuadColumnHints);

            var result = nonogram.Solve().Result;

            for (int row = 0; row < QuadRowHints.Length; row++) {
                for (int col = 0; col < QuadColumnHints.Length; col++) {
                    Assert.IsTrue(result[row, col] == QuadMap[row, col]);
                }
            }
        }
    }
}
