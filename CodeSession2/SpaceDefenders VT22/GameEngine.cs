using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders_VT22
{
    internal class GameEngine
    {
        public int Width { get; }
        public int Height { get; }

        public bool GameOver { get; set; } = false;

        Random Random = new Random();

        Player player;
        public IPlayer Player => player;

        List<Alien> Aliens = new List<Alien>();
        List<Bomb> Bombs = new List<Bomb>();
        List<Shot> Shots = new List<Shot>();
        List<Explosion> Explosions = new List<Explosion>();

        public GameEngine(int width, int height)
        {
            Width = width;
            Height = height;

            player = new Player(this);
        }


        public void Add(Bomb bomb) => Bombs.Add(bomb);
        public void Add(Shot shot) => Shots.Add(shot);
        public void Add(Explosion explosion) => Explosions.Add(explosion);


        public void Remove(Shot shot) => Shots.Remove(shot);
        public void Remove(Alien alien) => Aliens.Remove(alien);
        public void Remove(Bomb bomb) => Bombs.Remove(bomb);
        public void Remove(Explosion explosion) => Explosions.Remove(explosion);



        public void Tick()
        {
            Animate();
            Collide();
            if (Random.Next(100) <= 25)
            {
                if (Random.Next(100) < 50)
                {
                    Aliens.Add(new Saucer(this));
                } 
                else
                {
                    Aliens.Add(new Bomber(this));
                }
            }
        }

        public void Draw(IRenderer renderer)
        {
            foreach (var shot in Shots) shot.Draw(renderer);
            foreach (var bomb in Bombs) bomb.Draw(renderer);
            foreach (var explosion in Explosions) explosion.Draw(renderer);
            foreach (var alien in Aliens) alien.Draw(renderer);

            player.Draw(renderer);
        }

        void Collide()
        {
            var shots = new List<Shot>(Shots);
            var aliens = new List<Alien>(Aliens);
            foreach (var shot in shots) 
                foreach (var alien in aliens)
                    shot.TryHit(alien);

            var bombs = new List<Bomb>(Bombs);
            foreach (var bomb in bombs)
                bomb.TryHit(player);
        }

        void Animate()
        {
            var animatables = new List<IAnimatable>();
            animatables.AddRange(Aliens);
            animatables.AddRange(Bombs);
            animatables.AddRange(Shots);

            foreach (var animatable in animatables)
            {
                animatable.Tick();
            }


        }
    }
}
