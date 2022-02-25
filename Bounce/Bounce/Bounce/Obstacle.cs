using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bounce
{
    internal abstract class Obstacle : IObstacle
    {
        protected PointF Position;
        protected float Length;
        Random Random = new Random();

        // Currently uses min and max values to create a space of where obstacles cannot spawn.
        // This is to prevent obstacles from being able to spawn right in the area where circles spawn, causing the simulation to be useless.
        //! This area could be determined by a class by itself instead of needing to send 4 variables to each object.
        public Obstacle(int xMin, int yMin, int xMax, int yMax)
        {

        }
        public abstract void CheckCollision(PointF Position);
        public abstract void DrawObject(Graphics g, ObstacleType obstacle);
        public abstract void UpdateSpeed(PointF Speed);
    }
}
