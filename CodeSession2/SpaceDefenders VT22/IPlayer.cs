using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders_VT22
{

    public enum Direction { Left, Right };

    internal interface IPlayer
    {
        void Move(Direction d);
        void Fire();
    }
}
