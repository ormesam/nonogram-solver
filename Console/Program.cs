using NonogramSolver;

namespace Console {
    class Program {
        public static int[][] RowHints => new int[][] {
            new int[] { 1 },
            new int[] { 2 },
            new int[] { 3 },
            new int[] { 4 },
            new int[] { 5 },
            new int[] { 1 },
            new int[] { 8 },
            new int[] { 6 },
        };

        public static int[][] ColumnHints => new int[][] {
            new int[] { 1, 1 },
            new int[] { 2, 2 },
            new int[] { 3, 2 },
            new int[] { 4, 2 },
            new int[] { 8 },
            new int[] { 2 },
            new int[] { 2 },
            new int[] { 1 },
        };

        static void Main(string[] args) {
            Nonogram nonogram = new Nonogram(RowHints, ColumnHints);
            nonogram.Solve();
        }
    }
}
