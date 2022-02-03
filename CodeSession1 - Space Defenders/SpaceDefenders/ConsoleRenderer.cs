using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders
{
    internal class ConsoleRenderer : IRenderer
    {
        char[] Field;
        int Width, Height;

        public ConsoleRenderer(int width, int height)
        {
            Width = width + 1;  // En extra för att få plats med newlines
            Height = height;
            Field = new char[Width * Height];
            Clear();
        }
        public void Clear()
        {
            for (var i = 0; i < Field.Length; i++) Field[i] = ' ';
            for (var y = 0; y < Height; y++) Field[ToIndex(Width - 1, y)] = '\n';
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
            var index = ToIndex((int)x, (int)y);
            
            switch (entity)
            {
                case Entity.Alien:
                    Field[index] = 'Y';
                    break;
                case Entity.Player:
                    Field[index] = 'X';
                    break;
                case Entity.Bomb:
                    Field[index] = '.';
                    break;
                case Entity.Shot:
                    Field[index] = '|';
                    break;
            }
        }
    }
}
