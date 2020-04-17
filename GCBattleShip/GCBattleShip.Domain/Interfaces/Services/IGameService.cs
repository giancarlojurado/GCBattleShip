using System.Threading.Tasks;
using GCBattleShip.Domain.Classes;
using GCBattleShip.Domain.Enums;

namespace GCBattleShip.Domain.Interfaces.Services
{
    public interface IGameService
    {
        Task<string> CreateGame();
        Task<Game> GetGame(string id);
        Task AddBattleship(string gameId, BattleshipRequest request);
        Task<AttackResult> Attack(string gameId, string location);
    }
}