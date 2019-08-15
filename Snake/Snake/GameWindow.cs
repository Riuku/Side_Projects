using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TileMovements
{
    public partial class GameWindow : Form
    {
        private bool up, down, left, right;
        
        private Timer updateHead = new Timer();
        private Random rand = new Random();
        private const int tileSize = 25;
        private const int columns = 30;
        private const int rows = 19;
        private int score = 0;
        private const int speed = 80;

        private PlayerBody player = new PlayerBody(new Point(((columns/2) * tileSize) + 1, ((rows/2) * tileSize) + 1), tileSize);
        private Point food;/*GetRandPoint();*/
        private GamePanel p;
        public GameWindow()
        {
            food = GetRandPoint();
            p = new GamePanel(player, food, tileSize, columns, rows);
            p.Dock = DockStyle.Fill;
            p.Location = new Point(0, 0);
            p.Name = "GamePanel";
            p.Size = new Size(800, 450);
            p.TabIndex = 0;
            this.Controls.Add(p);
            InitializeComponent();

            SetBounds(0, 0, (columns * tileSize) + 50, (rows * tileSize) + 100);
            updateHead.Interval = speed;
            updateHead.Tick += UpdateHead_Tick;
            updateHead.Start();

        }

        private void UpdateHead_Tick(object sender, EventArgs e)
        {
            updatePlayerPosition();
            player.UpdateBodyAtPivot();
            scoreDataLbl.Text = score.ToString();
        }

        private void updatePlayerPosition()
        {
            if (validBounds())
                player.UpdatePosition();
            else
                playerDeath();

            if (player.GetPositions()[0].Equals(food))
                CollectFood();
            Invalidate(true);
        }

        private void playerDeath()
        {
            updateHead.Dispose();
            food = new Point(1000, 1000);
            MessageBox.Show("You have died.\nScore: " + score, "Death", MessageBoxButtons.OK);
            ClearAndReset();

        }

        private void ClearAndReset()
        {
            score = 0;
            food = GetRandPoint();
            p.SetFoodPoint(food);
            player.Reset(rows, columns);
            updateHead.Start();

        }

        private bool validBounds()
        {
            Point head = player.GetPositions()[0];
            switch(player.GetHeadDirection())
            {
                case Direction.UP:
                    if (head.Y - tileSize < 0)
                        return false;
                    else
                        return true;
                case Direction.DOWN:
                    if (head.Y + tileSize > tileSize * rows)
                        return false;
                    else
                        return true;
                case Direction.LEFT:
                    if (head.X - tileSize < 0)
                        return false;
                    else
                        return true;
                case Direction.RIGHT:
                    if (head.X + tileSize > tileSize * columns)
                        return false;
                    else
                        return true;
                default:
                    return true;
            }
        }

        private void GameWindow_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    up = true;
                    break;
                case Keys.Down:
                    down = true;
                    break;
                case Keys.Left:
                    left = true;
                    break;
                case Keys.Right:
                    right = true;
                    break;
            }
            changeDirection();
        }

        private void CollectFood()
        {
            score += 10;
            food = GetRandPoint();
            p.SetFoodPoint(food);
            player.AddBody();
        }

        private void changeDirection()
        {
            bool validMove = true;
            //only 2 valid movements in any case
            if (up && player.GetHeadDirection() != Direction.DOWN && player.GetHeadDirection() != Direction.UP)
                player.SetHeadDirection(Direction.UP);
            else if (down && player.GetHeadDirection() != Direction.UP && player.GetHeadDirection() != Direction.DOWN)
                player.SetHeadDirection(Direction.DOWN);
            else if (left && player.GetHeadDirection() != Direction.RIGHT && player.GetHeadDirection() != Direction.LEFT)
                player.SetHeadDirection(Direction.LEFT);
            else if (right && player.GetHeadDirection() != Direction.LEFT && player.GetHeadDirection() != Direction.RIGHT)
                player.SetHeadDirection(Direction.RIGHT);
            else
                validMove = false;

            if (validMove)
                player.AddPivot(player.GetPositions()[0]);

            up = false;
            down = false;
            left = false;
            right = false;
        }

        private Point GetRandPoint()
        {
            int randX = (rand.Next(columns) * tileSize) + 1;
            int randY = (rand.Next(rows) * tileSize) + 1;

            return new Point(randX, randY);
        }

    }

    public class GamePanel : Panel
    {
        PlayerBody player;
        Point food;
        int tileSize;
        int columns;
        int rows;
        public GamePanel(PlayerBody player, Point food, int tileSize, int columns, int rows)
        {
            this.columns = columns;
            this.rows = rows;
            this.tileSize = tileSize;
            this.player = player;
            this.food = food;
            DoubleBuffered = true;

            
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            Pen border = new Pen(new SolidBrush(Color.FromArgb(37, 37, 37)));

            //vertical border lines
            for (int i = 0; i < columns+1; i++)
            {
                e.Graphics.DrawLine(border, new Point(i * tileSize, 0), new Point(i * tileSize, rows * tileSize));
            }

            //horizontal border lines
            for (int i = 0; i < rows+1; i++)
            {
                e.Graphics.DrawLine(border, new Point(0, i * tileSize), new Point(columns * tileSize, i * tileSize));
            }

            //player
            foreach (Point p in player.GetPositions())
                e.Graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(p, new Size(tileSize - 1, tileSize - 1)));

            //food
            e.Graphics.FillRectangle(new SolidBrush(Color.Green), new Rectangle(food, new Size(tileSize - 1, tileSize - 1)));

            /*/testing
            for (int i=0; i < columns; i++)
            {
                for (int j=0; j < rows; j++)
                {
                    e.Graphics.DrawString(i * tileSize + ", " + j * tileSize, new Font(new FontFamily("Times New Roman"), 8), new SolidBrush(Color.Green), new PointF(i * tileSize + 2, j * tileSize + 2));
                }
            } */
            

            base.OnPaint(e);
        }

        public void SetFoodPoint(Point food)
        {
            this.food = food;
        }

    }
}
