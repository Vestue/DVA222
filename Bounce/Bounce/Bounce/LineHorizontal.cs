using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bounce
{
    internal class LineHorizontal : Line
    {
        public LineHorizontal(float x, float y) : base(x, y)
        {
            startPosition = Position;
            endPosition = new PointF(Position.X + Length, Position.Y);
            Pen = new Pen(Color.Green);
        }
        public override void OnCollision(Ball ball)
        {
            ball.UpdateSpeed(-1, Axis.y);
        }
    }
}
