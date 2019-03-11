using SudokuSolver.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Editors.SudokuSolver.ViewModels
{
    public class SudokuElementViewModel
    {
        #region Properties and Fields

        public SudokuElement SudokuElement { get; }

        #endregion

        public SudokuElementViewModel(SudokuElement sudokuElement)
        {
            SudokuElement = sudokuElement;
        }
    }
}
