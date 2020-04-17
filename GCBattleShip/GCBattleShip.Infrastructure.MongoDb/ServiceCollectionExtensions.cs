using GCBattleShip.Domain;
using GCBattleShip.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace GCBattleShip.Infrastructure.MongoDb
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterMongodbServices(this IServiceCollection services)
        {
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddSingletonFactory<IMongoDatabase, MongoDatabaseFactory>();


            var pack = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String)
            };

            ConventionRegistry.Register("EnumStringConvention", pack, t => true);

            return services;
        }
    }
}