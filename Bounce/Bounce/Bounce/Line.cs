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

        VectorMath vectorMath = new VectorMath();

        protected Line(float x, float y) : base(x, y)
        {
        }
        public bool CheckCollision(PointF ballPosition, float radius)
        {
            //PointF startPosition = Position;
            // End position could possibly be set in Obstacle class to increase code reusability.
            //PointF endPosition = PointF(Position.X + Length, Position.Y);

            // Find which point on the line that is closes to the center of the ball.
            PointF lineVector = new PointF(endPosition.X - startPosition.X, endPosition.Y - startPosition.Y);
            PointF lineNorm = new PointF((1 / Length) * lineVector.X, (1 / Length) * lineVector.Y);
            PointF startToBall = new PointF (startPosition.X - ballPosition.X, startPosition.Y - ballPosition.Y);
            var closestPointOnLine = vectorMath.Dot(startToBall, lineVector) / Length;

            // Check if the closest point is the start or end coordinates
            PointF closest;
            if (closestPointOnLine < 0) closest = new PointF(startPosition.X, startPosition.Y);
            else if (closestPointOnLine > Length) closest = new PointF (endPosition.X, endPosition.Y);
            else closest = new PointF(startPosition.X + closestPointOnLine * lineNorm.X, startPosition.Y + closestPointOnLine * lineNorm.Y);

            // Check if the distance from the closest point is less than the radius of the ball.
            // If this is true, the line is within the ball.
            var distanceBetweenBallAndClosest = ballPosition.X - closest.X + ballPosition.Y - closest.Y;

            // Gamla former i ifsatsen
            //Math.Sqrt(vectorMath.Dot(distanceBetweenBallAndClosest, distanceBetweenBallAndClosest))

            if (distanceBetweenBallAndClosest <= radius)
            {
                return true;
            }
            return false;
        }
        public abstract void OnCollision(Ball ball);
        public void DrawObject(Graphics g)
        {
            g.DrawLine(Pen, startPosition, endPosition);
        }
    }
}
