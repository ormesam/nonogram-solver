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
            CellValue[] line = new CellValue[8] {
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
            };

            int[] hints = { 8 };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(line.SequenceEqual(result));
        }

        [TestMethod]
        public void Line_Fill() {
            CellValue[] line = new CellValue[8] {
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
            };

            int[] hints = { 8 };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(result.All(i => i == CellValue.Filled));
        }

        [TestMethod]
        public void Line_Empty() {
            CellValue[] line = new CellValue[8] {
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
            };

            int[] hints = { 0 };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(result.All(i => i == CellValue.Blank));
        }

        [TestMethod]
        public void Line_PartiallyKnown_Full() {
            CellValue[] line = new CellValue[8] {
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
            };

            int[] hints = { 4, 3 };

            CellValue[] expectedResult = new CellValue[8] {
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Filled,
            };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void Line_Overlapping() {
            CellValue[] line = new CellValue[8] {
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
            };

            int[] hints = { 3, 3 };

            CellValue[] expectedResult = new CellValue[8] {
                CellValue.Unknown,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Unknown,
            };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void Line_PartiallyKnown() {
            CellValue[] line = new CellValue[8] {
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
            };

            int[] hints = { 2, 2 };

            CellValue[] expectedResult = new CellValue[8] {
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
            };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void Line_PartiallyKnown2() {
            CellValue[] line = new CellValue[8] {
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
            };

            int[] hints = { 2, 2, 1 };

            CellValue[] expectedResult = new CellValue[8] {
                CellValue.Unknown,
                CellValue.Filled,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Filled,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
            };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void Line_Restricted() {
            CellValue[] line = new CellValue[8] {
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Filled,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Blank,
            };

            int[] hints = { 3 };

            CellValue[] expectedResult = new CellValue[8] {
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Filled,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Blank,
            };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void Line_Restricted2() {
            CellValue[] line = new CellValue[8] {
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Unknown,
                CellValue.Filled,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Blank,
            };

            int[] hints = { 3 };

            CellValue[] expectedResult = new CellValue[8] {
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Unknown,
                CellValue.Filled,
                CellValue.Filled,
                CellValue.Unknown,
                CellValue.Blank,
            };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void Line_SingleKnown() {
            CellValue[] line = new CellValue[8] {
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Filled,
                CellValue.Unknown,
                CellValue.Unknown,
                CellValue.Unknown,
            };

            int[] hints = { 1 };

            CellValue[] expectedResult = new CellValue[8] {
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Filled,
                CellValue.Blank,
                CellValue.Blank,
                CellValue.Blank,
            };

            var result = lineSolver.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }
    }
}
