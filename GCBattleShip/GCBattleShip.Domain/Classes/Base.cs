using System;

namespace GCBattleShip.Domain.Classes
{
    public class Base
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public Base()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;
        }
    }
}