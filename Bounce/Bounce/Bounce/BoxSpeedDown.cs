using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bounce
{
    class BoxSpeedDown : Box
    {
        Pen Pen = new Pen(Color.Blue);
        public BoxSpeedDown(float x, float y) : base(x, y)
        {
        }
        public override void OnCollision(Ball ball)
        {
            if (ball.SpeedUpdated == false)
            {
                ball.SpeedBeforeUpdate = new PointF(ball.GetSpeed().X, ball.GetSpeed().Y);
                ball.UpdateSpeed((float)0.5, Axis.xy);
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
