using CelesteEngineEditor.Editors;
using SudokuSolver.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Editors.SudokuSolver
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

        #endregion

        public SudokuSolverViewModel(Sudoku sudoku) : 
            base(sudoku)
        {
        }
    }
}