using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bounce
{
    enum Obstacle { VerticalLine, HorizontalLine, SpeedUpBox, SpeedDownBox }
    internal interface IObstacle
    {
        void UpdateSpeed(PointF Speed);
        void DrawObject(Graphics g, Obstacle obstacle);
        void CheckCollision(PointF Position);
    }
}
