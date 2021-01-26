using System;
using System.Collections.Generic;
using System.Linq;

namespace NonogramSolver {
    public class Nonogram {
        private readonly int[][] rowHints;
        private readonly int[][] columnHints;
        private readonly Cell[,] map;
        private int iteration;
        private IList<Cell[]> currentLinePermutations = new List<Cell[]>();

        public IList<Cell[]> CurrentLinePermutations => currentLinePermutations;

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
            bool hasChanged = true;

            while (hasChanged) {
                hasChanged = false;

                for (int row = 0; row < rowHints.Length; row++) {
                    var currentRow = GetRow(row);
                    var updatedRow = Solve(GetRow(row), rowHints[row]);

                    bool hasLineChanged = !currentRow.SequenceEqual(updatedRow);

                    if (hasLineChanged) {
                        ReplaceRow(row, updatedRow);

                        hasChanged = true;
                    }
                }

                for (int col = 0; col < columnHints.Length; col++) {
                    var currentColumn = GetColumn(col);
                    var updatedColumn = Solve(GetColumn(col), columnHints[col]);

                    bool hasLineChanged = !currentColumn.SequenceEqual(updatedColumn);

                    if (hasLineChanged) {
                        ReplaceColumn(col, updatedColumn);

                        hasChanged = true;
                    }
                }

                Print();

                iteration++;
            }

            bool isSolved = IsSolved();

            return Convert();
        }

        public Cell[] Solve(Cell[] line, int[] hints) {
            currentLinePermutations.Clear();

            if (IsLineFull(line)) {
                return line;
            }

            var clone = line.ToArray();

            // If line is empty
            if (hints.Length <= 1 && hints[0] == 0) {
                FillEmptyCells(clone);
                return clone;
            }

            GeneratePermutations(line.Length, hints);
            var filteredPermutations = FilterPermutations(clone);
            Merge(clone, filteredPermutations);

            return clone;
        }

        private IList<Cell[]> FilterPermutations(Cell[] line) {
            List<Cell[]> validPermutations = new List<Cell[]>();

            foreach (var permutation in currentLinePermutations) {
                bool isValid = true;

                for (int i = 0; i < permutation.Length; i++) {
                    Cell cellValue = line[i];
                    Cell permutationValue = permutation[i];

                    if (cellValue != Cell.Unknown && cellValue != permutationValue) {
                        isValid = false;
                        break;
                    }
                }

                if (isValid) {
                    validPermutations.Add(permutation);
                }
            }

            return validPermutations;
        }

        public void GeneratePermutations(Cell[] line, int[] hints) {
            GeneratePermutations(line.Length, hints);
        }

        public void GeneratePermutations(int length, int[] hints) {
            Cell[] line = new Cell[length];

            for (int i = 0; i < length; i++) {
                line[i] = Cell.Unknown;
            }

            GeneratePermutations(line, 0, new Queue<int>(hints));
        }

        private void GeneratePermutations(Cell[] line, int startIdx, Queue<int> hints) {
            if (!hints.Any()) {
                FillEmptyCells(line);
                currentLinePermutations.Add(line.ToArray());

                return;
            }

            var hint = hints.Dequeue();

            // This maximum index this hint can be and still fit the others on
            int maxStartingIndex = line.Length - hints.Sum() - hints.Count - hint + 1;

            for (int i = startIdx; i < maxStartingIndex; i++) {
                var clone = line.ToArray();
                FillCells(clone, i, hint, Cell.Filled);

                GeneratePermutations(clone, i + hint + 1, new Queue<int>(hints));
            }
        }

        public void Merge(Cell[] line, IList<Cell[]> permitations) {
            if (permitations.Count == 0) {
                return;
            }

            for (int i = 0; i < line.Length; i++) {
                if (line[i] != Cell.Unknown) {
                    continue;
                }

                var value = permitations[0][i];

                bool allMatch = permitations.All(p => p[i] == value);

                if (allMatch) {
                    line[i] = value;
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
            if (numberOfCells == 0) {
                return;
            }

            for (int i = startIdx; i < startIdx + numberOfCells; i++) {
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
