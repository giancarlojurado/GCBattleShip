using System.Threading.Tasks;
using GCBattleShip.Domain.Classes;

namespace GCBattleShip.Domain.Interfaces.Repositories
{
    public interface IGameRepository
    {
        Task<string> CreateGame();
        Task<Game> GetGame(string id);

        Task SaveGame(Game game);
    }
}