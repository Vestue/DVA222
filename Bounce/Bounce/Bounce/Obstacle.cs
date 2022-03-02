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
        protected PointF Position;
        protected float Length;
        // These determine the size of all lines
        protected int MinSize = 10;
        protected int MaxSize = 200;

        // Currently uses x and y values of the center to determine how close the obstacles can spawn.
        // This is to prevent obstacles from being able to spawn right in the area where circles spawn, causing the simulation to be useless.
        //! This area could be determined by a class by itself instead of needing to send 4 variables to each object.
        protected Obstacle(float x, float y)
        {
            Random random = new Random();
            // Dessa kan möjligtvis bara läggas in direkt i if-satserna där de används istället för att hoppa till variabler
            float spawnMinX = x - MinSize;
            float spawnMaxX = x + MinSize;
            float spawnMinY = y - MinSize;
            float spawnMaxY = y + MinSize;

            // The obstacle is either spawned to the left or to the right of the spawnbox. 
            // THERE SHOULD BE A BETTER WAY TO GET THE SPAWNBOX MIN AND MAX VALUES
            //! AND TO GET THE MIN AND MAX VALUES OF THE SIMULATION !!!!!
            // This assumes that the min coordinate of the simulation is 0 and the maxiumum is 1000.
            // This is not the case since the balls go much further.

            // As objects are rendered left to right and top to bottom the min x and max y need to be further away from the centre point.
            if (random.Next(0, 2) == 0)
            {
                x = random.Next(-10 * MinSize, (int)spawnMinX - MinSize);
            }
            else
            {
                x = random.Next((int)spawnMaxX, 100 * MinSize);
            }
            // The object is placed above or below the spawnbox.
            if (random.Next(0, 2) == 0)
            {
                y = random.Next(-10 * MinSize, (int)spawnMinY);
            }
            else
            {
                y = random.Next((int)spawnMaxY + MinSize, 100 * MinSize);
            }

            Position = new PointF(x, y);
            Length = random.Next(MinSize,MaxSize);
        }
    }
}
