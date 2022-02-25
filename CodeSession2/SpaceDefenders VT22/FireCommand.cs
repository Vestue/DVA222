using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDefenders_VT22
{
    internal class FireCommand : IKeyCommand
    {
        IPlayer Player;
        public FireCommand(IPlayer player)
        {
            Player = player;
        }

        public void Perform() => Player.Fire();
    }
}
