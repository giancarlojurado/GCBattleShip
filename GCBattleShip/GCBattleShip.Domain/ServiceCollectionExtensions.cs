using System;
using GCBattleShip.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GCBattleShip.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSingletonFactory<T, TFactory>(this IServiceCollection collection)
            where T : class where TFactory : class, IServiceFactory<T>
        {
            collection.AddTransient<TFactory>();
            return AddInternal<T, TFactory>(collection, p => p.GetRequiredService<TFactory>(),
                ServiceLifetime.Singleton);
        }

        private static IServiceCollection AddInternal<T, TFactory>(
            this IServiceCollection collection,
            Func<IServiceProvider, TFactory> factoryProvider,
            ServiceLifetime lifetime) where T : class where TFactory : class, IServiceFactory<T>
        {
            object FactoryFunc(IServiceProvider provider)
            {
                var factory = factoryProvider(provider);
                return factory.Build();
            }

            var descriptor = new ServiceDescriptor(typeof(T), FactoryFunc, lifetime);
            collection.Add(descriptor);
            return collection;
        }
    }
}