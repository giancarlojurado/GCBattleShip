using System.Collections.Generic;

namespace GCBattleShip.Domain.Classes
{
    public class Board
    {
        public List<Ship> Ships { get; set; }
        public List<Location> Attacks { get; set; }

        public Board()
        {
            Ships = new List<Ship>();
            Attacks = new List<Location>();
        }
    }
}