using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NonogramSolver;

namespace Tests {
    [TestClass]
    public class Permutations {
        [TestMethod]
        public void Merge() {
            Cell[] permutation1 = new Cell[8] {
                Cell.Unknown,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Unknown,
                Cell.Unknown,
            };

            Cell[] permutation2 = new Cell[8] {
                Cell.Filled,
                Cell.Unknown,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Unknown,
                Cell.Filled,
            };

            var permutations = new List<Cell[]> {
                permutation1,
                permutation2,
            };

            Cell[] expectedResult = new Cell[8] {
                Cell.Unknown,
                Cell.Unknown,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Unknown,
                Cell.Unknown,
            };

            var clone = new Cell[8] {
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
            };

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            nonogram.Merge(clone, permutations);

            Assert.IsTrue(expectedResult.SequenceEqual(clone));
        }

        [TestMethod]
        public void GeneratePermutations_Singles() {
            Cell[] line = new Cell[8] {
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
            };

            int[] hints = { 1 };

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            nonogram.GeneratePermutations(line, hints);
            var generatedPermutations = nonogram.CurrentLinePermutations;

            Assert.AreEqual(8, generatedPermutations.Count);

            for (int i = 0; i < generatedPermutations.Count; i++) {
                Assert.IsTrue(generatedPermutations[i][i] == Cell.Filled);
            }
        }

        [TestMethod]
        public void GeneratePermutations_MultipleHintFill() {
            Cell[] line = new Cell[8] {
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
            };

            int[] hints = { 2, 1, 1, 1 };

            Cell[] permutation1 = new Cell[8] {
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
                Cell.Filled,
                Cell.Blank,
                Cell.Filled,
                Cell.Blank,
                Cell.Filled,
            };

            var permutations = new List<Cell[]> {
                permutation1,
            };

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            nonogram.GeneratePermutations(line, hints);
            var generatedPermutations = nonogram.CurrentLinePermutations;

            Assert.AreEqual(1, generatedPermutations.Count);
            Assert.IsTrue(permutations[0].SequenceEqual(generatedPermutations[0]));
        }

        [TestMethod]
        public void GeneratePermutations_SingleHint() {
            Cell[] line = new Cell[8] {
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
            };

            int[] hints = { 6 };

            Cell[] permutation1 = new Cell[8] {
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
                Cell.Blank,
            };

            Cell[] permutation2 = new Cell[8] {
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
            };

            Cell[] permutation3 = new Cell[8] {
                Cell.Blank,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
            };

            var permutations = new List<Cell[]> {
                permutation1,
                permutation2,
                permutation3,
            };

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            nonogram.GeneratePermutations(line, hints);
            var generatedPermutations = nonogram.CurrentLinePermutations;

            Assert.AreEqual(3, generatedPermutations.Count);

            for (int i = 0; i < generatedPermutations.Count; i++) {
                Assert.IsTrue(permutations.Any(p => p.SequenceEqual(generatedPermutations[i])));
            }
        }

        [TestMethod]
        public void GeneratePermutations_DoubleHint_Fill() {
            Cell[] line = new Cell[8] {
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
            };

            int[] hints = { 4, 3 };

            Cell[] permutation1 = new Cell[8] {
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
            };

            var permutations = new List<Cell[]> {
                permutation1,
            };

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            nonogram.GeneratePermutations(line, hints);
            var generatedPermutations = nonogram.CurrentLinePermutations;

            Assert.AreEqual(1, generatedPermutations.Count);
            Assert.IsTrue(permutations[0].SequenceEqual(generatedPermutations[0]));
        }

        [TestMethod]
        public void GeneratePermutations_DoubleHint_PartialFill() {
            Cell[] line = new Cell[8] {
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
            };

            int[] hints = { 3, 3 };

            Cell[] permutation1 = new Cell[8] {
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
            };

            Cell[] permutation2 = new Cell[8] {
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
            };

            Cell[] permutation3 = new Cell[8] {
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
            };

            var permutations = new List<Cell[]> {
                permutation1,
                permutation2,
                permutation3,
            };

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            nonogram.GeneratePermutations(line, hints);
            var generatedPermutations = nonogram.CurrentLinePermutations;

            Assert.AreEqual(3, generatedPermutations.Count);

            for (int i = 0; i < generatedPermutations.Count; i++) {
                Assert.IsTrue(permutations.Any(p => p.SequenceEqual(generatedPermutations[i])));
            }
        }

        [TestMethod]
        public void GeneratePermutations_DoubleHint_PartialFill2() {
            Cell[] line = new Cell[8] {
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
            };

            int[] hints = { 2, 3 };

            Cell[] permutation1 = new Cell[8] {
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
                Cell.Blank,
            };

            Cell[] permutation2 = new Cell[8] {
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
            };

            Cell[] permutation3 = new Cell[8] {
                Cell.Blank,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
            };

            Cell[] permutation4 = new Cell[8] {
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
            };

            Cell[] permutation5 = new Cell[8] {
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
                Cell.Blank,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
            };

            Cell[] permutation6 = new Cell[8] {
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
            };

            var permutations = new List<Cell[]> {
                permutation1,
                permutation2,
                permutation3,
                permutation4,
                permutation5,
                permutation6,
            };

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            nonogram.GeneratePermutations(line, hints);
            var generatedPermutations = nonogram.CurrentLinePermutations;

            Assert.AreEqual(6, generatedPermutations.Count);

            for (int i = 0; i < generatedPermutations.Count; i++) {
                Assert.IsTrue(permutations.Any(p => p.SequenceEqual(generatedPermutations[i])));
            }
        }

        [TestMethod]
        public void GeneratePermutations_TripleHint_PartialFill() {
            Cell[] line = new Cell[8] {
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
            };

            int[] hints = { 1, 2, 2 };

            Cell[] permutation1 = new Cell[8] {
                Cell.Filled,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
            };

            Cell[] permutation2 = new Cell[8] {
                Cell.Blank,
                Cell.Filled,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
            };

            Cell[] permutation3 = new Cell[8] {
                Cell.Filled,
                Cell.Blank,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
            };

            Cell[] permutation4 = new Cell[8] {
                Cell.Filled,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
            };

            var permutations = new List<Cell[]> {
                permutation1,
                permutation2,
                permutation3,
                permutation4,
            };

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            nonogram.GeneratePermutations(line, hints);
            var generatedPermutations = nonogram.CurrentLinePermutations;

            Assert.AreEqual(4, generatedPermutations.Count);

            for (int i = 0; i < generatedPermutations.Count; i++) {
                Assert.IsTrue(permutations.Any(p => p.SequenceEqual(generatedPermutations[i])));
            }
        }

        [TestMethod]
        public void GeneratePermutations_DoubleHint_LargeGap() {
            Cell[] line = new Cell[8] {
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
            };

            int[] hints = { 2, 2 };

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            nonogram.GeneratePermutations(line, hints);
            var generatedPermutations = nonogram.CurrentLinePermutations;

            Assert.AreEqual(10, generatedPermutations.Count);
        }
    }
}
