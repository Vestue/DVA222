using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bounce
{
    internal class VerticalLine : Line
    {
        Pen Pen = new Pen(Color.Yellow);
        public VerticalLine(int x, int y)
        {
            base(x, y);
            CreateObstacle();
        }
        private override void CreateObstacle()
        {
            startPosition = Position;
            endPosition = new PointF(Position.X, Position.Y - Length);
        }
        public override void OnCollision(Ball ball)
        {
            ball.UpdateSpeed(-1, Axis.x);
        }

        public override void DrawObject(Graphics g)
        {
            g.DrawLine(Pen, startPosition, endPosition);
        }
    }
}
