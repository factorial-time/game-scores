using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using GameScores.GamesCollector.ServiceDiscovery.Contracts;
using GameScores.GamesCollector.Worker.Config;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace GameScores.GamesCollector.Worker.BackgroundJobs;

public class ServiceGroupManagerJob : BackgroundService
{
    private readonly IServiceRegistry _serviceRegistry;
    
    private readonly DataCollectorConfig _config;

    private readonly string _serviceName;

    private readonly Channel<bool> _urlWorkingSetNotifications = Channel.CreateUnbounded<bool>();
    
    public ServiceGroupManagerJob(
        IServiceRegistry serviceRegistry,
        IOptions<DataCollectorConfig> config)
    {
        _serviceRegistry = serviceRegistry;
        _config = config.Value;

        _serviceName = _config.TargetUrl.Host;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _serviceRegistry.ServiceGroupChanged += OnServiceGroupChanged;
        
        await _serviceRegistry.RegisterAsync(_serviceName, stoppingToken);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await _urlWorkingSetNotifications.Reader.ReadAsync(stoppingToken);
            var keys = await _serviceRegistry.GetGroupMembersAsync(_serviceName, stoppingToken);
        }
    }
    
    private void OnServiceGroupChanged(object? sender, ServiceGroupChangedEventArgs e)
    {
        _urlWorkingSetNotifications.Writer.TryWrite(true);
    }
}