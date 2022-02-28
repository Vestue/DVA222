using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bounce
{
    class BoxSpeedDown : Box
    {
        Pen Pen = new Pen(Color.Blue);
        public BoxSpeedDown(int x, int y)
        {
            base(x, y);
        }

        // Denna kommer speedaUpp under resten av simulationen.
        // Det ska endast vara just när bollen är innanför lådan.
        // FIXA!!!!
        public override void OnCollision(Ball ball)
        {
            if (ball.SpeedUpdated == false)
            {
                ball.SpeedBeforeUpdate = new PointF(ball.Speed);
                ball.UpdateSpeed(0.75, Axis.xy);
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
