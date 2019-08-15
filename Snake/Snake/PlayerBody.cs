using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TileMovements
{
    public class PlayerBody
    {
        private List<Direction> directions = new List<Direction>();
        private List<Point> positions = new List<Point>();

        private HashSet<Point> pivots = new HashSet<Point>();
        private int tileSize;
        public PlayerBody(Point head, int tileSize)
        {
            this.tileSize = tileSize;
            positions.Add(head);
            directions.Add(Direction.UP);
            /*
            for (int i = 1; i < 20; i++)
            {
                positions.Add(new Point(head.X, head.Y + tileSize*i));
                directions.Add(Direction.UP);
            }*/
        }
        public void AddBody()
        {
            Point tail = positions[positions.Count - 1];
            Direction dir = directions[directions.Count - 1];

            switch (dir)
            {
                case Direction.UP:
                    positions.Add(new Point(tail.X, tail.Y + tileSize));
                    directions.Add(Direction.UP);
                    break;
                case Direction.DOWN:
                    positions.Add(new Point(tail.X, tail.Y - tileSize));
                    directions.Add(Direction.DOWN);
                    break;
                case Direction.LEFT:
                    positions.Add(new Point(tail.X + tileSize, tail.Y));
                    directions.Add(Direction.LEFT);
                    break;
                case Direction.RIGHT:
                    positions.Add(new Point(tail.X - tileSize, tail.Y));
                    directions.Add(Direction.RIGHT);
                    break;
            }
        }

        public void AddPivot(Point p)
        {
            if (positions.Count > 1)
                pivots.Add(p);
        }
        public void UpdatePosition()
        {
            for (int i = 0; i < positions.Count; i++)
            {
                switch (directions[i])
                {
                    case Direction.UP:
                        positions[i] = new Point(positions[i].X, positions[i].Y - tileSize);
                        break;
                    case Direction.DOWN:
                        positions[i] = new Point(positions[i].X, positions[i].Y + tileSize);
                        break;
                    case Direction.LEFT:
                        positions[i] = new Point(positions[i].X - tileSize, positions[i].Y);
                        break;
                    case Direction.RIGHT:
                        positions[i] = new Point(positions[i].X + tileSize, positions[i].Y);
                        break;
                }

                

            }
        }

        public void Reset(int rows, int columns)
        {
            positions.Clear();
            directions.Clear();
            pivots.Clear();

            positions.Add(new Point(((columns / 2) * tileSize) + 1, ((rows / 2) * tileSize) + 1));
            directions.Add(Direction.UP);
        }

        //fucked up code
        public void UpdateBodyAtPivot()
        {
            for (int i = 0; i < positions.Count; i++)
            {
                if (pivots.Count > 0)
                {

                    for (int j = 0; j < pivots.Count; j++)
                    {
                        if (i > 0 && positions[i] == pivots.ElementAt(j))
                        {
                            directions[i] = directions[i - 1];
                        }
                    }

                    
                }
            }
            if (pivots.Count > 0)
            {
                Point tail = GetTailPosition();
                if (tail == pivots.ElementAt(0))
                    pivots.Remove(tail);
            }
        }
        //end fuck up
        public Direction GetHeadDirection()
        {
            return directions[0];
        }

        public void SetHeadDirection(Direction dir)
        {
            directions[0] = dir;
        }

        private Point GetTailPosition()
        {
            return positions[positions.Count - 1];
        }

        public List<Point> GetPositions()
        {
            return positions;
        }
    }

    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
}
