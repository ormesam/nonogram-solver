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
            if (IsLineFull(line)) {
                return line;
            }

            var clone = line.ToArray();

            // If line is empty
            if (hints.Length <= 1 && hints[0] == 0) {
                FillEmptyCells(clone);
                return clone;
            }

            IList<IList<Cell>> valid_permutations = new List<IList<Cell>>();
            var permitations = Permutations(hints, line);

            foreach (var permutation in permitations) {
                for (int i = 0; i < (rowHints.Length - permutation.Count); i++) {
                    permutation.Add(Cell.Filled);
                }

                foreach (var pair in Enumerable.Zip(clone, permutation)) {
                    if (pair.First > 0 && pair.First != pair.Second) {
                        break;
                    }

                    valid_permutations.Add(permutation);
                }
            }

            var newRow = valid_permutations[0];

            for (int i = 1; i < valid_permutations.Count; i++) {
                var permutation = valid_permutations[i];

                int idx = 0;
                var zip = Enumerable.Zip(newRow, permutation).ToList();

                foreach (var r in zip) {
                    if (r.First == r.Second) {
                        newRow[idx] = r.First;
                    } else {
                        newRow[idx] = Cell.Unknown;
                    }

                    idx++;
                }
            }

            return newRow.ToArray();

            //if (IsLineLogicallyComplete(line, hints)) {
            //    FillEmptyCells(line);
            //}

            //return clone;
        }

        public IEnumerable<IList<Cell>> Permutations(int[] values, Cell[] row, int n = 0) {
            if (values.Length != 0) {

                int current = values[0];
                int[] other = values.Length == 1 ? new int[0] : values[1..(values.Length - 1)];

                for (int i = 0; i < (row.Length - other.Sum() - other.Length + 1 - current); i++) {
                    if (!row[i..(i + current)].Contains(Cell.Filled)) {
                        int j = 0;

                        foreach (var item in Permutations(other, row[(i + current - 1)..(row.Length - 1)], 1)) {
                            IList<Cell> list = new List<Cell>();

                            int array1Length = i + n;
                            int array2Length = current + j;

                            for (int k = 0; k < array1Length; k++) {
                                list.Add(Cell.Blank);
                            }

                            for (int k = 0; k < array2Length; k++) {
                                list.Add(Cell.Blank);
                            }

                            yield return list;

                            j++;
                        }
                    }
                }
            } else {
                yield return new List<Cell>();
            }
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

        private void SolveSegment(Cell[] lineSegment, Queue<int> hintQueue) {
            var hints = hintQueue.ToArray();


            // https://www.reddit.com/r/dailyprogrammer/comments/am1x6o/20190201_challenge_374_hard_nonogram_solver/




            for (int i = 0; i < hints.Length; i++) {
                int hint = hints[0];

                // work our way along the hints
            }

            var clone = lineSegment.ToArray();

            int startIdx = 0;

            for (int i = 0; i < lineSegment.Length; i++) {

            }

            for (int i = 0; i < hints.Length; i++) {
                FillCells(clone, startIdx, hints[i], Cell.Filled);
                startIdx += hints[i];

                FillCells(clone, startIdx, 1, Cell.Blank);
                startIdx++;
            }

            Cell[] reversed = clone.ToArray().Reverse().ToArray();

            for (int i = 0; i < clone.Length; i++) {
                if (clone[i] == reversed[i] && clone[i] == Cell.Filled) { // && i < hints[0]?
                    FillCells(lineSegment, i, 1, Cell.Filled);
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
