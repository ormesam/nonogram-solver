using System.Threading;
using NonogramSolver;

namespace Console {
    public class ConsoleLogger : ILogger {
        public void LineSolved(int idx, bool isRow, CellValue[] line) {
            for (int i = 0; i < line.Length; i++) {
                SetCursorPosition(isRow ? i : idx, isRow ? idx : i);

                System.Console.Write(GetDisplayValue(line[i]));

                if (isRow) {
                    System.Console.Write(" ");
                }

                Thread.Sleep(5);
            }
        }

        private void SetCursorPosition(int left, int top) {
            System.Console.SetCursorPosition(left * 2, top); ;
        }

        private string GetDisplayValue(CellValue value) {
            string character;

            switch (value) {
                case CellValue.Blank:
                    character = " ";
                    break;
                case CellValue.Filled:
                    character = "X";
                    break;
                default:
                    character = "-";
                    break;
            }

            return character;
        }
    }
}
