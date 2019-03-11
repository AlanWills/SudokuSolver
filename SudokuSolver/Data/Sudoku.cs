using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Data
{
    public class Sudoku
    {
        #region Properties and Fields

        // Top

        public SudokuSubGrid TopLeft { get; private set; } = new SudokuSubGrid();
        public SudokuSubGrid TopMiddle { get; private set; } = new SudokuSubGrid();
        public SudokuSubGrid TopRight { get; private set; } = new SudokuSubGrid();

        // Middle

        public SudokuSubGrid MiddleLeft { get; private set; } = new SudokuSubGrid();
        public SudokuSubGrid MiddleMiddle { get; private set; } = new SudokuSubGrid();
        public SudokuSubGrid MiddleRight { get; private set; } = new SudokuSubGrid();

        // Bottom

        public SudokuSubGrid BottomLeft { get; private set; } = new SudokuSubGrid();
        public SudokuSubGrid BottomMiddle { get; private set; } = new SudokuSubGrid();
        public SudokuSubGrid BottomRight { get; private set; } = new SudokuSubGrid();

        #endregion
    }
}