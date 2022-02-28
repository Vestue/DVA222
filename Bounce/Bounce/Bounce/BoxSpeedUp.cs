using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bounce
{
    internal class BoxSpeedUp : Box
    {
        Pen Pen = new Pen(Color.Red);
        public BoxSpeedUp(int x, int y)
        {
            base(x, y);
        }

        // Denna kommer speedaUpp under resten av simulationen.
        // Det ska endast vara just när bollen är innanför lådan.
        // FIXA!!!!
        public override void OnCollision(Ball ball)
        {
            // Make sure that the speed isn't increase continuesly
            if (ball.speedUpdated == false)
            {
                ball.UpdateSpeed(1.25, Axis.xy);
                ball.speedUpdated = true;
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
