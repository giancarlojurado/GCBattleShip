using System.ComponentModel.DataAnnotations;
using GCBattleShip.Domain.Enums;

namespace GCBattleShip.Domain.Classes
{
    public class BattleshipRequest : BaseRequest
    {
        [Required]
        [Range(1, 10,ErrorMessage = "Invalid size, should be between 1 and 10")]
        public int Size { get; set; }

        [Required]
        public Direction Direction { get; set; }
    }
}