using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bounce
{
    internal abstract class Line : Obstacle, IObstacle
    {
        protected PointF startPosition;
        protected PointF endPosition;

        protected Line(float x, float y) : base(x, y)
        {
        }

        // Simplified collisionCheck that interprets the ball as a square
        public bool CheckCollision(PointF ballPosition, float radius)
        {
            // These will need to be checked individually depending on line-type as a small marginal for error
            // is needed on the edges on the line, as balls otherwise risk getting "sucked" into the line
            // when perpendicular to the edge.
            bool withinXRange = false;
            bool withinYRange = false;
            // Horizontal
            if (startPosition.Y == endPosition.Y)
            {
                withinXRange = startPosition.X - radius + 2 <= ballPosition.X && ballPosition.X <= endPosition.X + radius - 2;
                withinYRange = endPosition.Y - radius + 1 <= ballPosition.Y && ballPosition.Y <= startPosition.Y + radius + 1;
            }
            // Vertical
            else if (startPosition.X == endPosition.X)
            {
                withinXRange = startPosition.X - radius + 1 <= ballPosition.X && ballPosition.X <= endPosition.X + radius + 1;
                withinYRange = endPosition.Y - radius + 2 <= ballPosition.Y && ballPosition.Y <= startPosition.Y + radius - 2;
            }
            if (withinXRange && withinYRange) return true;
            return false;
        }
        public abstract void OnCollision(Ball ball);
        public void Draw(Graphics g)
        {
            g.DrawLine(Pen, startPosition, endPosition);
        }
    }
}
