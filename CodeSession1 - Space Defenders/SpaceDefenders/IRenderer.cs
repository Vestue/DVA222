using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders
{
    enum Entity { Alien, Player, Bomb, Shot }
    internal interface IRenderer
    {
        void Clear();
        void Draw(float x, float y, Entity entity);

        void Display();
    }
}
