using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders
{
    internal class Shot : IAnimatable
    {
        float x, y, Speed = 5;
        GameEngine Engine;

        public int X => (int)x;
        public int Y => (int)y;

        public Shot(float x, float y, GameEngine engine)
        {
            this.x = x;
            this.y = y;
            Engine = engine;
        }

        public void Tick()
        {
            y -= Speed / 10f;

            if (y < 0)
            {
                Engine.Remove(this);
            }
        }

        public void Draw(IRenderer renderer)
        {
            renderer.Draw(x, y, Entity.Shot);
        }

        public void Hit(Alien alien)
        {
            alien.OnHit(1);
            Engine.Remove(this);
        }
    }
}
