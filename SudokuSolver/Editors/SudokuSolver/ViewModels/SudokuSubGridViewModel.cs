using SudokuSolver.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Editors.SudokuSolver.ViewModels
{
    public class SudokuSubGridViewModel
    {
        #region Properties and Fields

        public SudokuSubGrid SudokuSubGrid { get; }

        // Top
        public SudokuElementViewModel TopLeft { get; }
        public SudokuElementViewModel TopMiddle { get; }
        public SudokuElementViewModel TopRight { get; }

        // Middle
        public SudokuElementViewModel MiddleLeft { get; }
        public SudokuElementViewModel MiddleMiddle { get; }
        public SudokuElementViewModel MiddleRight { get; }

        // Bottom
        public SudokuElementViewModel BottomLeft { get; }
        public SudokuElementViewModel BottomMiddle { get; }
        public SudokuElementViewModel BottomRight { get; }

        #endregion

        public SudokuSubGridViewModel(SudokuSubGrid sudokuSubGrid)
        {
            SudokuSubGrid = sudokuSubGrid;

            TopLeft = new SudokuElementViewModel(sudokuSubGrid.TopLeft);
            TopMiddle = new SudokuElementViewModel(sudokuSubGrid.TopMiddle);
            TopRight = new SudokuElementViewModel(sudokuSubGrid.TopRight);

            MiddleLeft = new SudokuElementViewModel(sudokuSubGrid.MiddleLeft);
            MiddleMiddle = new SudokuElementViewModel(sudokuSubGrid.MiddleMiddle);
            MiddleRight = new SudokuElementViewModel(sudokuSubGrid.MiddleRight);

            BottomLeft = new SudokuElementViewModel(sudokuSubGrid.BottomLeft);
            BottomMiddle = new SudokuElementViewModel(sudokuSubGrid.BottomMiddle);
            BottomRight = new SudokuElementViewModel(sudokuSubGrid.BottomRight);
        }
    }
}