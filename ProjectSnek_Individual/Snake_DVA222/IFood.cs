using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Snake_DVA222
{
    internal interface IFood
    {
        void Draw(Graphics g);

        int returnPoints();

        void TryHit(Snake snake);

        bool intersect(Snake snake);
    }
}
