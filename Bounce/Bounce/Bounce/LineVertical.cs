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
        Pen Pen = new Pen(Color.Yellow);
        public LineVertical(int x, int y) : base(x, y)
        {
            CreateObject();
        }
        protected override void CreateObject()
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
