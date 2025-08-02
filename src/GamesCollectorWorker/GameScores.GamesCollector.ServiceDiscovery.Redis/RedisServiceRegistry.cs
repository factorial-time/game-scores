using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesCollector.ServiceDiscovery.Contracts;
using StackExchange.Redis;

namespace GameScores.GamesCollector.ServiceDiscovery.Redis;

public class RedisServiceRegistry : IServiceRegistry
{
    private const char GROUP_NAME_SEPARATOR = '/';
    
    private readonly IConnectionMultiplexer _multiplexer;

    public RedisServiceRegistry(IConnectionMultiplexer multiplexer)
    {
        _multiplexer = multiplexer;
    }

    public Task RegisterAsync(string groupId, string serviceId, CancellationToken stoppingToken) =>
        _multiplexer.GetDatabase().StringSetAsync(
            GetServiceKey(groupId, serviceId),
            string.Empty,
            TimeSpan.FromSeconds(30)
        );

    public async Task<IEnumerable<string>> GetGroupMembersAsync(string group, CancellationToken stoppingToken)
    {
        EndPoint endpoint = _multiplexer.GetEndPoints().First();
        IServer server = _multiplexer.GetServer(endpoint);
        
        IAsyncEnumerable<RedisKey> keys = server.KeysAsync(pattern: $"{group}*");

        var groupMembers = new List<string>();
        await foreach (RedisKey key in keys)
        {
            string[] keySegments = key.ToString().Split(GROUP_NAME_SEPARATOR);
            groupMembers.Add(keySegments[1]);
        }
        
        groupMembers.Sort();

        return groupMembers;
    }

    public Task HeartbeatAsync(string groupId, string serviceId, CancellationToken stoppingToken) =>
        _multiplexer.GetDatabase().KeyExpireAsync(
            GetServiceKey(groupId, serviceId),
            TimeSpan.FromSeconds(30)
        );

    private static string GetServiceKey(string groupId, string serviceId) =>
        $"{groupId}{GROUP_NAME_SEPARATOR}{serviceId}";
}