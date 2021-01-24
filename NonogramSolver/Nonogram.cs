using System;
using System.Collections.Generic;
using System.Linq;

namespace NonogramSolver {
    public class Nonogram {
        private readonly int[][] rowHints;
        private readonly int[][] columnHints;
        private readonly Cell[,] map;
        private int iteration;

        public Nonogram(int[][] rowHints, int[][] columnHints) {
            this.iteration = 1;
            this.rowHints = rowHints;
            this.columnHints = columnHints;

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

        public int[,] Solve() {
            bool lineChanged;

            do {
                lineChanged = false;

                for (int row = 0; row < rowHints.Length; row++) {
                    var currentRow = GetRow(row);
                    var updatedRow = SolveLine(GetRow(row), rowHints[row]);

                    bool hasChanged = !currentRow.SequenceEqual(updatedRow);

                    if (hasChanged) {
                        ReplaceRow(row, updatedRow);

                        lineChanged = true;
                    }
                }

                for (int col = 0; col < columnHints.Length; col++) {
                    var currentColumn = GetColumn(col);
                    var updatedColumn = SolveLine(GetColumn(col), columnHints[col]);

                    bool hasChanged = !currentColumn.SequenceEqual(updatedColumn);

                    if (hasChanged) {
                        ReplaceColumn(col, updatedColumn);

                        lineChanged = true;
                    }
                }

                Print();

                iteration++;
            } while (!IsSolved() && lineChanged);

            return Convert();
        }

        private Cell[] SolveLine(Cell[] line, int[] hints) {
            if (IsLineFull(line)) {
                return line;
            }

            int startIdx = 0;
            int endIdx = 0;

            while (endIdx < line.Length - 1) {
                NextSegment(line, ref startIdx, ref endIdx);
            }

            if (IsLineLogicallyComplete(line, hints)) {
                FillEmptyCells(line);
            }

            return line;
        }

        public void NextSegment(Cell[] line, ref int startIdx, ref int lastIdx) {
            if (startIdx != 0) {
                startIdx = lastIdx + 1;
            }

            lastIdx = line.Length - 1;

            bool foundFirst = false;

            for (int i = startIdx; i < line.Length; i++) {
                if (line[i] != Cell.Blank && !foundFirst) {
                    foundFirst = true;
                    startIdx = i;
                }

                if (foundFirst) {
                    if (line[i] == Cell.Blank) {
                        lastIdx = i - 1;
                        return;
                    }
                }
            }
        }

        private void FillEmptyCells(Cell[] line) {
            for (int i = 0; i < line.Length; i++) {
                if (line[i] == Cell.Unknown) {
                    line[i] = Cell.Blank;
                }
            }
        }

        private void FillCells(Cell[] line, int startIdx, int numberOfCells, Cell value) {
            for (int i = startIdx; i < startIdx + numberOfCells; i++) {
                if (line[i] != Cell.Unknown && line[i] != value) {
                    throw new Exception();
                }

                line[i] = value;
            }
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

        private bool IsLineLogicallyComplete(Cell[] line, int[] hints) {
            int currentCount = 0;
            IList<int> segments = new List<int>();

            for (int i = 0; i < line.Length; i++) {
                if (line[i] == Cell.Filled) {
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

        private bool IsLineFull(Cell[] line) {
            return line.All(i => i != Cell.Unknown);
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

        private T[] CopyAndReverse<T>(T[] line) {
            T[] reversedLine = new T[line.Length];

            for (int i = 0; i < line.Length; i++) {
                reversedLine[line.Length - i - 1] = line[i];
            }

            return reversedLine;
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

        private void Print() {
            Console.WriteLine("Iteration " + iteration);

            for (int row = 0; row < rowHints.Length; row++) {
                for (int col = 0; col < columnHints.Length; col++) {
                    string character;

                    switch (map[row, col]) {
                        case Cell.Blank:
                            character = "*";
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
        }
    }
}
