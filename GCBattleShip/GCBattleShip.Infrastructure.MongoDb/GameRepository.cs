using System;
using System.Threading.Tasks;
using GCBattleShip.Domain.Classes;
using GCBattleShip.Domain.Interfaces.Repositories;
using MongoDB.Driver;

namespace GCBattleShip.Infrastructure.MongoDb
{
    public class GameRepository : IGameRepository
    {
        private readonly IMongoCollection<Game> _games;
        
        public GameRepository(IMongoDatabase database)
        {
            _games = database.GetCollection<Game>("games");
        }
        
        public async Task<string> CreateGame()
        {
            var game = new Game();
            
            await _games.InsertOneAsync(game);

            return game.Id;
        }
        
        public async Task<Game> GetGame(string id)
        {
            var games = await _games.FindAsync(a => a.Id.Equals(id));
            var item = games.FirstOrDefault();
            return item;
        }

        public async Task SaveGame(Game game)
        {
            var options = new FindOneAndReplaceOptions<Game>
            {
                IsUpsert = false
            };
            
            var result = await _games.FindOneAndReplaceAsync<Game>(
                d => d.Id.Equals(game.Id),
                game,
                options);
        }
    }
}