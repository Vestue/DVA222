using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bounce
{
    internal class Obstacle
    {
        protected Pen Pen = new Pen(Color.Black);
        protected PointF Position;
        protected float Length;
        // These determine the size of all lines
        protected int MinSize = 10;
        protected int MaxSize = 200;

        // Currently uses x and y values of the center to determine how close the obstacles can spawn.
        // This is to prevent obstacles from being able to spawn right in the area where circles spawn, causing the simulation to be useless.
        protected Obstacle(float x, float y)
        {
            Random random = new Random();

            float spawnMinX = x - MinSize;
            float spawnMaxX = x + MinSize;
            float spawnMinY = y - MinSize;
            float spawnMaxY = y + MinSize;

            // As objects are rendered left to right and top to bottom,
            // the min x and max y need to be further away from the centre point.

            // Place to the left of spawnbox
            if (random.Next(0, 2) == 0)
            {
                x = random.Next(-10 * MinSize, (int)spawnMinX - MinSize);
            }
            // Place to the right of spawnbox
            else
            {
                x = random.Next((int)spawnMaxX, 100 * MinSize);
            }

            // Place below spawnbox
            if (random.Next(0, 2) == 0)
            {
                y = random.Next(-10 * MinSize, (int)spawnMinY);
            }
            // Place above spawnbox
            else
            {
                y = random.Next((int)spawnMaxY + MinSize, 100 * MinSize);
            }

            Position = new PointF(x, y);
            Length = random.Next(MinSize,MaxSize);
        }
    }
}
