using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders
{
    internal class SpaceDefendersGame
    {
        public void Run()
        {
            var width = 40;
            var height = 20;
            var renderer = new ConsoleRenderer(width, height);
            var engine = new GameEngine(width, height);

            var running = true;

            while (running)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.Escape:
                            running = false;
                            break;
                        case ConsoleKey.LeftArrow:
                            engine.Move(Player.Direction.Left);
                            break;
                        case ConsoleKey.RightArrow:
                            engine.Move(Player.Direction.Right);
                            break;
                        case ConsoleKey.Spacebar:
                            engine.Fire();
                            break;
                    }
                }

                engine.Tick();
                Console.SetCursorPosition(0, 0);
                renderer.Clear();
                engine.Draw(renderer);
                renderer.Display();

                if (engine.GameOver)
                {
                    running = false;
                }

                Thread.Sleep(1000 / 10);
            }
        }
    }
}
