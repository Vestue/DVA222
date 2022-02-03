using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders
{
    internal class Alien : IAnimatable
    {
        float x, y, Speed;
        Random Random = new Random();
        GameEngine Engine;

        public Alien(GameEngine engine)
        {
            Engine = engine;

            var startPos = Random.Next(0, 2); // Slumpar 0 eller 1
            Speed = Random.Next(5, 15);
            if (startPos == 0)
            {
                x = 0;
            }
            else
            {
                x = Engine.Width - 1;
                Speed = -Speed;
            }
            y = Random.Next(Engine.Height) * 0.8f;
        }

        public void Tick()
        {
            x += Speed / 10.0f;
            if ( x < 0 || x > Engine.Width - 1 )
            {
                Engine.Remove(this);
                return;
            }

            if (Random.Next(100) <= 5)
            {
                var bomb = new Bomb(x, y + 1, Engine);
                Engine.Add(bomb);
            }
        }

        public void Draw(IRenderer renderer)
        {
            renderer.Draw(x, y, Entity.Alien);
        }
    }
}
