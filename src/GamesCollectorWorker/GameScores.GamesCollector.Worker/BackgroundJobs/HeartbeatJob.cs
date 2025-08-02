using System;
using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesCollector.ServiceDiscovery.Contracts;
using GameScores.GamesCollector.Worker.Config;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace GameScores.GamesCollector.Worker.BackgroundJobs;

public class HeartbeatJob : BackgroundService
{
    private readonly IServiceRegistry _serviceRegistry;
    
    private readonly string _serviceName;

    public HeartbeatJob(IServiceRegistry serviceRegistry, IOptions<DataCollectorConfig> config)
    {
        _serviceRegistry = serviceRegistry;
        _serviceName = config.Value.TargetUrl.Host;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _serviceRegistry.HeartbeatAsync(_serviceName,
                ServiceIdentifierProvider.Instance.ServiceInstanceId,
                stoppingToken
            );
            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }
}