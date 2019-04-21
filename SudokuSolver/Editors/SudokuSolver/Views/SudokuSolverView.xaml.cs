using CelesteEngineEditor.Attributes;
using CelesteEngineEditor.Editors;
using DevZest.Windows.Docking;
using SudokuSolver.Data;
using SudokuSolver.Editors.SudokuSolver.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SudokuSolver.Editors.SudokuSolver.Views
{
    /// <summary>
    /// Interaction logic for SudokuSolver.xaml
    /// </summary>
    [CustomEditor(typeof(Sudoku), "Sudoku Solver", DockPosition.Document)]
    public partial class SudokuSolverView : Editor
    {
        #region Properties and Fields

        private SudokuSolverViewModel SudokuSolverViewModel { get { return ViewModel as SudokuSolverViewModel; } }

        #endregion

        public SudokuSolverView(object target) :
            base(new SudokuSolverViewModel(target as Sudoku))
        {
            InitializeComponent();
        }
        
        private void SolveButton_Click(object sender, RoutedEventArgs e)
        {
            SudokuSolverViewModel.Solve();
        }

        private void SolveSingleButton_Click(object sender, RoutedEventArgs e)
        {
            SudokuSolverViewModel.SolveSingle();
        }
    }
}