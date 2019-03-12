using CelesteEngineEditor.ViewModels;
using SudokuSolver.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Editors.SudokuSolver.ViewModels
{
    public class SudokuSubGridViewModel : Notifier, IEnumerable<SudokuElementViewModel>
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

        private List<SudokuElementViewModel> elements = new List<SudokuElementViewModel>(9);
        public ReadOnlyCollection<SudokuElementViewModel> Elements { get; }

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

            Elements = new ReadOnlyCollection<SudokuElementViewModel>(elements);
            elements.Add(TopLeft);
            elements.Add(TopMiddle);
            elements.Add(TopRight);
            elements.Add(MiddleLeft);
            elements.Add(MiddleMiddle);
            elements.Add(MiddleRight);
            elements.Add(BottomLeft);
            elements.Add(BottomMiddle);
            elements.Add(BottomRight);

            foreach (SudokuElementViewModel sudokuElements in Elements)
            {
                sudokuElements.PropertyChanged += SudokuElements_PropertyChanged; ;
            }
        }

        #region Solving Utility Functions

        public bool IsValueInColumn(int value, int columnIndex)
        {
            return elements[columnIndex] == value ||
                   elements[columnIndex + 3] == value ||
                   elements[columnIndex + 6] == value;
        }

        public bool IsValueInRow(int value, int rowIndex)
        {
            return elements[3 * rowIndex] == value ||
                   elements[3 * rowIndex + 1] == value ||
                   elements[3 * rowIndex + 2] == value;
        }

        #endregion

        #region IEnumerable Interface

        public IEnumerator<SudokuElementViewModel> GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// When any part of our sudoku changes we need to make sure the overall asset is dirtied.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SudokuElements_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyOnPropertyChanged(nameof(sender));
        }

        #endregion
    }
}