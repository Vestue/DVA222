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

        public Ball Ball
        {
            get => default;
            set
            {
            }
        }

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
				var ball = new Ball(400, 300, 10);
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
