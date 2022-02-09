using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lektion6
{
    internal interface IMouseClick
    {
        void Click(MouseClickData data);
    }

    public enum MouseButton { Left, Middle, Right };

    class MouseClickData
    {
        public MouseButton Button;
    }
}
