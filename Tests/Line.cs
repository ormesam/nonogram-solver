using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NonogramSolver;

namespace Tests {
    [TestClass]
    public class Line {
        [TestMethod]
        public void AlreadySolved() {
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

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            var result = nonogram.Solve(line, hints);

            Assert.IsTrue(line.SequenceEqual(result));
        }

        [TestMethod]
        public void FullLine() {
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

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            var result = nonogram.Solve(line, hints);

            Assert.IsTrue(result.All(i => i == Cell.Filled));
        }

        [TestMethod]
        public void EmptyLine() {
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

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            var result = nonogram.Solve(line, hints);

            Assert.IsTrue(result.All(i => i == Cell.Blank));
        }

        [TestMethod]
        public void PartiallyFullKnownLine() {
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

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            var result = nonogram.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void OverlappingLine() {
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

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            var result = nonogram.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void PartiallyKnownLine() {
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

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            var result = nonogram.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void PartiallyKnownLine2() {
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

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            var result = nonogram.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void RestrictedLine() {
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

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            var result = nonogram.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void RestrictedLine2() {
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

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            var result = nonogram.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void LineSingleKnown() {
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

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            var result = nonogram.Solve(line, hints);

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }
    }
}
