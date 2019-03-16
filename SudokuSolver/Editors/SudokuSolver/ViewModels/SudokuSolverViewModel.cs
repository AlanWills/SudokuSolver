using CelesteEngineEditor.Core;
using CelesteEngineEditor.Editors;
using SudokuSolver.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        private List<SudokuSubGridViewModel> subGrids = new List<SudokuSubGridViewModel>(9);
        public ReadOnlyCollection<SudokuSubGridViewModel> SubGrids { get; }

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

            SubGrids = new ReadOnlyCollection<SudokuSubGridViewModel>(subGrids);
            subGrids.Add(TopLeft);
            subGrids.Add(TopMiddle);
            subGrids.Add(TopRight);
            subGrids.Add(MiddleLeft);
            subGrids.Add(MiddleMiddle);
            subGrids.Add(MiddleRight);
            subGrids.Add(BottomLeft);
            subGrids.Add(BottomMiddle);
            subGrids.Add(BottomRight);

            foreach (SudokuSubGridViewModel sudokuSubGrid in SubGrids)
            {
                sudokuSubGrid.PropertyChanged += SudokuSubGrid_PropertyChanged;
            }
        }

        #region Solving Functions

        public void Solve()
        {
            SweepGrids();
            SweepRows();
            SweepColumns();
        }
        
        private void SweepGrids()
        {
            bool newValuesFound = false;
            do
            {
                newValuesFound = false;
                for (int subGridIndex = 0; subGridIndex < SubGrids.Count; ++subGridIndex)
                {
                    newValuesFound |= SweepSubGrid(subGridIndex);
                }
            }
            while (newValuesFound);
        }

        private bool SweepSubGrid(int subGridIndex)
        {
            List<HashSet<int>> suggestions = new List<HashSet<int>>(9);
            for (int i = 0; i < suggestions.Capacity; ++i)
            {
                suggestions.Add(new HashSet<int>());
            }

            HashSet<int> toFind = new HashSet<int>()
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9
            };

            SudokuSubGridViewModel subGrid = SubGrids[subGridIndex];

            foreach (SudokuElementViewModel sudokuElementViewModel in subGrid)
            {
                if (sudokuElementViewModel.Value != 0)
                {
                    toFind.Remove(sudokuElementViewModel.Value);
                }
            }

            for (int elementIndex = 0; elementIndex < subGrid.Elements.Count; ++elementIndex)
            {
                SudokuElementViewModel element = subGrid.Elements[elementIndex];

                if (element.Value == 0)
                {
                    int elementColumn = 3 * (subGridIndex % 3) + elementIndex % 3;
                    int elementRow = 3 * (subGridIndex / 3) + elementIndex / 3;

                    foreach (int value in toFind)
                    {
                        if (!IsValueInColumn(value, elementColumn) &&
                            !IsValueInRow(value, elementRow))
                        {
                            suggestions[elementIndex].Add(value);
                        }
                    }
                }
            }

            bool newValuesFound = false;

            foreach (int value in toFind)
            {
                if (suggestions.Count(x => x.Contains(value)) == 1)
                {
                    int index = suggestions.FindIndex(x => x.Contains(value));
                    subGrid.Elements[index].Value = value;
                    newValuesFound = true;
                }
            }

            Project.SetDirty(Sudoku);
            return newValuesFound;
        }

        private void SweepRows()
        {
            bool newValuesFound = false;
            do
            {
                newValuesFound = false;
                for (int i = 0; i < 9; ++i)
                {
                    newValuesFound |= SweepRow(i);
                }
            }
            while (newValuesFound);
        }

        private bool SweepRow(int rowIndex)
        {
            List<HashSet<int>> suggestions = new List<HashSet<int>>(9);
            for (int i = 0; i < suggestions.Capacity; ++i)
            {
                suggestions.Add(new HashSet<int>());
            }

            HashSet<int> toFind = new HashSet<int>();
            for (int i = 1; i <= 9; ++i)
            {
                if (!IsValueInRow(i, rowIndex))
                {
                    toFind.Add(i);
                }
            }

            List<SudokuElementViewModel> rowElements = GetElementsInRow(rowIndex);

            for (int i = 0; i < rowElements.Count; ++i)
            {
                if (rowElements[i].Value == 0)
                {
                    foreach (int value in toFind)
                    {
                        if (!IsValueInColumn(value, i) && !IsValueInSubGrid(value, 3 * (rowIndex / 3) + i / 3))
                        {
                            suggestions[i].Add(value);
                        }
                    }
                }
            }

            bool newValuesFound = false;

            foreach (int value in toFind)
            {
                if (suggestions.Count(x => x.Contains(value)) == 1)
                {
                    int index = suggestions.FindIndex(x => x.Contains(value));
                    rowElements[index].Value = value;
                    newValuesFound = true;
                }
            }

            Project.SetDirty(Sudoku);
            return newValuesFound;
        }

        private void SweepColumns()
        {
            bool newValuesFound = false;
            do
            {
                newValuesFound = false;
                for (int i = 0; i < 9; ++i)
                {
                    newValuesFound |= SweepColumn(i);
                }
            }
            while (newValuesFound);
        }

        private bool SweepColumn(int columnIndex)
        {
            List<HashSet<int>> suggestions = new List<HashSet<int>>(9);
            for (int i = 0; i < suggestions.Capacity; ++i)
            {
                suggestions.Add(new HashSet<int>());
            }

            HashSet<int> toFind = new HashSet<int>();
            for (int i = 1; i <= 9; ++i)
            {
                if (!IsValueInColumn(i, columnIndex))
                {
                    toFind.Add(i);
                }
            }

            List<SudokuElementViewModel> columnElements = GetElementsInColumn(columnIndex);

            for (int i = 0; i < columnElements.Count; ++i)
            {
                if (columnElements[i].Value == 0)
                {
                    foreach (int value in toFind)
                    {
                        if (!IsValueInRow(value, i) && !IsValueInSubGrid(value, columnIndex / 3 + 3 * (i / 3)))
                        {
                            suggestions[i].Add(value);
                        }
                    }
                }
            }

            bool newValuesFound = false;

            foreach (int value in toFind)
            {
                if (suggestions.Count(x => x.Contains(value)) == 1)
                {
                    int index = suggestions.FindIndex(x => x.Contains(value));
                    columnElements[index].Value = value;
                    newValuesFound = true;
                }
            }

            Project.SetDirty(Sudoku);
            return newValuesFound;
        }

        public bool IsValueInSubGrid(int value, int subGridIndex)
        {
            return SubGrids[subGridIndex].Elements.Any(x => x == value);
        }

        public bool IsValueInColumn(int value, int columnIndex)
        {
            int subGridIndex = columnIndex / 3;
            int subColumnIndex = columnIndex - subGridIndex * 3;

            return SubGrids[subGridIndex].IsValueInColumn(value, subColumnIndex) ||
                   SubGrids[subGridIndex + 3].IsValueInColumn(value, subColumnIndex) ||
                   SubGrids[subGridIndex + 6].IsValueInColumn(value, subColumnIndex);
        }

        public bool IsValueInRow(int value, int rowIndex)
        {
            int subGridIndex = 3 * (rowIndex / 3);
            int subRowIndex = rowIndex - subGridIndex;

            return SubGrids[subGridIndex].IsValueInRow(value, subRowIndex) ||
                   SubGrids[subGridIndex + 1].IsValueInRow(value, subRowIndex) ||
                   SubGrids[subGridIndex + 2].IsValueInRow(value, subRowIndex);
        }

        #endregion

        #region Element Utility Functions

        public List<SudokuElementViewModel> GetElementsInRow(int rowIndex)
        {
            List<SudokuElementViewModel> elements = new List<SudokuElementViewModel>(9);

            int subGridIndex = 3 * (rowIndex / 3);
            int subGridRowIndex = rowIndex - subGridIndex;

            elements.AddRange(SubGrids[subGridIndex].GetElementsInRow(subGridRowIndex));
            elements.AddRange(SubGrids[subGridIndex + 1].GetElementsInRow(subGridRowIndex));
            elements.AddRange(SubGrids[subGridIndex + 2].GetElementsInRow(subGridRowIndex));

            return elements;
        }

        public List<SudokuElementViewModel> GetElementsInColumn(int columnIndex)
        {
            List<SudokuElementViewModel> elements = new List<SudokuElementViewModel>(9);

            int subGridIndex = columnIndex / 3;
            int subGridColumnIndex = columnIndex - subGridIndex * 3;

            elements.AddRange(SubGrids[subGridIndex].GetElementsInColumn(subGridColumnIndex));
            elements.AddRange(SubGrids[subGridIndex + 3].GetElementsInColumn(subGridColumnIndex));
            elements.AddRange(SubGrids[subGridIndex + 6].GetElementsInColumn(subGridColumnIndex));

            return elements;
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// When any part of our sudoku changes we need to make sure the overall asset is dirtied.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SudokuSubGrid_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Project.SetDirty(TargetObject);
        }

        #endregion
    }
}