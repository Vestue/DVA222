using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders_VT22
{
    internal class SpaceDefenders
    {
        IRenderer Renderer;
        GameEngine Engine;
        Dictionary<ConsoleKey, IKeyCommand> Controls = new Dictionary<ConsoleKey, IKeyCommand>();
        public SpaceDefenders()
        {
            var width = 40;
            var height = 20;
            Renderer = new ConsoleRenderer(width, height);
            Engine = new GameEngine(width, height);

            var left = new MoveCommand(Engine.Player, Direction.Left);
            Controls[ConsoleKey.LeftArrow] = left;
            var right = new MoveCommand(Engine.Player, Direction.Right);
            Controls[ConsoleKey.RightArrow] = right;
            var fire = new FireCommand(Engine.Player);
            Controls[ConsoleKey.Spacebar] = fire;

        }
        public void Run()
        {
         
            var running = true;

            while (running)
            {
                while (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.Escape:
                            running = false;
                            break;

                        default:
                            if (Controls.ContainsKey(key.Key))
                            {
                                var command = Controls[key.Key];
                                command.Perform();
                            }
                            break;
                    }
                }

                Engine.Tick();

                Console.SetCursorPosition(0, 0);
                Renderer.Clear();
                Engine.Draw(Renderer);
                Renderer.Display();

                if (Engine.GameOver)
                {
                    running = false;
                }

                Thread.Sleep(1000 / 10);
            }
        }
    }
}
