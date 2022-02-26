using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bounce
{
    class Vector
    {
        public float Dot(float x1, float y1, float x2, float y2) => x1 * x2 + y1 * y2;
        public float Dot(PointF a, PointF b) => a.X * b.X + a.Y * b.Y;
    }
}
