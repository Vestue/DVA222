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
		bool SpeedAltered = false;
		bool RecentlySwitchedDirection = false;
		PointF SpeedBeforeUpdate;
		int countInBox;
		int countSinceSwitchedDirection;

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
			else if (SpeedAltered || RecentlySwitchedDirection)
            {
				// The collisioncheck fails within a certain interval.
				// To prevent the speed from being reset while inside the box it needs to be checked several times before resetting.
				// 250 times seems to be the optimal number as 200 is clunky and 300 makes them jump too far when speed up.
				if (SpeedAltered) countInBox++;
				if (countInBox == 250)
                {
					Speed.X = SpeedBeforeUpdate.X;
					Speed.Y = SpeedBeforeUpdate.Y;
					SpeedAltered = false;
					countInBox = 0;
				}
				// Counts since collision within boxes and with lines are counted seperately
				// since lines can spawn within boxes and should still work as intended when it occurs.
				if (RecentlySwitchedDirection) countSinceSwitchedDirection++;
				if (countSinceSwitchedDirection == 100) RecentlySwitchedDirection = false;
            }
        }

		// Ska bara updateras åt ett håll om det är en linje.
		// Både x och y ska uppdateras om det är en rektangel.
		public void UpdateSpeed(float speedFactor, Axis axis)
        {
			switch (axis)
            {
				case Axis.x:
					// Adds a small delay before the direction can be change again.
					// This is to balls from getting stuck wiggling in lines.
					if (RecentlySwitchedDirection == false)
                    {
						Speed.X = Speed.X * speedFactor;
						RecentlySwitchedDirection = true;
					}
					break;
				case Axis.y:
					if (RecentlySwitchedDirection == false)
                    {
						Speed.Y = Speed.Y * speedFactor;
						RecentlySwitchedDirection = true;
					}
					break;
				case Axis.xy:
					if (SpeedAltered == false)
                    {
						SpeedBeforeUpdate = new PointF(Speed.X, Speed.Y);
					}

					Speed.X = Speed.X * speedFactor;
					Speed.Y = Speed.Y * speedFactor;

					SpeedAltered = true;
					break;
            }
        }
	}
}
