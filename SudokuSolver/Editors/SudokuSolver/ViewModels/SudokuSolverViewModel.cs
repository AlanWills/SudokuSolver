using CelesteEngineEditor.Editors;
using SudokuSolver.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Editors.SudokuSolver.ViewModels
{
    public class SudokuSolverViewModel : EditorViewModel
    {
        #region Properties and Fields

        private Sudoku sudoku;
        public Sudoku Sudoku
        {
            get
            {
                sudoku = sudoku ?? TargetObject as Sudoku;
                return sudoku;
            }
        }

        // Top
        public SudokuSubGridViewModel TopLeft { get; }
        public SudokuSubGridViewModel TopMiddle { get; }
        public SudokuSubGridViewModel TopRight { get; }

        // Middle
        public SudokuSubGridViewModel MiddleLeft { get; }
        public SudokuSubGridViewModel MiddleMiddle { get; }
        public SudokuSubGridViewModel MiddleRight { get; }

        // Bottom
        public SudokuSubGridViewModel BottomLeft { get; }
        public SudokuSubGridViewModel BottomMiddle { get; }
        public SudokuSubGridViewModel BottomRight { get; }

        #endregion

        public SudokuSolverViewModel(Sudoku sudoku) : 
            base(sudoku)
        {
            TopLeft = new SudokuSubGridViewModel(sudoku.TopLeft);
            TopMiddle = new SudokuSubGridViewModel(sudoku.TopMiddle);
            TopRight = new SudokuSubGridViewModel(sudoku.TopRight);

            MiddleLeft = new SudokuSubGridViewModel(sudoku.MiddleLeft);
            MiddleMiddle = new SudokuSubGridViewModel(sudoku.MiddleMiddle);
            MiddleRight = new SudokuSubGridViewModel(sudoku.MiddleRight);

            BottomLeft = new SudokuSubGridViewModel(sudoku.BottomLeft);
            BottomMiddle = new SudokuSubGridViewModel(sudoku.BottomMiddle);
            BottomRight = new SudokuSubGridViewModel(sudoku.BottomRight);
        }
    }
}