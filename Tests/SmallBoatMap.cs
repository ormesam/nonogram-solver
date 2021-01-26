using Microsoft.VisualStudio.TestTools.UnitTesting;
using NonogramSolver;

namespace Tests {
    [TestClass]
    public class SmallBoatMap {
        public int[,] BoatMap => new int[8, 8] {
            { 0, 0, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 1, 0, 0, 0 },
            { 0, 0, 1, 1, 1, 0, 0, 0 },
            { 0, 1, 1, 1, 1, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 1, 1, 1 },
            { 0, 1, 1, 1, 1, 1, 1, 0 },
        };

        public int[][] RowHints => new int[][] {
            new int[] { 1 },
            new int[] { 2 },
            new int[] { 3 },
            new int[] { 4 },
            new int[] { 5 },
            new int[] { 1 },
            new int[] { 8 },
            new int[] { 6 },
        };

        public int[][] ColumnHints => new int[][] {
            new int[] { 1, 1 },
            new int[] { 2, 2 },
            new int[] { 3, 2 },
            new int[] { 4, 2 },
            new int[] { 8 },
            new int[] { 2 },
            new int[] { 2 },
            new int[] { 1 },
        };

        [TestMethod]
        public void SolveSmallMap() {
            Nonogram nonogram = new Nonogram(RowHints, ColumnHints);

            var result = nonogram.Solve();

            for (int row = 0; row < RowHints.Length; row++) {
                for (int col = 0; col < ColumnHints.Length; col++) {
                    Assert.IsTrue(result[row, col] == BoatMap[row, col]);
                }
            }
        }

        public int[,] CandleMap => new int[8, 8] {
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 0, 0, 1, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 1, 0, 0, 1 },
            { 1, 0, 0, 1, 1, 0, 0, 1 },
            { 1, 1, 0, 1, 1, 0, 1, 1 },
            { 1, 1, 0, 1, 1, 0, 1, 1 },
            { 0, 1, 1, 1, 1, 1, 1, 0 },
            { 0, 0, 1, 1, 1, 1, 0, 0 },
        };

        public int[][] CandleRowHints => new int[][] {
            new int[] { 0 },
            new int[] { 1, 1, 1 },
            new int[] { 1, 1, 1 },
            new int[] { 1, 2, 1 },
            new int[] { 2, 2, 2 },
            new int[] { 2, 2, 2 },
            new int[] { 6 },
            new int[] { 4 },
        };

        public int[][] CandleColumnHints => new int[][] {
            new int[] { 5 },
            new int[] { 3 },
            new int[] { 2 },
            new int[] { 1, 5 },
            new int[] { 6 },
            new int[] { 2 },
            new int[] { 3 },
            new int[] { 5 },
        };

        [TestMethod]
        public void SolveSmallCandle() {
            Nonogram nonogram = new Nonogram(CandleRowHints, CandleColumnHints);

            var result = nonogram.Solve();

            for (int row = 0; row < CandleRowHints.Length; row++) {
                for (int col = 0; col < CandleColumnHints.Length; col++) {
                    Assert.IsTrue(result[row, col] == CandleMap[row, col]);
                }
            }
        }
    }
}
