using System;
using GCBattleShip.Domain.Interfaces.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GCBattleShip.Infrastructure.MongoDb
{
    public class MongoDatabaseFactory : IServiceFactory<IMongoDatabase>
    {
        private readonly MongoDbSettings _settings;
        private static IMongoDatabase _database;

        public MongoDatabaseFactory(IOptions<MongoDbSettings> settings)
        {
            _settings = settings.Value;            
        }

        public IMongoDatabase Build()
        {
            if (_database != null) return _database;           

            try
            {
                var client = new MongoClient(_settings.Connectionstring);                                                       

                _database = client.GetDatabase(_settings.Database);

                return _database;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
    }
}