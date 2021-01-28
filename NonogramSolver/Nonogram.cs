using System;
using System.Diagnostics;
using System.Linq;

namespace NonogramSolver {
    public class Nonogram {
        private readonly int[][] rowHints;
        private readonly int[][] columnHints;
        private readonly Cell[,] map;
        private readonly LineSolver lineSolver;

        public Nonogram(int[][] rowHints, int[][] columnHints) {
            this.rowHints = rowHints;
            this.columnHints = columnHints;
            this.lineSolver = new LineSolver();

            map = GenerateEmptyMap(rowHints.Length, columnHints.Length);
        }

        private Cell[,] GenerateEmptyMap(int rows, int columns) {
            var map = new Cell[rows, columns];

            for (int row = 0; row < rows; row++) {
                for (int col = 0; col < columns; col++) {
                    map[row, col] = Cell.Unknown;
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

                for (int row = 0; row < rowHints.Length; row++) {
                    var currentRow = GetRow(row);
                    var updatedRow = lineSolver.Solve(GetRow(row), rowHints[row]);

                    bool hasLineChanged = !currentRow.SequenceEqual(updatedRow);

                    if (hasLineChanged) {
                        ReplaceRow(row, updatedRow);

                        hasChanged = true;
                    }
                }

                for (int col = 0; col < columnHints.Length; col++) {
                    var currentColumn = GetColumn(col);
                    var updatedColumn = lineSolver.Solve(GetColumn(col), columnHints[col]);

                    bool hasLineChanged = !currentColumn.SequenceEqual(updatedColumn);

                    if (hasLineChanged) {
                        ReplaceColumn(col, updatedColumn);

                        hasChanged = true;
                    }
                }

                Print(iteration);
            }

            stopWatch.Stop();

            return new NonogramSolveResult {
                CouldBeSolved = IsSolved(),
                Result = Convert(),
                Iterations = iteration,
                TimeTaken = stopWatch.Elapsed,
            };
        }

        private bool IsSolved() {
            for (int row = 0; row < rowHints.Length; row++) {
                if (!lineSolver.IsLineLogicallyComplete(GetRow(row), rowHints[row])) {
                    return false;
                }
            }

            for (int col = 0; col < columnHints.Length; col++) {
                if (!lineSolver.IsLineLogicallyComplete(GetColumn(col), columnHints[col])) {
                    return false;
                }
            }

            return true;
        }

        private Cell[] GetColumn(int colIdx) {
            Cell[] column = new Cell[rowHints.Length];

            for (int row = 0; row < column.Length; row++) {
                column[row] = map[row, colIdx];
            }

            return column;
        }

        private Cell[] GetRow(int rowIdx) {
            Cell[] row = new Cell[columnHints.Length];

            for (int col = 0; col < row.Length; col++) {
                row[col] = map[rowIdx, col];
            }

            return row;
        }

        private void ReplaceColumn(int colIdx, Cell[] column) {
            for (int row = 0; row < column.Length; row++) {
                map[row, colIdx] = column[row];
            }
        }

        private void ReplaceRow(int rowIdx, Cell[] row) {
            for (int col = 0; col < row.Length; col++) {
                map[rowIdx, col] = row[col];
            }
        }

        private int[,] Convert() {
            var map = new int[rowHints.Length, columnHints.Length];

            for (int row = 0; row < rowHints.Length; row++) {
                for (int col = 0; col < columnHints.Length; col++) {
                    map[row, col] = this.map[row, col] == Cell.Filled ? 1 : 0;
                }
            }

            return map;
        }

        private void Print(int iteration) {
            Console.WriteLine("Iteration " + iteration);

            for (int row = 0; row < rowHints.Length; row++) {
                for (int col = 0; col < columnHints.Length; col++) {
                    string character;

                    switch (map[row, col]) {
                        case Cell.Blank:
                            character = " ";
                            break;
                        case Cell.Filled:
                            character = "X";
                            break;
                        default:
                            character = "-";
                            break;
                    }

                    Console.Write(character);
                    Console.Write(" ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
