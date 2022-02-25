using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders_VT22
{
    internal class ConsoleRenderer : IRenderer
    {
        char[] Field;
        char[] Background;
        int Width, Height;

        readonly char TopLeft = '\u250c';
        readonly char TopRight = '\u2510';
        readonly char BottomLeft = '\u2514';
        readonly char BottomRight = '\u2518';
        readonly char Line = '\u2500';
        readonly char Bar = '\u2502';
        readonly char LeftTCross = '\u251c';
        readonly char RightTCross = '\u2524';

        public ConsoleRenderer(int width, int height)
        { 
            Width = width + 3;
            Height = height + 4;
            Field = new char[Width * Height];
            Background = new char[Width * Height];

            for (var i = 0; i < Background.Length; i++) Background[i] = ' ';
            for (var x = 1; x < Width - 2; x++)
            {
                Background[ToIndex(x, 0)] = Line;
                Background[ToIndex(x, Height-3)] = Line; 
                Background[ToIndex(x, Height-1)] = Line;
            }

            for (var y = 1; y < Height - 3; y++)
            {
                Background[ToIndex(0, y)] = Bar;
                Background[ToIndex(Width-2, y)] = Bar;
            }

            Background[ToIndex(0, 0)] = TopLeft;
            Background[ToIndex(Width-2, 0)] = TopRight;
            Background[ToIndex(0, Height-1)] = BottomLeft;
            Background[ToIndex(Width-2, Height-1)] = BottomRight;
            Background[ToIndex(0, Height-3)] = LeftTCross;
            Background[ToIndex(Width-2, Height-3)] = RightTCross;
            Background[ToIndex(0, Height-2)] = Bar;
            Background[ToIndex(Width-2, Height-2)] = Bar;

            for (var y = 0; y < Height; y++) Background[ToIndex(Width - 1, y)] = '\n';
        }

        public void Clear()
        {
            Array.Copy(Background, Field, Background.Length);
        }

        public void Display()
        {
            Console.Write(Field);
        }

        private int ToIndex(int x, int y)
        {
            return x + y * Width;
        }

        public void Draw(float x, float y, Entity entity)
        {
            var index = ToIndex((int)x + 1, (int)y + 1);

            switch (entity)
            {
                case Entity.Saucer:
                    Field[index] = 'Y';
                    break;
                case Entity.Bomber:
                    if (Field[index - 1] == ' ') 
                        Field[index - 1] = '-';
                    Field[index] = 'O';
                    if (Field[index + 1] == ' ')
                        Field[index + 1] = '-';
                    break;

                case Entity.Player:
                    Field[index] = 'X';
                    break;
                case Entity.Bomb:
                    Field[index] = '.';
                    break;
                case Entity.Nuke:
                    Field[index] = 'o';
                    break;
                case Entity.Shot:
                    Field[index] = '|';
                    break;

                case Entity.Explosion:
                    Field[index] = '*';
                    break;
            }
        }

        public void DrawScore(int score)
        {
            var scoreChars = $"Score: {score}".ToCharArray();
            var index = ToIndex(1, Height - 2);
            Array.Copy(scoreChars, 0, Field, index, scoreChars.Length);
        }

        public void DrawHealth(int health)
        {
            var index = ToIndex(Width - 3, Height - 2);
            for (var i = 0; i < health; i++)
            {
                Field[index - i] = '|';
            }
        }
    }
}
