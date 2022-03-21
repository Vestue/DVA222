using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_DVA222
{
    internal class Movement
    {
        public void Move(KeyEventArgs key, Snake snake)
        {
           if(key == null)
            {
                snake.SetDirection(Direction.Right);
            }
            else if (((key.KeyCode == Keys.Up && snake.ID == 2) || (key.KeyCode == Keys.W && snake.ID == 1)) && (snake.GetDirection() != Direction.Down)) snake.SetDirection(Direction.Up);
            else if (((key.KeyCode == Keys.Down && snake.ID == 2) || (key.KeyCode == Keys.S && snake.ID == 1)) && (snake.GetDirection() != Direction.Up)) snake.SetDirection(Direction.Down);
            else if (((key.KeyCode == Keys.Left && snake.ID == 2) || (key.KeyCode == Keys.A && snake.ID == 1)) && (snake.GetDirection() != Direction.Right)) snake.SetDirection(Direction.Left);
            else if (((key.KeyCode == Keys.Right && snake.ID == 2) || (key.KeyCode == Keys.D && snake.ID == 1)) && (snake.GetDirection() != Direction.Left)) snake.SetDirection(Direction.Right);
        }
    }
}
