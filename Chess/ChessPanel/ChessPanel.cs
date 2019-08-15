using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessGame;

namespace CP
{
    /// <summary>
    /// The type of delegate used to register for SelectionChanged events
    /// </summary>
    /// <param name="sender"></param>

    public delegate void SelectionChangedHandler(Position start, Position end, Piece previous, Piece target);
    public partial class ChessPanel : UserControl
    {
        private DrawingPanel drawingPanel;
        private const int DATA_COL_WIDTH = 80;
        private const int DATA_ROW_HEIGHT = 80;
        private const int PADDING = 1;
        private const int COL_COUNT = 8;
        private const int ROW_COUNT = 8;
        public ChessPanel()
        {
            InitializeComponent();

            drawingPanel = new DrawingPanel(this);
            drawingPanel.Location = new Point(0, 0);
            Controls.Add(drawingPanel);
        }

        public void Clear()
        {
            drawingPanel.Clear();
        }

        /// <summary>
        /// If the zero-based column and row are in range, sets the value of that
        /// cell and returns true.  Otherwise, returns false.
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        public bool SetValue(int col, int row, Piece pc)
        {
            return drawingPanel.SetValue(col, row, pc);
        }


        /// <summary>
        /// If the zero-based column and row are in range, assigns the value
        /// of that cell to the out parameter and returns true.  Otherwise,
        /// returns false.
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        public bool GetValue(int col, int row, out Piece pc)
        {
            return drawingPanel.GetValue(col, row, out pc);
        }


        /// <summary>
        /// If the zero-based column and row are in range, uses them to set
        /// the current selection and returns true.  Otherwise, returns false.
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>

        public bool SetSelection(int col, int row)
        {
            return drawingPanel.SetSelection(col, row);
        }


        /// <summary>
        /// Assigns the column and row of the current selection to the
        /// out parameters.
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>

        public void GetSelection(out int col, out int row)
        {
            drawingPanel.GetSelection(out col, out row);
        }


        /// <summary>
        /// When the SpreadsheetPanel is resized, we set the size and locations of the three
        /// components that make it up.
        /// </summary>
        /// <param name="eventargs"></param>

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            if (FindForm() == null || FindForm().WindowState != FormWindowState.Minimized)
            {
                drawingPanel.Size = new Size(Width, Height);
                Console.WriteLine(Width.ToString() + ", " + Height.ToString());

            }
        }

        /// <summary>
        /// The event used to send notifications of a selection change
        /// </summary>

        public event SelectionChangedHandler SelectionChanged;

        private class Address
        {

            public int Col { get; set; }
            public int Row { get; set; }

            public Address(int c, int r)
            {
                Col = c;
                Row = r;
            }

            public override int GetHashCode()
            {
                return Col.GetHashCode() ^ Row.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if ((obj == null) || !(obj is Address))
                {
                    return false;
                }
                Address a = (Address)obj;
                return Col == a.Col && Row == a.Row;
            }

        }


        private class DrawingPanel : Panel
        {
            // Columns and rows are numbered beginning with 0.  This is the coordinate
            // of the selected cell.
            private int _selectedCol;
            private int _selectedRow;

            private int _previousCol;
            private int _previousRow;
            private Piece previous_pc;
            private bool previous_was_piece = false;

            // Coordinate of cell in upper-left corner of display
            private int _firstColumn = 0;
            private int _firstRow = 0;

            // The strings contained by the spreadsheet
            private Dictionary<Address, Piece> _values;

            // The containing panel
            private ChessPanel _chessForm;

            //toggles background color per square.
            bool toggle = true;

            public DrawingPanel(ChessPanel cf)
            {
                DoubleBuffered = true;
                _values = new Dictionary<Address, Piece>();
                _chessForm = cf;
            }


            private bool InvalidAddress(int col, int row)
            {
                return col < 0 || row < 0 || col >= COL_COUNT || row >= ROW_COUNT;
            }


            public void Clear()
            {
                _values.Clear();
                Invalidate();
            }

            public bool SetValue(int col, int row, Piece pc)
            {
                if (InvalidAddress(col, row))
                {
                    return false;
                }

                Address a = new Address(col, row);
                if (pc == null)
                {
                    _values.Remove(a);
                }
                else
                {
                    _values[a] = pc;
                }
                Invalidate();
                return true;
            }


            public bool GetValue(int col, int row, out Piece pc)
            {
                if (InvalidAddress(col, row))
                {
                    pc = null;
                    return false;
                }
                if (!_values.TryGetValue(new Address(col, row), out pc))
                {
                    pc = null;
                }
                return true;
            }


            public bool SetSelection(int col, int row)
            {
                if (InvalidAddress(col, row))
                {
                    return false;
                }
                _selectedCol = col;
                _selectedRow = row;
                Invalidate();
                return true;
            }


