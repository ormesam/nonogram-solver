using System.Collections.Generic;
using System.Linq;

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
            while (!IsSolved()) {
                bool lineChanged = false;

                for (int row = 0; row < rowHints.Length; row++) {
                    bool hasChanged = SolveLine(GetRow(row), rowHints[row]);

                    if (hasChanged) {
                        lineChanged = true;
                    }
                }

                for (int col = 0; col < columnHints.Length; col++) {
                    bool hasChanged = SolveLine(GetColumn(col), columnHints[col]);

                    if (hasChanged) {
                        lineChanged = true;
                    }
                }

                // If no lines have changed but it is not solved then it is unsolvable
                if (!lineChanged) {
                    break;
                }
            }

            return map;
        }

        private bool SolveLine(int[] line, int[] hints) {
            return false;
        }

        private bool IsSolved() {
            for (int row = 0; row < rowHints.Length; row++) {
                if (!IsLineLogicallyComplete(GetRow(row), rowHints[row])) {
                    return false;
                }
            }

            for (int col = 0; col < columnHints.Length; col++) {
                if (!IsLineLogicallyComplete(GetColumn(col), columnHints[col])) {
                    return false;
                }
            }

            return true;
        }

        private bool IsLineLogicallyComplete(int[] line, int[] hints) {
            int currentCount = 0;
            IList<int> segments = new List<int>();

            for (int i = 0; i < line.Length; i++) {
                if (line[i] == 1) {
                    currentCount++;
                } else if (currentCount > 0) {
                    segments.Add(currentCount);
                    currentCount = 0;
                }
            }

            if (currentCount > 0) {
                segments.Add(currentCount);
            }

            if (!segments.Any()) {
                segments.Add(0);
            }

            return hints.SequenceEqual(segments);
        }

        private int[] GetColumn(int colIdx) {
            int[] column = new int[rowHints.Length];

            for (int row = 0; row < column.Length; row++) {
                column[row] = map[row, colIdx];
            }

            return column;
        }

        private int[] GetRow(int rowIdx) {
            int[] row = new int[columnHints.Length];

            for (int col = 0; col < row.Length; col++) {
                row[col] = map[rowIdx, col];
            }

            return row;
        }

        private void ReplaceColumn(int colIdx, int[] column) {
            for (int row = 0; row < column.Length; row++) {
                map[row, colIdx] = column[row];
            }
        }

        private void ReplaceRow(int rowIdx, int[] row) {
            for (int col = 0; col < row.Length; col++) {
                map[rowIdx, col] = row[col];
            }
        }
    }
}
