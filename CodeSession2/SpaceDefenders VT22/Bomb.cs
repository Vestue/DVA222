using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders_VT22
{
    internal class Bomb : IAnimatable
    {

        float x, y, Speed = 5;
        GameEngine Engine;
        int Damage;
        Entity Entity;

        public Bomb(float x, float y, GameEngine engine, int damage, Entity entity)
        {
            Damage = damage;
            Entity = entity;
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
            renderer.Draw(x, y, Entity);
        }

        public void TryHit(Player player)
        {
            if (player.CheckHit(x, y))
            {
                Engine.Remove(this);
                player.OnHit(Damage);
            }
        }
    }
}
