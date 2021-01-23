namespace NonogramSolver {
    public class Nonogram {
        private readonly int[][] rowHints;
        private readonly int[][] columnHints;
        private readonly int[,] map;

        public Nonogram(int[][] rowHints, int[][] columnHints) {
            this.rowHints = rowHints;
            this.columnHints = columnHints;

            map = GenerateEmptyMap(rowHints.Length, columnHints.Length);
        }

        private int[,] GenerateEmptyMap(int rows, int columns) {
            var map = new int[rows, columns];

            for (int row = 0; row < rows; row++) {
                for (int col = 0; col < columns; col++) {
                    map[row, col] = 0;
                }
            }

            return map;
        }

        public int[,] Solve() {
            return map;
        }
    }
}
