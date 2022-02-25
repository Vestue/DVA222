using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders_VT22
{
    internal class Bomber : Alien
    {
        public Bomber(GameEngine engine) : base(engine, 2, 2)
        {
        }

        protected override void Fire()
        {
            var bomb = new Bomb(x, y + 1, Engine, 2, Entity.Nuke);
            Engine.Add(bomb);
        }

        public override void Draw(IRenderer renderer)
        {
            renderer.Draw(x, y, Entity.Bomber);
        }

        public override bool CheckHit(float x, float y)
        {
            return
                x >= this.x - 1 && x <= this.x + 1 &&
                y >= this.y - 1 && y <= this.y + 1;
        }
    }
}
