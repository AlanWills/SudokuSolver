using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Data
{
    public class SudokuElement
    {
        #region Properties and Fields

        private int value = 0;
        /// <summary>
        /// The current value in this element.  0 corresponds to an unknown value.
        /// </summary>
        public int Value
        {
            get { return value; }
            set
            {
                this.value = value;

                if (Value > 0)
                {
                    // This value is set in stone and so cannot have any other possible values
                    PossibleValues.Clear();
                }
            }
        }

        /// <summary>
        /// The possible values this single element could have to maintain the constrictions of the sudoku.
        /// </summary>
        public HashSet<int> PossibleValues { get; } = new HashSet<int>()
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9
        };

        #endregion
    }
}
