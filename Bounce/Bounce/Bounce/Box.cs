using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bounce
{
    internal abstract class Box : Obstacle, IObstacle
    {
        PointF topLeft;
        PointF topRight;
        PointF bottomLeft;
        PointF bottomRight;
        float Heigth;

        protected Box(float x, float y) : base(x, y)
        {
            Random random = new Random();
            Heigth = random.Next(MinSize, MaxSize);

            topLeft = new PointF(Position.X, Position.Y);
            topRight = new PointF(Position.X + Length, Position.Y);
            bottomLeft = new PointF(Position.X, Position.Y - Heigth);
            bottomRight = new PointF(Position.X + Length, Position.Y - Heigth);
        }
        public bool CheckCollision(PointF ballPosition, float radius)
        {
            var midX = (topLeft.X + topRight.X) / 2;
            var midY = (topLeft.Y + bottomLeft.Y) / 2;

            PointF ballDistance = new PointF(Math.Abs(ballPosition.X - midX), Math.Abs(ballPosition.Y - midY));

            if (ballDistance.X > Length / 2 + radius) return false;
            if (ballDistance.Y > Heigth / 2 + radius) return false;

            if (ballDistance.X <= Length / 2) return true;
            if (ballDistance.Y <= Heigth / 2) return true;

            var cornerDistance = Math.Pow((ballDistance.X - Length / 2), 2) + Math.Pow((ballDistance.Y - Heigth / 2), 2);
            return cornerDistance <= Math.Pow(radius, 2);
        }
        public void Draw(Graphics g)
        {
            g.DrawLine(Pen, topLeft, topRight);
            g.DrawLine(Pen, topLeft, bottomLeft);
            g.DrawLine(Pen, bottomLeft, bottomRight);
            g.DrawLine(Pen, topRight, bottomRight);
        }
        public abstract void OnCollision(Ball ball);
    }
}
