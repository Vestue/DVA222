using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bounce
{
    internal class LineVertical : Line
    {
        public LineVertical(float x, float y) : base(x, y)
        {
            startPosition = Position;
            endPosition = new PointF(Position.X, Position.Y - Length);
            Pen = new Pen(Color.Yellow);
        }
        public override void OnCollision(Ball ball)
        {
            ball.UpdateSpeed(-1, Axis.x);
        }
    }
}
