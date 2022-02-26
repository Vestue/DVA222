using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bounce
{
    enum ObstacleType { VerticalLine, HorizontalLine, SpeedUpBox, SpeedDownBox }
    enum Axis { x, y, xy }
    internal interface IObstacle
    {
        void OnCollision(Ball ball);
        void DrawObject(Graphics g, ObstacleType obstacle);
        void CheckCollision(PointF position, float radius);
    }
}
