using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Data
{
    public class SudokuSubGrid
    {
        #region Properties and Fields

        // Top

        public SudokuElement TopLeft { get; private set; } = new SudokuElement();
        public SudokuElement TopMiddle { get; private set; } = new SudokuElement();
        public SudokuElement TopRight { get; private set; } = new SudokuElement();

        // Middle

        public SudokuElement MiddleLeft { get; private set; } = new SudokuElement();
        public SudokuElement MiddleMiddle { get; private set; } = new SudokuElement();
        public SudokuElement MiddleRight { get; private set; } = new SudokuElement();

        // Bottom

        public SudokuElement BottomLeft { get; private set; } = new SudokuElement();
        public SudokuElement BottomMiddle { get; private set; } = new SudokuElement();
        public SudokuElement BottomRight { get; private set; } = new SudokuElement();

        #endregion
    }
}
