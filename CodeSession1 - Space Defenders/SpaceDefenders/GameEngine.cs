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

        public bool GameOver { get; set; } = false;

        Random Random = new Random();

        Player player;

        List<Alien> Aliens = new List<Alien>();
        List<Bomb> Bombs = new List<Bomb>();
        List<Shot> Shots = new List<Shot>();
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

        public void Add(Shot shot) => Shots.Add(shot);
        public void Remove(Shot shot) => Shots.Remove(shot);

        public void Move(Player.Direction direction)
        {
            player.Move(direction);
        }
        public void Fire() => player.Fire();

        public void Tick()
        {
            Animate();
            Collide();
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

            foreach (var shot in Shots)
            {
                shot.Draw(renderer);
            }
            player.Draw(renderer);
        }

        private void Collide()
        {
            var shots = new List<Shot>(Shots);
            var aliens = new List<Alien>(Aliens);
            foreach (var shot in shots)
            {
                foreach (var alien in aliens)
                {
                    if (shot.X == alien.X && shot.Y == alien.Y)
                    {
                        shot.Hit(alien);
                    }
                }
            }

            var bombs = new List<Bomb>(Bombs);
            foreach (var bomb in Bombs)
            {
                if (bomb.X == player.X && bomb.Y == player.Y)
                {
                    bomb.Hit(player);
                }
            }
        }

        private void Animate()
        {
            var animateables = new List<IAnimatable>();
            animateables.AddRange(Aliens);
            animateables.AddRange(Bombs);
            animateables.AddRange(Shots);

            foreach (var animateable in animateables)
            {
                animateable.Tick();
            }

            // Bomber
        }
    }
}
