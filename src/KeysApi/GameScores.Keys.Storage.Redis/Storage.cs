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
        await _multiplexer.GetDatabase().StringSetAsync(
            objectKey,
            key,
            //  ToDo: TTL shouldn't be hardcoded. TTL depends on sport type and it should be a part of service contract.
            expiry: TimeSpan.FromHours(2),
            when: When.NotExists
        );

        RedisValue storedKey = await _multiplexer.GetDatabase().StringGetAsync(objectKey);

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
        
        string[] teams = data.Teams.Select(team => team.ToLower()).ToArray();
        Array.Sort(teams);

        foreach (string team in teams)
        {
            keyBuilder.Append(team);
            keyBuilder.Append('_');
        }

        return keyBuilder.ToString();
    }
}