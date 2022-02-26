﻿using System;
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
        protected int minSize = 10;
        protected int maxSize = 100;

        // Currently uses x and y values of the center to determine how close the obstacles can spawn.
        // This is to prevent obstacles from being able to spawn right in the area where circles spawn, causing the simulation to be useless.
        //! This area could be determined by a class by itself instead of needing to send 4 variables to each object.
        public Obstacle(float x, float y)
        {
            // Dessa kan möjligtvis bara läggas in direkt i if-satserna där de används istället för att hoppa till variabler
            float spawnMinX = x - maxSize;
            float spawnMaxX = x + maxSize;
            float spawnMinY = y - maxSize;
            float spawnMaxY = y + maxSize;

            // The obstacle is either spawned to the left or to the right of the spawnbox. 
            // THERE SHOULD BE A BETTER WAY TO GET THE SPAWNBOX MIN AND MAX VALUES
            //! AND TO GET THE MIN AND MAX VALUES OF THE SIMULATION !!!!!
            // This assumes that the min coordinate of the simulation is 0 and the maxiumum is 1000.
            // This is not the case since the balls go much further.

            // As objects are rendered left to right and top to bottom the min x and max y need to be further away from the centre point.
            if (Random.Next(0, 2) == 0)
            {
                x = Random.Next(-10 * maxSize, (int)spawnMinX - maxSize);
            }
            else
            {
                x = Random.Next((int)spawnMaxX, 10 * maxSize);
            }
            // The object is placed above or below the spawnbox.
            if (Random.Next(0, 2) == 0)
            {
                y = Random.Next(-10 * maxSize, (int)spawnMinY);
            }
            else
            {
                y = Random.Next((int)spawnMaxY + maxSize, 10 * maxSize);
            }

            Position = new PointF(x, y);
            Length = Random.Next(minSize,maxSize);
        }
        public abstract void CheckCollision(PointF position, float radius);
        public abstract void DrawObject(Graphics g, ObstacleType obstacle);
        public abstract void UpdateSpeed(PointF speed);
    }
}