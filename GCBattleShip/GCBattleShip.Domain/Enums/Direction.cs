using System.Runtime.Serialization;

namespace GCBattleShip.Domain.Enums
{
    public enum Direction
    {
        [EnumMember(Value = "horizontal")]
        Horizontal,
        
        [EnumMember(Value = "vertical")]
        Vertical,
    }
}