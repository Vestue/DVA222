using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bounce
{
    internal class Box : Obstacle, IObstacle
    {
        protected PointF topLeft;
        protected PointF topRight;
        protected PointF bottomLeft;
        protected PointF bottomRight;
        protected float Heigth;

        protected Box(int x, int y)
        {
            base(x, y);
            CreateObstacle();
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

            var cornerDistance = (ballDistance.X - Length / 2) ^ 2 + (ballDistance.Y - Heigth / 2) ^ 2;
            return (cornerDistance <= radius ^ 2);
        }
        private void CreateObstacle()
        {
            Random random = new Random();
            Heigth = random.Next(MinSize, MaxSize);

            topLeft = new PointF(Position.X, Position.Y);
            topRight = new PointF(Position.X + Length, Position.Y);
            bottomLeft = new PointF(Position.X, Position.Y - Heigth);
            bottomRight = new PointF(Position.X + Length, Position.Y - Heigth);
        }
        public abstract void DrawObject(Graphics g);
        public abstract void OnCollision(Ball ball);
    }
}
