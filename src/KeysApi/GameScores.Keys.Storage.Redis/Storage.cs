using GameScores.Keys.Dto;
using GameScores.Keys.Storage.Contracts;
using StackExchange.Redis;

namespace GameScores.Keys.Storage.Redis;

public class Storage : IStorage
{
    private readonly IConnectionMultiplexer _multiplexer;

    public Storage(IConnectionMultiplexer multiplexer)
    {
        _multiplexer = multiplexer;
    }
    
    public async Task<string> ObtainKeyAsync(KeyData data, CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}