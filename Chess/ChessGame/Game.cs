using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGame
{
    public class Game
    {
        private List<String> whiteNames;
        private List<String> blackNames;
        private List<Piece> whitePieces;
        private List<Piece> blackPieces;
        private Player whitePlayer;
        private Player blackPlayer;

        public Game()
        {

            //16 white pieces for white player
            whiteNames = new List<string>() {"white_Rook", "white_Knight",
                "white_Bishop", "white_Queen", "white_King", "white_Bishop_dup",
                "white_Knight_dup", "white_Rook_dup", "white_Pawn_0", "white_Pawn_1", "white_Pawn_2",
                "white_Pawn_3", "white_Pawn_4", "white_Pawn_5", "white_Pawn_6", "white_Pawn_7" };

            //16 black pieces for black player
            blackNames = new List<string>() {"black_Rook", "black_Knight",
                "black_Bishop", "black_Queen", "black_King", "black_Bishop_dup",
                "black_Knight_dup", "black_Rook_dup", "black_Pawn_0", "black_Pawn_1", "black_Pawn_2",
                "black_Pawn_3", "black_Pawn_4", "black_Pawn_5", "black_Pawn_6", "black_Pawn_7" };

            whitePieces = new List<Piece>();
            blackPieces = new List<Piece>();
            SetBoardImageValues();


        }

        private void SetBoardImageValues()
        {
            foreach (string piece in whiteNames)
            {

                whitePieces.Add(new Piece(piece));
            }

            foreach (string piece in blackNames)
            {
                blackPieces.Add(new Piece(piece));
            }
        }

        public List<Piece> GetWhitePieces()
        {
            return whitePieces;
        }
        public List<Piece> GetBlackPieces()
        {
            return blackPieces;
        }

        public void MovePiece(Position start, Position end, Piece previous, Piece target)
        {
            if (target == null)
            {
                //moving to empty space
                Console.WriteLine("Moving Piece(" + previous.Name + ") to empty position(" + end.Colm + ", " + end.Row + ")");
            } else
            {
                //attempting to capture piece.
                Console.WriteLine("attempting to capture piece:" + target.Name);
            }
        }
    }
}