using System;
using System.Collections.Generic;
using System.Drawing;
using ChessGame.Properties;

namespace ChessGame
{
    public class Piece
    {
        private Image image;
        private Position pos;
        private HashSet<Position> available_moves;

        public Piece(String name)
        {
            this.Name = name;
            bool white = true;
            bool duplicate = false;
            int pawnColm = 1;
            int pawnRow = 0;
            int nonPawnColm = 0;
            string[] identifier = name.Split('_');
            if (identifier[0].Equals("black"))
            {
                pawnColm = 6;
                nonPawnColm = 7;
                white = false;
            }
            else if (!identifier[0].Equals("white"))
            {
                throw new ArgumentException("filename is not a valid image file");
            }

            if (identifier.Length == 3 && identifier[2] != "dup")
                pawnRow = int.Parse(identifier[2]);
            else if (identifier.Length == 3 && identifier[2] == "dup")
                duplicate = true;


            switch (identifier[1])
            {
                case "Rook":
                    if (duplicate)
                        Position = new Position(7, nonPawnColm);
                    else
                        Position = new Position(0, nonPawnColm);

                    if (white)
                        Image = Resource.white_Rook;
                    else
                        Image = Resource.black_Rook;
                    break;
                case "Knight":
                    if (duplicate)
                        Position = new Position(6, nonPawnColm);
                    else
                        Position = new Position(1, nonPawnColm);

                    if (white)
                        Image = Resource.white_Knight;
                    else
                        Image = Resource.black_Knight;

                    break;
                case "Bishop":
                    if (duplicate)
                        Position = new Position(5, nonPawnColm);
                    else
                        Position = new Position(2, nonPawnColm);

                    if (white)
                        Image = Resource.white_Bishop;
                    else
                        Image = Resource.black_Bishop;
                    break;
                case "Queen":
                    Position = new Position(3, nonPawnColm);

                    if (white)
                        Image = Resource.white_Queen;
                    else
                        Image = Resource.black_Queen;
                    break;
                case "King":
                    Position = new Position(4, nonPawnColm);

                    if (white)
                        Image = Resource.white_King;
                    else
                        Image = Resource.black_King;
                    break;
                case "Pawn":
                    Position = new Position(pawnRow, pawnColm);

                    if (white)
                        Image = Resource.white_Pawn;
                    else
                        Image = Resource.black_Pawn;
                    break;
            }
        }

        public Image Image { get { return image; } private set { image = value; } }
        public Position Position { get { return pos; } private set { pos = value; } }
        public String Name { get; set; }
    }
}