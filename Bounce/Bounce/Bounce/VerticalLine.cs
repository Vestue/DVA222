using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bounce
{
    internal class VerticalLine : Line
    {
        private void CreateObject()
        {
            startPoint = Position;
            endPoint = new PointF(Position.x, y - Length);
        }
    }
}
