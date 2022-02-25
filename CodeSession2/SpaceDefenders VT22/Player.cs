using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders_VT22
{
    internal class Player : IPlayer
    {

        float x, y, Speed = 10;
        GameEngine Engine;

        int Score = 0;
        int Health = 5;

        public Player(GameEngine engine)
        {
            Engine = engine;
            x = Engine.Width / 2;
            y = Engine.Height - 1;
        }

        public void Move(Direction d)
        {
            switch (d)
            {
                case Direction.Left:
                    x -= Speed / 10f;
                    if (x < 0) x = 0;
                    break;
                case Direction.Right:
                    x += Speed / 10f;
                    if (x > Engine.Width - 1) x = Engine.Width - 1;
                    break;
            }
        }

        public void AddScore(int score) => Score += score;

        public void Fire()
        {
            var shot = new Shot(x, y - 1, Engine, this);
            Engine.Add(shot);
        }

        public void Draw(IRenderer renderer)
        {
            renderer.Draw(x, y, Entity.Player);
            renderer.DrawScore(Score);
            renderer.DrawHealth(Health);
        }

        public void OnHit(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Engine.GameOver = true;
            }
        }

        public bool CheckHit(float x, float y)
        {
            return Math.Abs(this.x - x) <= 1
               && Math.Abs(this.y - y) <= 1;
        }
    }
}
