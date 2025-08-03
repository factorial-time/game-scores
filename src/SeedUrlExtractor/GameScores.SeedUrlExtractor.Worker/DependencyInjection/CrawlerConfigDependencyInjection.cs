using GameScores.SeedUrlExtractor.Worker.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameScores.SeedUrlExtractor.Worker.DependencyInjection;

public static class CrawlerConfigDependencyInjection
{
    public static IServiceCollection AddCrawler(this IServiceCollection services, IConfigurationSection config)
    {
        var clientsConfig = new CrawlerConfig();
        config.Bind(clientsConfig);
        services.Configure<CrawlerConfig>(config);
        
        return services;
    }
}