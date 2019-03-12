using BindingsKernel.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SudokuSolver.Data
{
    public class SudokuSubGrid : ICustomDeserialization, ICustomSerialization
    {
        #region Properties and Fields

        // Top

        public SudokuElement TopLeft { get; private set; } = new SudokuElement();
        public SudokuElement TopMiddle { get; private set; } = new SudokuElement();
        public SudokuElement TopRight { get; private set; } = new SudokuElement();

        // Middle

        public SudokuElement MiddleLeft { get; private set; } = new SudokuElement();
        public SudokuElement MiddleMiddle { get; private set; } = new SudokuElement();
        public SudokuElement MiddleRight { get; private set; } = new SudokuElement();

        // Bottom

        public SudokuElement BottomLeft { get; private set; } = new SudokuElement();
        public SudokuElement BottomMiddle { get; private set; } = new SudokuElement();
        public SudokuElement BottomRight { get; private set; } = new SudokuElement();

        #endregion

        #region Deserialization Implementation

        public void Deserialize(string name, XmlReader reader)
        {
            reader.Read();

            TopLeft.Value = int.Parse(reader.GetAttribute("TopLeft"));
            TopMiddle.Value = int.Parse(reader.GetAttribute("TopMiddle"));
            TopRight.Value = int.Parse(reader.GetAttribute("TopRight"));

            MiddleLeft.Value = int.Parse(reader.GetAttribute("MiddleLeft"));
            MiddleMiddle.Value = int.Parse(reader.GetAttribute("MiddleMiddle"));
            MiddleRight.Value = int.Parse(reader.GetAttribute("MiddleRight"));

            BottomLeft.Value = int.Parse(reader.GetAttribute("BottomLeft"));
            BottomMiddle.Value = int.Parse(reader.GetAttribute("BottomMiddle"));
            BottomRight.Value = int.Parse(reader.GetAttribute("BottomRight"));

            reader.Read();
        }

        #endregion

        #region Serialization Implementation

        public void Serialize(string name, XmlWriter writer)
        {
            writer.WriteStartElement(name);

            writer.WriteAttributeString("TopLeft", TopLeft.Value.ToString());
            writer.WriteAttributeString("TopMiddle", TopMiddle.Value.ToString());
            writer.WriteAttributeString("TopRight", TopRight.Value.ToString());
            writer.WriteAttributeString("MiddleLeft", MiddleLeft.Value.ToString());
            writer.WriteAttributeString("MiddleMiddle", MiddleMiddle.Value.ToString());
            writer.WriteAttributeString("MiddleRight", MiddleRight.Value.ToString());
            writer.WriteAttributeString("BottomLeft", BottomLeft.Value.ToString());
            writer.WriteAttributeString("BottomMiddle", BottomMiddle.Value.ToString());
            writer.WriteAttributeString("BottomRight", BottomRight.Value.ToString());

            writer.WriteEndElement();
        }

        #endregion
    }
}
