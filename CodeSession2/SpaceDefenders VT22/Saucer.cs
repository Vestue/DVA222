using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders_VT22
{
    internal class Saucer : Alien
    {

        public Saucer(GameEngine engine) : base(engine, 1, 1)
        {
        }

        protected override void Fire()
        {
            var bomb = new Bomb(x, y + 1, Engine, 1, Entity.Bomb);
            Engine.Add(bomb);
        }
        public override void Draw(IRenderer renderer)
        {
            renderer.Draw(x, y, Entity.Saucer);
        }

        public override bool CheckHit(float x, float y)
        {
            return Math.Abs(this.x - x) <= 1
                && Math.Abs(this.y - y) <= 1;
        }
    }
}
