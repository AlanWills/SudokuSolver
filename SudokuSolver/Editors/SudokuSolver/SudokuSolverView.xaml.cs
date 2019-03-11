using CelesteEngineEditor.Attributes;
using CelesteEngineEditor.Editors;
using DevZest.Windows.Docking;
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

namespace SudokuSolver.Editors.SudokuSolver
{
    /// <summary>
    /// Interaction logic for SudokuSolver.xaml
    /// </summary>
    [CustomEditor("Sudoku Solver", DockPosition.Document)]
    [CustomMenuItem("Solvers/Sudoku Solver")]
    public partial class SudokuSolverView : Editor
    {
        public SudokuSolverView(object targetValue) :
            base(new SudokuSolverViewModel(targetValue))
        {
            InitializeComponent();
        }
    }
}