using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bounce
{
    class SpeedDownBox : Box
    {
        Pen Pen = new Pen(Color.Blue);
        public BoxSpeedUp(int x, int y) => base(x, y);
        // Denna kommer speedaUpp under resten av simulationen.
        // Det ska endast vara just när bollen är innanför lådan.
        // FIXA!!!!
        public override void OnCollision(Ball ball) => ball.UpdateSpeed(0.75, Axis.xy);
        public override void DrawObject(Graphics g)
        {

        }
    }
}
