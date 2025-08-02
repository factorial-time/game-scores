using System.Text;
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
    
    public async Task<string> ObtainKeyAsync(string key, KeyData data, CancellationToken stoppingToken)
    {
        string objectKey = GetObjectKey(data);
        RedisValue storedKey = await _multiplexer.GetDatabase().StringSetAndGetAsync(
            objectKey,
            key,
            //  ToDo: TTL shouldn't be hardcoded. TTL depends on sport type and it should be a part of service contract.
            expiry: TimeSpan.FromHours(2),
            when: When.NotExists
        );

        return storedKey.ToString();
    }

    private static string GetObjectKey(KeyData data)
    {
        //  ToDo: 30 is the average size for 2 team names + separators between key segments.
        //  ToDo: StringBuilder Pool should helps with performance.
        var keyBuilder = new StringBuilder(data.SportType.Length + data.CompetitionName.Length + 50);
        keyBuilder.Append(data.SportType.ToLower());
        keyBuilder.Append(':');
        keyBuilder.Append(data.CompetitionName.ToLower());
        keyBuilder.Append(':');

        foreach (string team in data.Teams)
        {
            keyBuilder.Append(team.ToLower());
            keyBuilder.Append('_');
        }

        return keyBuilder.ToString();
    }
}