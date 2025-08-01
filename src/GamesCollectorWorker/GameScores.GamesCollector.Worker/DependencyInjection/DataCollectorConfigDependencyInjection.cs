using GameScores.GamesCollector.Worker.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameScores.GamesCollector.Worker.DependencyInjection;

public static class DataCollectorConfigDependencyInjection
{
    public static IServiceCollection AddDataCollector(this IServiceCollection services, IConfiguration config)
    {
        IConfigurationSection configSection = config.GetSection("DataCollector");
        
        var collectorConfig = new DataCollectorConfig();
        configSection.Bind(collectorConfig);
        services.Configure<DataCollectorConfig>(configSection);
        
        return services;
    }
}