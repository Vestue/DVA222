using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders_VT22
{
    internal class Shot : IAnimatable
    {

        float x, y, Speed = 5;
        public Player Owner { get; }
        public int Damage { get; } = 1;
        GameEngine Engine;

        public Shot(float x, float y, GameEngine engine, Player owner)
        {
            Owner = owner;
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

        public void TryHit(Alien alien)
        {
            if (alien.CheckHit(x, y))
            {
                alien.OnHit(this);
                Engine.Remove(this);
            }
        }
    }
}
