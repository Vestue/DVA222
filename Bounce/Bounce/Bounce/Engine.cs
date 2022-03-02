﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Bounce
{
	public class Engine
	{
		MainForm Form = new MainForm();
		Timer Timer = new Timer();
		List<Ball> Balls = new List<Ball>();
		Random Random = new Random();

		// Added
		float midX;
		float midY;
		List<IObstacle> Obstacles = new List<IObstacle>();

        public void Run()
		{
			midX = Form.Width / 2;
			midY = Form.Height / 2;
			for (int i = 0; i < 50; i++)
			{
				IObstacle boxSpeedDown = new BoxSpeedDown(midX, midY);
				IObstacle boxSpeedUp = new BoxSpeedUp(midX, midY);
				IObstacle lineHorizontal = new LineHorizontal(midX, midY);
				IObstacle lineVertical = new LineVertical(midX, midY);

				// Kollisionen triggras inte inom lådan
				//Obstacles.Add(boxSpeedDown);
				//Obstacles.Add(boxSpeedUp);

				// För linjerna verkar kollisionen fungera någolunda bra, behöver bara stoppa wigglen.
				// Bollarna wigglar horisontellt
				//Obstacles.Add(lineHorizontal);

				// Bollarna wigglar vertikalt
				//Obstacles.Add(lineVertical);
			}

			Form.Paint += Draw;
			Timer.Tick += TimerEventHandler;
			Timer.Interval = 1000/25;
			Timer.Start();

			Application.Run(Form);
		}

		void TimerEventHandler(Object obj, EventArgs args)
		{
			if (Random.Next(100) < 25)
            {
				// Changed to use the size of the form instead of hardcoded x and y.
				// To make sure that the balls spawn in the middle even if the size of the form is changed.
				var ball = new Ball(midX, midY, 10);
				Balls.Add(ball);
			}
			
			foreach (var ball in Balls) ball.Move();
			Form.Refresh();
			Collide();
		}

		void Draw(Object obj, PaintEventArgs args)
		{
			foreach (var ball in Balls) ball.Draw(args.Graphics);
			foreach (var IObstacle in Obstacles) IObstacle.DrawObject(args.Graphics);
		}

		void Collide()
        {
			//var balls = new List<Ball>(Balls);
			//var obstacles = new List<IObstacle>(Obstacles);
			foreach (var ball in Balls)
				foreach (var obstacle in Obstacles)
					ball.TryCollide(obstacle);
        }
	}
}
