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

        public string PossibleValuesString
        {
            get
            {
                StringBuilder builder = new StringBuilder(32);
                foreach (int value in SudokuElement.PossibleValues)
                {
                    builder.Append(value)
                           .Append(',');
                }

                return builder.ToString();
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

        #region Possible Value Utility Functions

        public void RemoveFromPossibleValues(IEnumerable<int> valuesToRemove)
        {
            SudokuElement.PossibleValues.ExceptWith(valuesToRemove);
            NotifyOnPropertyChanged(nameof(PossibleValuesString));
        }

        #endregion
    }
}
