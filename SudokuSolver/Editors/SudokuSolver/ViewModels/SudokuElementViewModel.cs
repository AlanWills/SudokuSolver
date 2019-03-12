using CelesteEngineEditor.ViewModels;
using SudokuSolver.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Editors.SudokuSolver.ViewModels
{
    public class SudokuElementViewModel : Notifier
    {
        #region Properties and Fields

        private SudokuElement SudokuElement { get; }

        public int Value
        {
            get { return SudokuElement.Value; }
            set
            {
                SudokuElement.Value = value;
                NotifyOnPropertyChanged(nameof(ValueString));
            }
        }

        public string ValueString
        {
            get { return SudokuElement.Value == 0 ? "" : SudokuElement.Value.ToString(); }
            set
            {
                SudokuElement.Value = string.IsNullOrEmpty(value) ? 0 : int.Parse(value);
                NotifyOnPropertyChanged(nameof(ValueString));
            }
        }

        #endregion

        public SudokuElementViewModel(SudokuElement sudokuElement)
        {
            SudokuElement = sudokuElement;
        }

        #region Operators

        public static bool operator==(SudokuElementViewModel sudokuElementViewModel, int target)
        {
            return sudokuElementViewModel.Value == target;
        }

        public static bool operator !=(SudokuElementViewModel sudokuElementViewModel, int target)
        {
            return sudokuElementViewModel.Value != target;
        }

        #endregion
    }
}
