using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders
{
    internal class Player
    {
        float x, y, Speed = 10;
        GameEngine Engine;

        public int X => (int)x;
        public int Y => (int)y;

        public Player(GameEngine engine)
        {
            Engine = engine;
            x = Engine.Width / 2;
            y = Engine.Height - 1;
        }

        public enum Direction { Left, Right };
        public void Move(Direction direction)
        {
            switch (direction)
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

        public void Fire()
        {
            var shot = new Shot(x, y - 1, Engine);
            Engine.Add(shot);
        }

        public void Draw(IRenderer renderer)
        {
            renderer.Draw(x, y, Entity.Player);
        }

        public void OnHit(int damage)
        {
            Engine.GameOver = true;
        }
    }
}
