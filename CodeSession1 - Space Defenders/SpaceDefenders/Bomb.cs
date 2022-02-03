using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders
{
    internal class Bomb : IAnimatable
    {
        float x, y, Speed = 5;
        GameEngine Engine;

        public Bomb(float x, float y, GameEngine engine)
        {
            this.x = x;
            this.y = y;
            Engine = engine;
        }

        public void Tick()
        {
            y += Speed / 10f;

            if (y >= Engine.Height)
            {
                Engine.Remove(this);
            }
        }

        public void Draw(IRenderer renderer)
        {
            renderer.Draw(x, y, Entity.Bomb);
        }
    }
}
