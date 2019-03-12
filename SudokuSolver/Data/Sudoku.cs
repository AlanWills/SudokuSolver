using BindingsKernel;
using BindingsKernel.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SudokuSolver.Data
{
    public class Sudoku : ScriptableObject, ICustomDeserialization, ICustomSerialization
    {
        #region Properties and Fields

        // Top
        public SudokuSubGrid TopLeft { get; private set; } = new SudokuSubGrid();
        public SudokuSubGrid TopMiddle { get; private set; } = new SudokuSubGrid();
        public SudokuSubGrid TopRight { get; private set; } = new SudokuSubGrid();

        // Middle
        public SudokuSubGrid MiddleLeft { get; private set; } = new SudokuSubGrid();
        public SudokuSubGrid MiddleMiddle { get; private set; } = new SudokuSubGrid();
        public SudokuSubGrid MiddleRight { get; private set; } = new SudokuSubGrid();

        // Bottom
        public SudokuSubGrid BottomLeft { get; private set; } = new SudokuSubGrid();
        public SudokuSubGrid BottomMiddle { get; private set; } = new SudokuSubGrid();
        public SudokuSubGrid BottomRight { get; private set; } = new SudokuSubGrid();

        #endregion

        #region Deserialization Implementation

        public void Deserialize(string name, XmlReader reader)
        {
            reader.ReadStartElement(name);

            TopLeft.Deserialize("TopLeft", reader);
            TopMiddle.Deserialize("TopMiddle", reader);
            TopRight.Deserialize("TopRight", reader);

            MiddleLeft.Deserialize("MiddleLeft", reader);
            MiddleMiddle.Deserialize("MiddleMiddle", reader);
            MiddleRight.Deserialize("MiddleRight", reader);

            BottomLeft.Deserialize("BottomLeft", reader);
            BottomMiddle.Deserialize("BottomMiddle", reader);
            BottomRight.Deserialize("BottomRight", reader);

            reader.ReadEndElement();
        }

        #endregion

        #region Serialization Implementation

        public void Serialize(string name, XmlWriter writer)
        {
            TopLeft.Serialize("TopLeft", writer);
            TopMiddle.Serialize("TopMiddle", writer);
            TopRight.Serialize("TopRight", writer);

            MiddleLeft.Serialize("MiddleLeft", writer);
            MiddleMiddle.Serialize("MiddleMiddle", writer);
            MiddleRight.Serialize("MiddleRight", writer);

            BottomLeft.Serialize("BottomLeft", writer);
            BottomMiddle.Serialize("BottomMiddle", writer);
            BottomRight.Serialize("BottomRight", writer);
        }

        #endregion
    }
}