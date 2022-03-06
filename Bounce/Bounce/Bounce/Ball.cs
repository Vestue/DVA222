using System;
using System.Drawing;

namespace Bounce
{
	public class Ball
	{
	    Pen Pen = new Pen(Color.Black);

		PointF Position;
		PointF Speed;

		float Radius;

		static Random Random = new Random();

		// Added properties
		public bool SpeedUpdated { get; private set; } = false;
		PointF SpeedBeforeUpdate;
		int timesLeftBox;

		public Ball(float x, float y, float radius)
		{
			Position = new PointF(x,y);
			var xd = Random.Next(1, 6);
			var yd = Random.Next(1, 6);
			if (Random.Next(0, 2) == 0) xd = -xd;
			if (Random.Next(0, 2) == 0) yd = -yd;

			Speed = new PointF(xd,yd);
			Radius = radius;
		}

		public void Draw(Graphics g)
		{
			g.DrawEllipse(Pen,Position.X - Radius, Position.Y - Radius, 2 * Radius, 2 * Radius);
		}

		public void Move()
		{
			Position.X += Speed.X;
			Position.Y += Speed.Y;
		}
		public void TryCollide(IObstacle obstacle)
        {
			if (obstacle.CheckCollision(Position, Radius)) obstacle.OnCollision(this);
			// If the ball has left a obstacle which changes speed inside of it.
			else if (SpeedUpdated && obstacle.CheckCollision(Position, Radius) == false)
            {
				// The collisioncheck fails within a certain interval.
				// To prevent the speed from being reset while inside the box it needs to be checked several times before resetting.
				// 250 times seems to be the optimal number as 200 is clunky and 300 makes them jump too far when speed up.
				timesLeftBox++;
				if (timesLeftBox == 250)
                {
					Speed.X = SpeedBeforeUpdate.X;
					Speed.Y = SpeedBeforeUpdate.Y;
					SpeedUpdated = false;
					timesLeftBox = 0;
				}
            }
        }

		// Ska bara updateras åt ett håll om det är en linje.
		// Både x och y ska uppdateras om det är en rektangel.
		public void UpdateSpeed(float speedFactor, Axis axis)
        {
			switch (axis)
            {
				case Axis.x:
					Speed.X = Speed.X * speedFactor;
					break;
				case Axis.y:
					Speed.Y = Speed.Y * speedFactor;
					break;
				case Axis.xy:
					if (SpeedUpdated == false)
                    {
						SpeedBeforeUpdate = new PointF(Speed.X, Speed.Y);
					}

					Speed.X = Speed.X * speedFactor;
					Speed.Y = Speed.Y * speedFactor;

					// Fungerar ej med denna
					SpeedUpdated = true;
					break;
            }
        }
	}
}
