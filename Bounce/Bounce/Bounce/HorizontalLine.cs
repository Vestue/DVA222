using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bounce
{
    internal abstract class HorizontalLine : Obstacle
    {
        public override bool CheckCollision(PointF ballPosition, float radius)
        {
            PointF lineVector = PointF(Position.X + Length, Position.Y) - Position;
            PointF startToBall = Position - ballPosition;
        }
    }
}
