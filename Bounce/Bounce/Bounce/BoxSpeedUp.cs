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
        public BoxSpeedUp(float x, float y) : base(x, y)
        {
            Pen = new Pen(Color.Red);
        }
        public override void OnCollision(Ball ball)
        {
            ball.UpdateSpeed((float)1.1, Axis.xy);
        }
    }
}
