using System.Collections.Generic;
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
            CellValue[] permutation1 = new CellValue[8] {
                CellValue.Unknown,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Unknown,
                CellValue.Unknown,
            };

            CellValue[] permutation2 = new CellValue[8] {
                CellValue.Filled,
                CellValue.Unknown,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Unknown,
                CellValue.Filled,
            };

            var permutations = new List<CellValue[]> {
                permutation1,
                permutation2,
            };

            CellValue[] expectedResult = new CellValue[8] {
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Unknown,
                CellValue.Unknown,
            };

            var clone = new CellValue[8] {
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
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
                Assert.IsTrue(generatedPermutations[i][i] == CellValue.Filled);
            }
        }

        [TestMethod]
        public void Permutations_MultipleHintFill() {
            int[] hints = { 2, 1, 1, 1 };

            CellValue[] permutation1 = new CellValue[8] {
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Filled,
            };

            var permutations = new List<CellValue[]> {
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

            CellValue[] permutation1 = new CellValue[8] {
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Blank,
            };

            CellValue[] permutation2 = new CellValue[8] {
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
            };

            CellValue[] permutation3 = new CellValue[8] {
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
            };

            var permutations = new List<CellValue[]> {
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

            CellValue[] permutation1 = new CellValue[8] {
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
            };

            var permutations = new List<CellValue[]> {
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

            CellValue[] permutation1 = new CellValue[8] {
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
            };

            CellValue[] permutation2 = new CellValue[8] {
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
            };

            CellValue[] permutation3 = new CellValue[8] {
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
            };

            var permutations = new List<CellValue[]> {
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

            CellValue[] permutation1 = new CellValue[8] {
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Blank,
            };

            CellValue[] permutation2 = new CellValue[8] {
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
            };

            CellValue[] permutation3 = new CellValue[8] {
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
            };

            CellValue[] permutation4 = new CellValue[8] {
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
            };

            CellValue[] permutation5 = new CellValue[8] {
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
            };

            CellValue[] permutation6 = new CellValue[8] {
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
            };

            var permutations = new List<CellValue[]> {
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

            CellValue[] permutation1 = new CellValue[8] {
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
            };

            CellValue[] permutation2 = new CellValue[8] {
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
            };

            CellValue[] permutation3 = new CellValue[8] {
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
            };

            CellValue[] permutation4 = new CellValue[8] {
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
            };

            var permutations = new List<CellValue[]> {
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
