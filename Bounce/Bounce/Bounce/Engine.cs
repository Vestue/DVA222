using System;
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

        public void Run()
		{
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
				var ball = new Ball(Form.Width / 2, Form.Height / 2, 10);
				Balls.Add(ball);
			}
			
			foreach (var ball in Balls) ball.Move();

			Form.Refresh();
		}

		void Draw(Object obj, PaintEventArgs args)
		{
			foreach (var ball in Balls) ball.Draw(args.Graphics);
		}

	}
}
