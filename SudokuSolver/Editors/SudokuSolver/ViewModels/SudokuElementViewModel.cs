using CelesteEngineEditor.ViewModels;
using SudokuSolver.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
                if (Value > 0)
                {
                    ClearPossibleValues();
                }

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

        public ReadOnlyCollection<int> PossibleValues { get; private set; }

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
            PossibleValues = new ReadOnlyCollection<int>(sudokuElement.PossibleValues.ToList());
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

        public void ClearPossibleValues()
        {
            SudokuElement.PossibleValues.Clear();

            // We've changed our backing data, so update the readonly public interface
            OnValueRemoved();
        }

        public void SetPossibleValuesTo(IEnumerable<int> possibleValuesToRemain)
        {
            SudokuElement.PossibleValues.Clear();
            
            foreach (int value in possibleValuesToRemain)
            {
                SudokuElement.PossibleValues.Add(value);
            }

            // We've changed our backing data, so update the readonly public interface
            OnValueRemoved();
        }

        public void RemoveFromPossibleValues(int valueToRemove)
        {
            if (SudokuElement.PossibleValues.Contains(valueToRemove))
            {
                SudokuElement.PossibleValues.Remove(valueToRemove);

                // We've changed our backing data, so update the readonly public interface
                OnValueRemoved();
            }
        }

        public void RemoveFromPossibleValues(IEnumerable<int> valuesToRemove)
        {
            int oldSize = SudokuElement.PossibleValues.Count;
            SudokuElement.PossibleValues.ExceptWith(valuesToRemove);

            if (oldSize != SudokuElement.PossibleValues.Count)
            {
                // We've changed our backing data, so update the readonly public interface
                OnValueRemoved();
            }
        }

        private void OnValueRemoved()
        {
            // Debug checks to ensure that either the value is set and we have no possible values
            // Or that the value is not set and there are still possible values
            Debug.Assert(SudokuElement.Value > 0 || SudokuElement.PossibleValues.Count > 0);
            Debug.Assert(SudokuElement.Value == 0 || SudokuElement.PossibleValues.Count == 0);

            PossibleValues = new ReadOnlyCollection<int>(SudokuElement.PossibleValues.ToList());
            NotifyOnPropertyChanged(nameof(PossibleValuesString));
        }

        #endregion
    }
}
