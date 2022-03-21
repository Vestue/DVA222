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
       public Food(int x, int y, Engine engine)
        {
            Engine = engine;
            Square.Width = Engine.GameObjectSize - 1;
            Square.Height = Engine.GameObjectSize - 1;
            Square.X = x;
            Square.Y = y;
            color = random.Next(0, 3);
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
            Engine.Remove(this);
        }
    }
}
