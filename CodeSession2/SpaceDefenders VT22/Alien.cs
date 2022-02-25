using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders_VT22
{
    internal abstract class Alien : IAnimatable
    {
        protected float x, y, Speed;
        Random Random = new Random();
        protected GameEngine Engine;
        public int Worth { get; }
        protected int Health;

        public Alien(GameEngine engine, int worth, int health)
        {
            Worth = worth;
            Health = health;
            Engine = engine;

            var startPos = Random.Next(0, 2);
            Speed = Random.Next(5, 15);
            if (startPos == 0)
            {
                x = 0;
            } else
            {
                x = Engine.Width - 1;
                Speed = -Speed;
            }
            y = Random.Next(Engine.Height) * 0.8f;
        }

        public void Tick()
        {
            x += Speed / 10.0f;
            if ( x < 0 || x > Engine.Width - 1)
            {
                Engine.Remove(this);
                return;
            }

            if (Random.Next(100) <= 5)
            {
                Fire();
            }
        }

        protected abstract void Fire();

        public abstract void Draw(IRenderer renderer);

        public void OnHit(Shot shot)
        {
            Health -= shot.Damage;
            if (Health <= 0)
            {
                shot.Owner.AddScore(Worth);
                Engine.Remove(this);
                Engine.Add(new Explosion(x, y, Engine));
            }
        }

        public abstract bool CheckHit(float x, float y);
       
    }
}
