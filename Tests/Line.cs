using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NonogramSolver;

namespace Tests {
    [TestClass]
    public class Line {
        private LineSolver lineSolver;

        [TestInitialize]
        public void Initialize() {
            lineSolver = new LineSolver();
        }

        [TestMethod]
        public void Line_Complete() {
            Cell[] line = new Cell[8] {
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
            };

            int[] hints = { 8 };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(line.SequenceEqual(result));
        }

        [TestMethod]
        public void Line_Fill() {
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

            int[] hints = { 8 };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(result.All(i => i == Cell.Filled));
        }

        [TestMethod]
        public void Line_Empty() {
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

            int[] hints = { 0 };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(result.All(i => i == Cell.Blank));
        }

        [TestMethod]
        public void Line_PartiallyKnown_Full() {
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

            Cell[] expectedResult = new Cell[8] {
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
                Cell.Filled,
                Cell.Filled,
                Cell.Filled,
            };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void Line_Overlapping() {
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

            Cell[] expectedResult = new Cell[8] {
                Cell.Unknown,
                Cell.Filled,
                Cell.Filled,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Filled,
                Cell.Filled,
                Cell.Unknown,
            };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void Line_PartiallyKnown() {
            Cell[] line = new Cell[8] {
                Cell.Unknown,
                Cell.Unknown,
                Cell.Blank,
                Cell.Blank,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
            };

            int[] hints = { 2, 2 };

            Cell[] expectedResult = new Cell[8] {
                Cell.Filled,
                Cell.Filled,
                Cell.Blank,
                Cell.Blank,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
            };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void Line_PartiallyKnown2() {
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

            int[] hints = { 2, 2, 1 };

            Cell[] expectedResult = new Cell[8] {
                Cell.Unknown,
                Cell.Filled,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Filled,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
            };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void Line_Restricted() {
            Cell[] line = new Cell[8] {
                Cell.Blank,
                Cell.Blank,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Filled,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Blank,
            };

            int[] hints = { 3 };

            Cell[] expectedResult = new Cell[8] {
                Cell.Blank,
                Cell.Blank,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Filled,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Blank,
            };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void Line_Restricted2() {
            Cell[] line = new Cell[8] {
                Cell.Blank,
                Cell.Blank,
                Cell.Blank,
                Cell.Unknown,
                Cell.Filled,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Blank,
            };

            int[] hints = { 3 };

            Cell[] expectedResult = new Cell[8] {
                Cell.Blank,
                Cell.Blank,
                Cell.Blank,
                Cell.Unknown,
                Cell.Filled,
                Cell.Filled,
                Cell.Unknown,
                Cell.Blank,
            };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void Line_SingleKnown() {
            Cell[] line = new Cell[8] {
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Filled,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
            };

            int[] hints = { 1 };

            Cell[] expectedResult = new Cell[8] {
                Cell.Blank,
                Cell.Blank,
                Cell.Blank,
                Cell.Blank,
                Cell.Filled,
                Cell.Blank,
                Cell.Blank,
                Cell.Blank,
            };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }
    }
}
