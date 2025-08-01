using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameScores.GamesCollector.ServiceDiscovery.Contracts;

public interface IServiceRegistry
{
    Task RegisterAsync(string serviceId, CancellationToken stoppingToken);

    Task<IEnumerable<string>> GetGroupMembersAsync(string group, CancellationToken stoppingToken);

    event EventHandler<ServiceGroupChangedEventArgs> ServiceGroupChanged;
}