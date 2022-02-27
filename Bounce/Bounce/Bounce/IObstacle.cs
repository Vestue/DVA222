using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bounce
{
    //enum ObstacleType { VerticalLine, HorizontalLine, SpeedUpBox, SpeedDownBox }
    enum Axis { x, y, xy }
    internal interface IObstacle
    {
        public void OnCollision(Ball ball);
        public void DrawObject(Graphics g);
        public bool CheckCollision(PointF ballPosition, float radius);
    }
}
