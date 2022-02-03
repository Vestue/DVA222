using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders
{
    internal class GameEngine
    {
        public int Width { get; }
        public int Height { get; }
        Random Random = new Random();

        Player player;

        List<Alien> Aliens = new List<Alien>();
        List<Bomb> Bombs = new List<Bomb>();
        public GameEngine(int width, int heigth)
        {
            Width = width;
            Height = heigth;

            player = new Player(this);
        }

        // Delegation
        public void Remove(Alien alien)
        {
            Aliens.Remove(alien);
        }

        public void Remove(Bomb bomb)
        {
            Bombs.Remove(bomb);
        }

        public void Add(Bomb bomb)
        {
            Bombs.Add(bomb);
        }

        public void Move(Player.Direction direction)
        {
            player.Move(direction);
        }

        public void Tick()
        {
            Animate();
            if (Random.Next(100) <= 25)
            {
                Aliens.Add(new Alien(this));
            }
        }

        public void Draw(IRenderer renderer)
        {
            foreach (var alien in Aliens)
            {
                alien.Draw(renderer);
            }

            foreach (var bomb in Bombs)
            {
                bomb.Draw(renderer);
            }
            player.Draw(renderer);
        }

        private void Animate()
        {
            var animateables = new List<IAnimatable>();
            animateables.AddRange(Aliens);
            animateables.AddRange(Bombs);

            foreach (var animateable in animateables)
            {
                animateable.Tick();
            }

            // Bomber
        }
    }
}
