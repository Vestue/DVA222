using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bounce
{
    internal class Line : Obstacle, IObstacle
    {
        protected PointF startPosition;
        protected PointF endPosition;

        protected Line(float x, float y)
        {
            base(x, y);
        }
        public override bool CheckCollision(PointF ballPosition, float radius)
        {
            //PointF startPosition = Position;
            // End position could possibly be set in Obstacle class to increase code reusability.
            //PointF endPosition = PointF(Position.X + Length, Position.Y);

            // Find which point on the line that is closes to the center of the ball.
            var lineVector = endPosition - startPosition;
            var lineNorm = (1 / Length) * lineVector;
            var startToBall = startPosition - ballPosition;
            var closestPointOnLine = VectorMath.Dot(startToBall, lineVector) / Length;

            // Check if the closest point is the start or end coordinates
            PointF closest;
            if (closestPointOnLine < 0) closest = startPosition;
            else if (closestPointOnLine > Length) closest = endPosition;
            else closest = startPosition + closestPointOnLine * lineNorm;

            // Check if the distance from the closest point is less than the radius of the ball.
            // If this is true, the line is within the ball.
            var distanceBetweenBallAndClosest = ballPosition - closest;

            if (Math.Sqrt(VectorMath.Dot(distanceBetweenBallAndClosest, distanceBetweenBallAndClosest)) <= radius)
            {
                return true;
            }
            return false;
        }
        protected abstract void CreateObstacle();
        public abstract void OnCollision(Ball ball);
        public abstract void DrawObject(Graphics g);
        public abstract bool CheckCollision(PointF ballPosition, float radius);
    }
}
