using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bounce
{
    enum ObstacleType { VerticalLine, HorizontalLine, SpeedUpBox, SpeedDownBox }
    internal interface IObstacle
    {
        void UpdateSpeed(PointF Speed);
        void DrawObject(Graphics g, ObstacleType obstacle);
        void CheckCollision(PointF Position);
    }
}
