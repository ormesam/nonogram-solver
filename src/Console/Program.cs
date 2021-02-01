using NonogramSolver;

namespace Console {
    class Program {

        public static int[][] RowHints => new int[][] {
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

        public static int[][] ColumnHints => new int[][] {
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

        static void Main(string[] args) {
            Nonogram nonogram = new Nonogram(RowHints, ColumnHints, new ConsoleLogger());
            nonogram.Solve();
        }
    }
}
