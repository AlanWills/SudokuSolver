using CelesteEngineEditor.Editors;
using SudokuSolver.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        }

        #region Solving Functions

        public void Solve()
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

            foreach (SudokuElementViewModel sudokuElementViewModel in MiddleMiddle)
            {
                if (sudokuElementViewModel.Value != 0)
                {
                    toFind.Remove(sudokuElementViewModel.Value);
                }
            }

            int currentIndex = 0;
            foreach (SudokuElementViewModel sudokuElementViewModel in MiddleMiddle)
            {
                if (sudokuElementViewModel.Value == 0)
                {
                    foreach (int value in toFind)
                    {
                        if (!IsValueInColumn(value, 3 + (currentIndex % 3)) &&
                            !IsValueInRow(value, 3 + (currentIndex / 3)))
                        {
                            suggestions[currentIndex].Add(value);
                        }
                    }
                }

                ++currentIndex;
            }

            foreach (int value in toFind)
            {
                if (suggestions.Count(x => x.Contains(value)) == 1)
                {
                    int index = suggestions.FindIndex(x => x.Contains(value));
                    MiddleMiddle.Elements[index].Value = value;
                }
            }
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
    }
}