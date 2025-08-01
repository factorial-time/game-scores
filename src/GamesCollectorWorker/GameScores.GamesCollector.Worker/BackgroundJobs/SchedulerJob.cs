using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using GameScores.GamesCollector.SeedUrlProvider.Contracts;
using GameScores.GamesCollector.ServiceDiscovery.Contracts;
using GameScores.GamesCollector.UrlProcessor.Contracts;
using GameScores.GamesCollector.Worker.Config;
using GameScores.GamesCollector.Worker.WorkingSet;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace GameScores.GamesCollector.Worker.BackgroundJobs;

public class SchedulerJob : BackgroundService
{
    private readonly ISeedUrlProvider _seedUrlProvider;

    private readonly IServiceRegistry _serviceRegistry;

    private readonly IUrlProcessor _processor;
    
    private readonly DataCollectorConfig _config;
    
    private readonly string _serviceName;
    
    private readonly Channel<bool> _urlWorkingSetNotifications = Channel.CreateUnbounded<bool>();

    public SchedulerJob(
        ISeedUrlProvider seedUrlProvider,
        IServiceRegistry serviceRegistry,
        IUrlProcessor processor,
        IOptions<DataCollectorConfig> config)
    {
        _seedUrlProvider = seedUrlProvider;
        _serviceRegistry = serviceRegistry;
        _processor = processor;
        _config = config.Value;
        
        _serviceName = _config.TargetUrl.Host;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _serviceRegistry.ServiceGroupChanged += OnServiceGroupChanged;
        await _serviceRegistry.RegisterAsync(
            _serviceName,
            ServiceIdentifierProvider.Instance.ServiceInstanceId,
            stoppingToken
        );
        
        IEnumerable<Uri> workingSet = await _seedUrlProvider.FetchSeedUrlsAsync(stoppingToken);
        UrlWorkingSet.Instance.Refresh(workingSet);

        while (!stoppingToken.IsCancellationRequested)
        {
            string[] groupMembers = (await _serviceRegistry.GetGroupMembersAsync(_config.TargetUrl.Host, stoppingToken))
                .ToArray();
            int currentWorkerIndex = Array.IndexOf(groupMembers, ServiceIdentifierProvider.Instance.ServiceInstanceId);
            foreach (Uri url in UrlWorkingSet.Instance.GetAssignedUrls(currentWorkerIndex, groupMembers.Length))
            {
                await _processor.ProcessAsync(url, stoppingToken);
            }

            await Task.Delay(_config.PullTimeout, stoppingToken);
        }
    }
    
    private void OnServiceGroupChanged(object? sender, ServiceGroupChangedEventArgs e)
    {
        _urlWorkingSetNotifications.Writer.TryWrite(true);
    }
}