using System.Collections.Generic;

namespace GCBattleShip.Domain.Classes
{
    public class Ship
    {
        public List<Location> Location { get; set; }

        public Ship(List<string> locations)
        {
            Location = new List<Location>();
            foreach (var location in locations)
            {
                var gridLocation = new Location
                {
                    GridLocation = location
                };
                Location.Add(gridLocation);
            }
        }
    }
}