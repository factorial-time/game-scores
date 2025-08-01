using GameScores.GamesCollector.ServiceDiscovery.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace GameScores.GamesCollector.ServiceDiscovery.Redis;

public static class RedisServiceDiscoveryDependencyInjection
{
    public static IServiceCollection AddServiceDiscovery(this IServiceCollection services, IConfiguration config) =>
        services
            .AddSingleton<IConnectionMultiplexer>(_ =>
                ConnectionMultiplexer.Connect(config.GetConnectionString("ServiceDiscovery"))
            )
            .AddSingleton<IServiceRegistry, RedisServiceRegistry>();
}