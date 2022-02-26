using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bounce
{
    internal abstract class Line : Obstacle
    {
        public override void CheckCollision(PointF ballPosition, float radius)
        {

        }
    }
}
