using System;
using System.Diagnostics;
using System.Linq;

namespace NonogramSolver {
    public class Nonogram {
        private readonly int[][] rowHints;
        private readonly int[][] columnHints;
        private readonly ILogger logger;
        private readonly CellValue[,] map;
        private readonly LineSolver lineSolver;
        private readonly int width;
        private readonly int height;

        public Nonogram(int[][] rowHints, int[][] columnHints, ILogger logger = null) {
            this.rowHints = rowHints;
            this.columnHints = columnHints;
            this.logger = logger;
            this.width = columnHints.Length;
            this.height = rowHints.Length;
            this.lineSolver = new LineSolver();

            map = GenerateEmptyMap();
        }

        private CellValue[,] GenerateEmptyMap() {
            var map = new CellValue[height, width];

            for (int row = 0; row < height; row++) {
                for (int col = 0; col < width; col++) {
                    map[row, col] = CellValue.Unknown;
                }
            }

            return map;
        }

        public NonogramSolveResult Solve() {
            bool hasChanged = true;
            int iteration = 0;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            while (hasChanged) {
                iteration++;
                hasChanged = false;

                for (int row = 0; row < height; row++) {
                    var currentRow = GetRow(row);
                    var updatedRow = lineSolver.Solve(GetRow(row), rowHints[row]);

                    bool hasLineChanged = !currentRow.SequenceEqual(updatedRow);

                    if (hasLineChanged) {
                        ReplaceRow(row, updatedRow);

                        hasChanged = true;
                    }

                    logger?.LineSolved(row, true, updatedRow);
                }

                for (int col = 0; col < width; col++) {
                    var currentColumn = GetColumn(col);
                    var updatedColumn = lineSolver.Solve(GetColumn(col), columnHints[col]);

                    bool hasLineChanged = !currentColumn.SequenceEqual(updatedColumn);

                    if (hasLineChanged) {
                        ReplaceColumn(col, updatedColumn);

                        hasChanged = true;
                    }

                    logger?.LineSolved(col, false, updatedColumn);
                }
            }

            stopWatch.Stop();

            return new NonogramSolveResult {
                IsSolved = IsSolved(),
                Result = Convert(),
                Iterations = iteration,
                TimeTaken = stopWatch.Elapsed,
            };
        }

        private bool IsSolved() {
            for (int row = 0; row < height; row++) {
                if (!LineSolver.IsLineLogicallyComplete(GetRow(row), rowHints[row])) {
                    return false;
                }
            }

            for (int col = 0; col < width; col++) {
                if (!LineSolver.IsLineLogicallyComplete(GetColumn(col), columnHints[col])) {
                    return false;
                }
            }

            return true;
        }

        private CellValue[] GetColumn(int colIdx) {
            CellValue[] column = new CellValue[height];

            for (int row = 0; row < column.Length; row++) {
                column[row] = map[row, colIdx];
            }

            return column;
        }

        private CellValue[] GetRow(int rowIdx) {
            CellValue[] row = new CellValue[width];

            for (int col = 0; col < row.Length; col++) {
                row[col] = map[rowIdx, col];
            }

            return row;
        }

        private void ReplaceColumn(int colIdx, CellValue[] column) {
            for (int row = 0; row < column.Length; row++) {
                map[row, colIdx] = column[row];
            }
        }

        private void ReplaceRow(int rowIdx, CellValue[] row) {
            for (int col = 0; col < row.Length; col++) {
                map[rowIdx, col] = row[col];
            }
        }

        private int[,] Convert() {
            var map = new int[height, width];

            for (int row = 0; row < height; row++) {
                for (int col = 0; col < width; col++) {
                    map[row, col] = this.map[row, col] == CellValue.Filled ? 1 : 0;
                }
            }

            return map;
        }
    }
}
