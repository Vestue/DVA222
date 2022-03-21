using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Snake_DVA222
{
    internal class Food : IFood
    {
        SolidBrush pen = new SolidBrush(Color.White);
        int value;
        int points;
        Random random = new Random();
        int color;
        
        Rectangle Square = new Rectangle();
        Engine Engine;

        // *INDIVIDUAL ASSIGNMENT*
        private bool _willAlterSpeed = false;
        public Food(int x, int y, Engine engine)
        {
            Engine = engine;
            Square.Width = Engine.GameObjectSize - 1;
            Square.Height = Engine.GameObjectSize - 1;
            Square.X = x;
            Square.Y = y;
            color = random.Next(3, 4);
            switch (color)
            {
                case 0:
                    value = 1;
                    points = 1;
                    pen.Color = Color.Orange;
                    break;
                case 1:
                    value = 2;
                    points = 5;
                    pen.Color = Color.Red;
                    break;
                case 2:
                    value = -1;
                    points = 1;
                    pen.Color= Color.Yellow;
                    break;

                // *INDIVIDUAL ASSIGNMENT*
                case 3:
                    value = 0;
                    // Reward should be worth the risk.
                    points = 8;
                    pen.Color = Color.Blue;
                    _willAlterSpeed = true;
                    break;
            }
        }


        public void Draw(Graphics g)
        {
            g.FillRectangle(pen, Square);
            ControlPaint.DrawBorder(g, Square, Color.Black, ButtonBorderStyle.Solid);
        }

        public bool intersect(Snake snake)
        {
            var BodyCord = snake.GetBodyCords();
            var SnakeHeadPos = BodyCord[0];

            bool withinXRange = false;
            bool withinYRange = false;

            if (SnakeHeadPos.X - Engine.GameObjectSize <= Square.X && Square.X <= SnakeHeadPos.X + Engine.GameObjectSize)
                withinXRange = true;
            if (SnakeHeadPos.Y - Engine.GameObjectSize <= Square.Y && Square.Y <= SnakeHeadPos.Y + Engine.GameObjectSize)
                withinYRange = true;

            if (withinXRange && withinYRange)
            {
                return true;
            }

            return false;
        }

        public int returnPoints()
        {

            return value;
        }

        public void TryHit(Snake snake)
        {
            snake.Hit(points, value);

            // *INDIVIDUAL ASSIGNMENT*
            if (_willAlterSpeed)
            {
                Engine._snakes[random.Next(0, Engine.CurrentAmountOfPlayers)].UpdateSpeed(random.Next(1, 4));
            }

            Engine.Remove(this);
        }
    }
}
