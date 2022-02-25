using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders_VT22
{
    internal class Explosion
    {

        float X, Y;
        GameEngine Engine;

        public Explosion(float x, float y, GameEngine engine)
        {
            Engine = engine;
            X = x;
            Y = y;
            var timer = new System.Timers.Timer(500);
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Engine.Remove(this);
        }

        public void Draw(IRenderer renderer)
        {
            renderer.Draw(X, Y, Entity.Explosion);
        }
    }
}
