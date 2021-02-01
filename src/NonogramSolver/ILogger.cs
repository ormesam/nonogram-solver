namespace NonogramSolver {
    public interface ILogger {
        void LineSolved(int idx, bool isRow, CellValue[] line);
    }
}
