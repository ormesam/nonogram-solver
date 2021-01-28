using System.Collections.Generic;
using System.Linq;

namespace NonogramSolver {
    public class LineSolver {
        private readonly IList<CellValue[]> currentLinePermutations;

        internal IList<CellValue[]> CurrentLinePermutations => currentLinePermutations;

        public LineSolver() {
            currentLinePermutations = new List<CellValue[]>();
        }

        internal CellValue[] Solve(CellValue[] line, int[] hints) {
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

        private IList<CellValue[]> FilterPermutations(CellValue[] line) {
            List<CellValue[]> validPermutations = new List<CellValue[]>();

            foreach (var permutation in currentLinePermutations) {
                bool isValid = true;

                for (int i = 0; i < permutation.Length; i++) {
                    CellValue cellValue = line[i];
                    CellValue permutationValue = permutation[i];

                    if (cellValue != CellValue.Unknown && cellValue != permutationValue) {
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
            CellValue[] line = new CellValue[length];

            for (int i = 0; i < length; i++) {
                line[i] = CellValue.Unknown;
            }

            GeneratePermutations(line, 0, new Queue<int>(hints));
        }

        private void GeneratePermutations(CellValue[] line, int startIdx, Queue<int> hints) {
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
                FillCells(clone, i, hint, CellValue.Filled);

                GeneratePermutations(clone, i + hint + 1, new Queue<int>(hints));
            }
        }

        internal void Merge(CellValue[] line, IList<CellValue[]> permutations) {
            if (permutations.Count == 0) {
                return;
            }

            for (int i = 0; i < line.Length; i++) {
                if (line[i] != CellValue.Unknown) {
                    continue;
                }

                var value = permutations[0][i];

                bool allMatch = permutations.All(p => p[i] == value);

                if (allMatch) {
                    line[i] = value;
                }
            }
        }

        private void FillEmptyCells(CellValue[] line) {
            for (int i = 0; i < line.Length; i++) {
                if (line[i] == CellValue.Unknown) {
                    line[i] = CellValue.Blank;
                }
            }
        }

        private void FillCells(CellValue[] line, int startIdx, int numberOfCells, CellValue value) {
            if (numberOfCells == 0) {
                return;
            }

            for (int i = startIdx; i < startIdx + numberOfCells; i++) {
                line[i] = value;
            }
        }

        public bool IsLineFull(CellValue[] line) {
            return line.All(i => i != CellValue.Unknown);
        }

        public bool IsLineLogicallyComplete(CellValue[] line, int[] hints) {
            int currentCount = 0;
            IList<int> segments = new List<int>();

            for (int i = 0; i < line.Length; i++) {
                if (line[i] == CellValue.Filled) {
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
