using System;

namespace NonogramSolver {
    public class NonogramSolveResult {
        public bool IsSolved { get; set; }
        public int[,] Result { get; set; }
        public TimeSpan TimeTaken { get; set; }
        public int Iterations { get; set; }
    }
}
