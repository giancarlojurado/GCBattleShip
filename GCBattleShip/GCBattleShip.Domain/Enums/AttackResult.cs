using System.Runtime.Serialization;

namespace GCBattleShip.Domain.Enums
{
    public enum AttackResult
    {
        [EnumMember(Value = "hit")]
        Hit,
        
        [EnumMember(Value = "miss")]
        Miss
    }
}