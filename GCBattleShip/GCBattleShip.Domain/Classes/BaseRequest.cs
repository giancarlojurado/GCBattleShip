using System.ComponentModel.DataAnnotations;

namespace GCBattleShip.Domain.Classes
{
    public class BaseRequest
    {
        [Required]
        [RegularExpression(Common.LocationPattern, ErrorMessage = "Invalid location, should be from A1 to J10")]
        public string Location { get; set; }
    }
}