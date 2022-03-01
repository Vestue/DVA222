using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bounce
{
    internal class BoxSpeedUp : Box
    {
        Pen Pen = new Pen(Color.Red);
        public BoxSpeedUp(int x, int y) : base(x, y)
        {
        }
        public override void OnCollision(Ball ball)
        {
            // Make sure that the speed isn't increase continuesly
            if (ball.SpeedUpdated == false)
            {
                ball.SpeedBeforeUpdate = new PointF(ball.GetSpeed().X, ball.GetSpeed().Y);
                ball.UpdateSpeed((float)1.25, Axis.xy);
                ball.SpeedUpdated = true;
            }
        }
        public override void DrawObject(Graphics g)
        {
            g.DrawLine(Pen, topLeft, topRight);
            g.DrawLine(Pen, topLeft, bottomLeft);
            g.DrawLine(Pen, bottomLeft, bottomRight);
            g.DrawLine(Pen, topRight, bottomRight);
        }
    }
}
