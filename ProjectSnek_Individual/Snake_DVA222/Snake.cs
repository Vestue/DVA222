using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_DVA222
{
    internal class Snake
    {
        Direction Dir;
        SolidBrush pen = new SolidBrush(Color.Purple);
        
        public int Points { get; set; } = 0;
        List<Coordinate> BodyCords = new List<Coordinate>();
        int BodyPartsToAdd = 0;
        public int ID { get; private set; }
        Engine Engine { get; set; }

        // *INDIVIDUAL ASSIGNMENT*
        // Higher value means slower speed
        private readonly int _defaultSpeed = 5;
        public int Speed { get; private set; } = 5;
        System.Windows.Forms.Timer _speedUpTimer = new System.Windows.Forms.Timer();
        private int _speedTickCount;

        public Snake(int length, Coordinate startPos, int playerNumber, Engine engine)
        {
            ID = playerNumber;
            Dir = Direction.Up;
            Engine = engine;
            Colorize();
            for (int i = 0; i < length; i++)
            {
                // startPos is the position of the head of the snake;
                // The snake's body is placed in the opposite direction of the starting direction
                switch (Dir)
                {
                    case Direction.Up:
                        BodyCords.Add(new Coordinate(startPos.X, startPos.Y + i * Engine.GameObjectSize));
                        break;
                    case Direction.Down:
                        BodyCords.Add(new Coordinate(startPos.X, startPos.Y - i * Engine.GameObjectSize));
                        break;
                    case Direction.Left:
                        BodyCords.Add(new Coordinate(startPos.X + i * Engine.GameObjectSize, startPos.Y));
                        break;
                    case Direction.Right:
                        BodyCords.Add(new Coordinate(startPos.X - i * Engine.GameObjectSize, startPos.Y));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            // *INDIVIDUAL ASSIGNMENT*
            _speedUpTimer.Tick += _speedUpTimer_Tick;
            _speedUpTimer.Interval = 1000;
        }

        // *INDIVIDUAL ASSIGNMENT*
        private void _speedUpTimer_Tick(object? sender, EventArgs e)
        {
            if (_speedTickCount >= 10)
            {
                _speedUpTimer.Stop();
                ResetSpeed();
                _speedTickCount = 0;
                Colorize();
                return;
            }
            if (_speedTickCount % 2 == 0) Colorize();
            else pen.Color = Color.OrangeRed;
            _speedTickCount++;
        }

        public int GetPoints() => Points;

        public void Move(int width, int height)
        {
            // If there are body parts left to add (after food is eaten) 
            // then don't remove the last one until there are no more body parts to add
            if (BodyPartsToAdd > 0)
            {
                BodyPartsToAdd--;
            }
            else if (BodyPartsToAdd < 0)
            {
                BodyPartsToAdd++;
                BodyCords.RemoveAt(BodyCords.Count - 1);
                BodyCords.RemoveAt(BodyCords.Count - 1);
            }
            else
            {
                // Remove the last body part
                BodyCords.RemoveAt(BodyCords.Count - 1);
            }

            // Add a new body part in front of the head in the direction the snake is going in
            switch (Dir)
            {
                case Direction.Up:
                    if (BodyCords[0].Y - 1 < 0) { Hit(); }
                    BodyCords.Insert(0, new Coordinate(BodyCords[0].X, BodyCords[0].Y - Engine.GameObjectSize));
                    break;
                case Direction.Down:
                    if (BodyCords[0].Y + 1 > height) { Hit(); }
                    BodyCords.Insert(0, new Coordinate(BodyCords[0].X, BodyCords[0].Y + Engine.GameObjectSize));
                    break;
                case Direction.Left:
                    if (BodyCords[0].X - 1 < 0) { Hit(); }
                    BodyCords.Insert(0, new Coordinate(BodyCords[0].X - Engine.GameObjectSize, BodyCords[0].Y));
                    break;
                case Direction.Right:
                    if (BodyCords[0].X + 1 > width) { Hit(); }
                    BodyCords.Insert(0, new Coordinate(BodyCords[0].X + Engine.GameObjectSize, BodyCords[0].Y));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Direction SetDirection(Direction dir) => Dir = dir;

        public Direction GetDirection() => Dir; 
        public List<Coordinate> GetBodyCords() => BodyCords;

        private void Hit()
        {
            // Hit function for hitting a wall or bodypart
            Engine.Remove(this);
        }

        public void Hit(int points, int lengthIncrease)
        {
            // Hit() function for Food
            Points += points;
            BodyPartsToAdd += lengthIncrease;

            if (BodyCords == null)
                Engine.Remove(this);
        }

        public bool SnakeCollide(Snake snake)
        {
            // Works for both other snakes and itself
            if (snake == null) throw new ArgumentNullException();

            List<Coordinate> otherBodyCords = snake.GetBodyCords();

            for (int i = 0; i < otherBodyCords.Count; i++)
            {
                // If the snake is checking its own body then don't check the head (would always falsely return true)
                if (i == 0 && this == snake) i++;
                // You only need to check if the head of this snake is hitting 
                if (BodyCords[0].X == otherBodyCords[i].X && BodyCords[0].Y == otherBodyCords[i].Y)
                {
                    Hit();
                    snake.Points += 5;
                    return true;
                }
            }
            return false;
        }

        public void Draw(Graphics g)
        {
            Rectangle rect = new Rectangle();
            rect.Width = Engine.GameObjectSize;
            rect.Height = Engine.GameObjectSize;
            for (int i = 0; i < BodyCords.Count - 1; i++)
            {
                rect.X = BodyCords[i].X;
                rect.Y = BodyCords[i].Y;
                g.FillRectangle(pen, BodyCords[i].X, BodyCords[i].Y, Engine.GameObjectSize, Engine.GameObjectSize);
                ControlPaint.DrawBorder(g, rect, Color.Black, ButtonBorderStyle.Solid);
            }
        }

        public void Colorize()
        {
            // *INDIVIDUAL ASSIGNMENT*
            switch (ID)
            {
                case 1:
                    pen.Color = Color.Purple;
                    break;
                case 2:
                    pen.Color = Color.Green;
                    break;
                case 3:
                    pen.Color = Color.HotPink;
                    break;
            }
        }

        // *INDIVIDUAL ASSIGNMENT*
        public void ResetSpeed() => Speed = _defaultSpeed;
        public void UpdateSpeed(int increaseAmount)
        {
            _speedUpTimer.Start();
            Speed -= increaseAmount;
            pen.Color = Color.OrangeRed;
            if (Speed <= 0)
            {
                Colorize();
                ResetSpeed();
            }
        }
    }
}