﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bounce
{
    internal class LineHorizontal : Line
    {
        Pen Pen = new Pen(Color.Green);
        public LineHorizontal(float x, float y) : base(x, y)
        {
            CreateObject();
        }
        protected override void CreateObject()
        {
            startPosition = Position;
            endPosition = new PointF(Position.X + Length, Position.Y);
        }
        public override void OnCollision(Ball ball)
        {
            ball.UpdateSpeed(-1, Axis.y);
        }

        public override void DrawObject(Graphics g)
        {
            g.DrawLine(Pen, startPosition, endPosition);
        }
    }
}