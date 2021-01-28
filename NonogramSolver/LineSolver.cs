using System.Collections.Generic;
using System.Linq;

namespace NonogramSolver {
    internal class LineSolver {
        private readonly IList<Cell[]> currentLinePermutations;

        internal IList<Cell[]> CurrentLinePermutations => currentLinePermutations;

        internal LineSolver() {
            currentLinePermutations = new List<Cell[]>();
        }

        internal Cell[] Solve(Cell[] line, int[] hints) {
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

        internal void GeneratePermutations(int length, int[] hints) {
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

        internal void Merge(Cell[] line, IList<Cell[]> permutations) {
            if (permutations.Count == 0) {
                return;
            }

            for (int i = 0; i < line.Length; i++) {
                if (line[i] != Cell.Unknown) {
                    continue;
                }

                var value = permutations[0][i];

                bool allMatch = permutations.All(p => p[i] == value);

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

        private bool IsLineFull(Cell[] line) {
            return line.All(i => i != Cell.Unknown);
        }

        internal bool IsLineLogicallyComplete(Cell[] line, int[] hints) {
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
    }
}
