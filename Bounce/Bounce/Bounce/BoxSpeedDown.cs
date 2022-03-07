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
        public BoxSpeedDown(float x, float y) : base(x, y)
        {
            Pen = new Pen(Color.Blue);
        }
        public override void OnCollision(Ball ball)
        {
            ball.UpdateSpeed((float)0.95, Axis.xy);
        }
    }
}
