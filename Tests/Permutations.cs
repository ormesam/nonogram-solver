﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NonogramSolver;

namespace Tests {
    [TestClass]
    public class Permutations {
        private LineSolver lineSolver;

        [TestInitialize]
        public void Initialize() {
            lineSolver = new LineSolver();
        }

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

            lineSolver.Merge(clone, permutations);

            Assert.IsTrue(expectedResult.SequenceEqual(clone));
        }

        [TestMethod]
        public void Permutations_Singles() {
            int[] hints = { 1 };

            lineSolver.GeneratePermutations(8, hints);
            var generatedPermutations = lineSolver.CurrentLinePermutations;

            Assert.AreEqual(8, generatedPermutations.Count);

            for (int i = 0; i < generatedPermutations.Count; i++) {
                Assert.IsTrue(generatedPermutations[i][i] == Cell.Filled);
            }
        }

        [TestMethod]
        public void Permutations_MultipleHintFill() {
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

            lineSolver.GeneratePermutations(8, hints);
            var generatedPermutations = lineSolver.CurrentLinePermutations;

            Assert.AreEqual(1, generatedPermutations.Count);
            Assert.IsTrue(permutations[0].SequenceEqual(generatedPermutations[0]));
        }

        [TestMethod]
        public void Permutations_SingleHint() {
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

            lineSolver.GeneratePermutations(8, hints);
            var generatedPermutations = lineSolver.CurrentLinePermutations;

            Assert.AreEqual(3, generatedPermutations.Count);

            for (int i = 0; i < generatedPermutations.Count; i++) {
                Assert.IsTrue(permutations.Any(p => p.SequenceEqual(generatedPermutations[i])));
            }
        }

        [TestMethod]
        public void Permutations_DoubleHint_Fill() {
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

            lineSolver.GeneratePermutations(8, hints);
            var generatedPermutations = lineSolver.CurrentLinePermutations;

            Assert.AreEqual(1, generatedPermutations.Count);
            Assert.IsTrue(permutations[0].SequenceEqual(generatedPermutations[0]));
        }

        [TestMethod]
        public void Permutations_DoubleHint_PartialFill() {
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

            lineSolver.GeneratePermutations(8, hints);
            var generatedPermutations = lineSolver.CurrentLinePermutations;

            Assert.AreEqual(3, generatedPermutations.Count);

            for (int i = 0; i < generatedPermutations.Count; i++) {
                Assert.IsTrue(permutations.Any(p => p.SequenceEqual(generatedPermutations[i])));
            }
        }

        [TestMethod]
        public void Permutations_DoubleHint_PartialFill2() {
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

            lineSolver.GeneratePermutations(8, hints);
            var generatedPermutations = lineSolver.CurrentLinePermutations;

            Assert.AreEqual(6, generatedPermutations.Count);

            for (int i = 0; i < generatedPermutations.Count; i++) {
                Assert.IsTrue(permutations.Any(p => p.SequenceEqual(generatedPermutations[i])));
            }
        }

        [TestMethod]
        public void Permutations_TripleHint_PartialFill() {
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

            lineSolver.GeneratePermutations(8, hints);
            var generatedPermutations = lineSolver.CurrentLinePermutations;

            Assert.AreEqual(4, generatedPermutations.Count);

            for (int i = 0; i < generatedPermutations.Count; i++) {
                Assert.IsTrue(permutations.Any(p => p.SequenceEqual(generatedPermutations[i])));
            }
        }

        [TestMethod]
        public void Permutations_DoubleHint_LargeGap() {
            int[] hints = { 2, 2 };

            lineSolver.GeneratePermutations(8, hints);
            var generatedPermutations = lineSolver.CurrentLinePermutations;

            Assert.AreEqual(10, generatedPermutations.Count);
        }
    }
}
