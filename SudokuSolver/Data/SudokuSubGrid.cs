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
        public int TopMiddle { get; set; }
        public int TopRight { get; set; }

        // Middle

        public int MiddleLeft { get; set; }
        public int MiddleMiddle { get; set; }
        public int MiddleRight { get; set; }

        // Bottom

        public int BottomLeft { get; set; }
        public int BottomMiddle { get; set; }
        public int BottomRight { get; set; }

        #endregion
    }
}
