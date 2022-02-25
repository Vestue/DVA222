using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders_VT22
{
    internal class MoveCommand : IKeyCommand    
    {
        IPlayer Player;
        Direction Direction;

        public MoveCommand(IPlayer player, Direction direction)
        {
            Player = player;
            Direction = direction;
        }

        public void Perform() => Player.Move(Direction);
    }
}
