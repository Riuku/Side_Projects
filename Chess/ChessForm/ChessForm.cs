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

namespace ChessForm
{
    public partial class ChessForm : Form
    {
        private Game game;

        private TextBox name_box = new TextBox();

        public ChessForm()
        {
            InitializeComponent();
            game = new Game();
            chessPanel.SelectionChanged += ChessPanel_SelectionChanged;
            
            InitializeBoardPieces();
        }

        private void ChessPanel_SelectionChanged(Position start, Position end, Piece previous, Piece target)
        {
            game.MovePiece(start, end, previous, target);
            debug.Text = "Moving Piece--> type:" + previous.Name + " from:(" + start.Colm + ", " + start.Row + ") to (" + end.Colm + ", " + end.Row + ")";
        }

        private void InitializeBoardPieces()
        {
            foreach (Piece piece in game.GetWhitePieces())
            {

                int x = piece.Position.Colm;
                int y = piece.Position.Row;
                chessPanel.SetValue(x, y, piece);
            }

            foreach (Piece piece in game.GetBlackPieces())
            {

                int x = piece.Position.Colm;
                int y = piece.Position.Row;
                chessPanel.SetValue(x, y, piece);
            }
        }

        private void ChessForm_Shown(object sender, EventArgs e)
        {
            //name_prompt
            Name_prompt name_prompt = new Name_prompt();


            //show dialog and get result
            DialogResult result = name_prompt.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                turn.Text = name_prompt.textBoxText;
            }
        }

        
    }
}