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
    private const string GROUP_NOTIFICATIONS_CHANNEL = "workers";

    private const char GROUP_NAME_SEPARATOR = '/';
    
    private readonly IConnectionMultiplexer _multiplexer;

    private EventHandler<ServiceGroupChangedEventArgs>? _serviceGroupChanged;
    
    private int _subscribersCount;

    public RedisServiceRegistry(IConnectionMultiplexer multiplexer)
    {
        _multiplexer = multiplexer;
    }
    
    public async Task RegisterAsync(string groupId, string serviceId, CancellationToken stoppingToken)
    {
        var serviceKey = $"{groupId}{GROUP_NAME_SEPARATOR}{serviceId}";
        
        await _multiplexer.GetDatabase().StringSetAsync(serviceKey, string.Empty);

        await _multiplexer.GetSubscriber().PublishAsync(GROUP_NOTIFICATIONS_CHANNEL, string.Empty);
    }

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

    public event EventHandler<ServiceGroupChangedEventArgs>? ServiceGroupChanged
    {
        add
        {
            _serviceGroupChanged += value;
            
            if (Interlocked.Increment(ref _subscribersCount) == 1)
            {
                _multiplexer.GetSubscriber().Subscribe(GROUP_NOTIFICATIONS_CHANNEL, OnServiceGroupChanged);
            }
        }
        remove
        {
            _serviceGroupChanged -= value;

            if (Interlocked.Decrement(ref _subscribersCount) == 0)
            {
                _multiplexer.GetSubscriber().Unsubscribe(GROUP_NOTIFICATIONS_CHANNEL);
            }
        }
    }

    private void OnServiceGroupChanged(RedisChannel channel, RedisValue value)
    {
        var eventHandler = Volatile.Read(ref _serviceGroupChanged);
        if (eventHandler != null)
        {
            eventHandler(this, new ServiceGroupChangedEventArgs());
        }
    }
}