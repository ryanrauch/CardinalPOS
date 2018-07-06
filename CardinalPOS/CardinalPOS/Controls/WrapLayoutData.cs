using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CardinalPOS.Controls
{
    struct WrapLayoutData
    {
        public int VisibleChildCount { get; private set; }
        public Size CellSize { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        
        public WrapLayoutData(int visibleChildCount, Size cellSize, int rows, int columns) : this()
        {
            VisibleChildCount = visibleChildCount;
            CellSize = cellSize;
            Rows = rows;
            Columns = columns;
        }
    }
}
