using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bounce
{
    internal class HorizontalLine : Line
    {
        private override void CreateObject()
        {
            startPosition = Position;
            endPosition = new PointF(Position.X + Length, Position.Y);
        }
    }
}
