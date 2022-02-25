using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders_VT22
{
    enum Entity { Saucer, Bomber, Player, Bomb, Nuke, Shot, Explosion }
    internal interface IRenderer
    {
        void Clear();
        void Draw(float x, float y, Entity entity);
        void DrawScore(int score);
        void DrawHealth(int health);

        void Display();
    }
}
