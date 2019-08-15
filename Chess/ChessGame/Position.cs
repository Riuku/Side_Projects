using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Position
    {
        public Position(int row, int colm)
        {
            this.Row = row;
            this.Colm = colm;
        }

        public int Row { get; }
        public int Colm { get; }
    }
}
