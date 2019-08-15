using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Player
    {
        public Player(bool isWhite, string name)
        {
            this.IsWhite = isWhite;
            this.Name = name;
            this.Score = 0;
        }

        public bool IsWhite { get; set; }
        public int Score { get; set; }
        public string Name { get; set; }
    }
}