            public void GetSelection(out int col, out int row)
            {
                col = _selectedCol;
                row = _selectedRow;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                // Clip based on what needs to be refreshed.
                Region clip = new Region(e.ClipRectangle);
                e.Graphics.Clip = clip;

                // Color the background of the data area
                
                Brush bg_square = new SolidBrush(Color.FromArgb(217, 207, 188));
                for (int i=0; i< ROW_COUNT; i++)
                {
                    for (int j=0; j < COL_COUNT; j++)
                    {
                        Rectangle region = new Rectangle(j * DATA_COL_WIDTH + PADDING,
                                                                   i * DATA_ROW_HEIGHT + PADDING,
                                                                   DATA_COL_WIDTH - PADDING,
                                                                   DATA_ROW_HEIGHT - PADDING);

                        
                        e.Graphics.FillRectangle(bg_square, region);
                        bg_square = toggleBGColor(bg_square);
                    }
                    bg_square = toggleBGColor(bg_square);
                }

                // Pen, brush, and fonts to use
                Brush brush = new SolidBrush(Color.Black);
                Pen pen = new Pen(brush);
                Font regularFont = Font;
                Font boldFont = new Font(regularFont, FontStyle.Bold);

                // Draw the column lines
                int bottom = (ROW_COUNT - _firstRow) * DATA_ROW_HEIGHT;
                e.Graphics.DrawLine(pen, new Point(0, 0), new Point(0, bottom));
                for (int x = 0; x <= (COL_COUNT - _firstColumn); x++)
                {
                    e.Graphics.DrawLine(
                        pen,
                        new Point(x * DATA_COL_WIDTH, 0),
                        new Point(x * DATA_COL_WIDTH, bottom));
                }



                // Draw the row lines
                int right = (COL_COUNT - _firstColumn) * DATA_COL_WIDTH;
                e.Graphics.DrawLine(pen, new Point(0, 0), new Point(right, 0));
                for (int y = 0; y <= ROW_COUNT - _firstRow; y++)
                {
                    e.Graphics.DrawLine(
                        pen,
                        new Point(0, y * DATA_ROW_HEIGHT),
                        new Point(right, y * DATA_ROW_HEIGHT));
                }



                // Highlight the selection, if it is visible
                if ((_selectedCol - _firstColumn >= 0) && (_selectedRow - _firstRow >= 0))
                {
                    e.Graphics.DrawRectangle(
                        pen,
                        new Rectangle((_selectedCol - _firstColumn) * DATA_COL_WIDTH + 1,
                                      (_selectedRow - _firstRow) * DATA_ROW_HEIGHT + 1,
                                      DATA_COL_WIDTH - 2,
                                      DATA_ROW_HEIGHT - 2));
                }

                // Draw the images
                foreach (KeyValuePair<Address, Piece> address in _values)
                {
                    Image image = address.Value.Image;
                    int x = address.Key.Col - _firstColumn;
                    int y = address.Key.Row - _firstRow;
                    float height = image.Height;
                    float width = image.Width;
                    if (x >= 0 && y >= 0)
                    {
                        Rectangle region = new Rectangle(x * DATA_COL_WIDTH + PADDING,
                                                                   y * DATA_ROW_HEIGHT + PADDING,
                                                                   DATA_COL_WIDTH - 2 * PADDING,
                                                                   DATA_ROW_HEIGHT - 2 * PADDING);
                        Region cellClip = new Region(region);
                        cellClip.Intersect(clip);
                        e.Graphics.Clip = cellClip;
                        e.Graphics.DrawImage(image, region);
                    }
                }


            }

            /// <summary>
            /// Determines which cell, if any, was clicked.  Generates a SelectionChanged event.  All of
            /// the indexes are zero based.
            /// </summary>
            /// <param name="e"></param>

            protected override void OnMouseClick(MouseEventArgs e)
            {
                base.OnClick(e);
                int x = (e.X) / DATA_COL_WIDTH;
                int y = (e.Y) / DATA_ROW_HEIGHT;
                if ((x + _firstColumn < COL_COUNT) && (y + _firstRow < ROW_COUNT))
                {
                    _selectedCol = x + _firstColumn;
                    _selectedRow = y + _firstRow;
                    if (_chessForm.SelectionChanged != null)
                    {
                        

                        if (GetValue(_selectedCol, _selectedRow, out Piece target_pc))
                        {
                            if (previous_was_piece)
                            {
                                Position previous = new Position(_previousRow, _previousCol);
                                Position current = new Position(_selectedRow, _selectedCol);
                                _chessForm.SelectionChanged(previous, current, previous_pc, target_pc);
                            }

                            if (target_pc == null)
                            {
                                _previousCol = -1;
                                _previousRow = -1;
                                previous_was_piece = false;
                            } else
                            {
                                _previousCol = _selectedCol;
                                _previousRow = _selectedRow;
                                previous_pc = target_pc;
                                previous_was_piece = true;
                            }

                            

                        }


                    }
                }
                Invalidate();
            }

            private Brush toggleBGColor(Brush b)
            {
                if (toggle)
                {
                    toggle = false;
                    b = new SolidBrush(Color.FromArgb(158, 107, 64)); //dark brown
                }
                else
                {
                    toggle = true;
                    b = new SolidBrush(Color.FromArgb(217, 207, 188)); //beige
                }
                return b;
            }
        }
    }

}