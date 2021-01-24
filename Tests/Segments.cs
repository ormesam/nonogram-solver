using Microsoft.VisualStudio.TestTools.UnitTesting;
using NonogramSolver;

namespace Tests {
    [TestClass]
    public class Segments {
        [TestMethod]
        public void SingleEmptySegments() {
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

            int startIdx = 0;
            int endIdx = 0;

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            nonogram.NextSegment(line, ref startIdx, ref endIdx);

            Assert.AreEqual(0, startIdx);
            Assert.AreEqual(7, endIdx);
        }

        [TestMethod]
        public void SingleSegments() {
            Cell[] line = new Cell[8] {
                Cell.Blank,
                Cell.Filled,
                Cell.Unknown,
                Cell.Filled,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Blank,
            };

            int startIdx = 0;
            int endIdx = 0;

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            nonogram.NextSegment(line, ref startIdx, ref endIdx);

            Assert.AreEqual(1, startIdx);
            Assert.AreEqual(6, endIdx);
        }

        [TestMethod]
        public void TwoSegments() {
            Cell[] line = new Cell[8] {
                Cell.Blank,
                Cell.Filled,
                Cell.Unknown,
                Cell.Filled,
                Cell.Blank,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Blank,
            };

            int startIdx = 0;
            int endIdx = 0;

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            nonogram.NextSegment(line, ref startIdx, ref endIdx);

            Assert.AreEqual(1, startIdx);
            Assert.AreEqual(3, endIdx);

            nonogram.NextSegment(line, ref startIdx, ref endIdx);

            Assert.AreEqual(5, startIdx);
            Assert.AreEqual(6, endIdx);
        }

        [TestMethod]
        public void ThreeSegments() {
            Cell[] line = new Cell[10] {
                Cell.Blank,
                Cell.Filled,
                Cell.Unknown,
                Cell.Filled,
                Cell.Blank,
                Cell.Unknown,
                Cell.Unknown,
                Cell.Blank,
                Cell.Unknown,
                Cell.Blank,
            };

            int startIdx = 0;
            int endIdx = 0;

            Nonogram nonogram = new Nonogram(new int[0][], new int[0][]);
            nonogram.NextSegment(line, ref startIdx, ref endIdx);

            Assert.AreEqual(1, startIdx);
            Assert.AreEqual(3, endIdx);

            nonogram.NextSegment(line, ref startIdx, ref endIdx);

            Assert.AreEqual(5, startIdx);
            Assert.AreEqual(6, endIdx);

            nonogram.NextSegment(line, ref startIdx, ref endIdx);

            Assert.AreEqual(8, startIdx);
            Assert.AreEqual(8, endIdx);
        }
    }
}
