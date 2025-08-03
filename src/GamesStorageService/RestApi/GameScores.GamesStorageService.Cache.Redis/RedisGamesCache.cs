using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Cache.Contracts;
using GameScores.GamesStorageService.Dto;

namespace GameScores.GamesStorageService.Cache.Redis;

public class RedisGamesCache : IGamesCache
{
    public Task<DataPage<GameInfo>> GetGamesBySportTypeAsync(
        string sportType,
        int pageIndex,
        int pageSize,
        out bool existsInCache,
        CancellationToken stoppingToken) =>
        GetFromCacheAsync(
            GetSportTypeCacheItemKey(sportType, pageIndex, pageSize),
            out existsInCache,
            stoppingToken
        );

    public Task<DataPage<GameInfo>> SetGamesBySportTypeAsync(
        string sportType,
        int pageIndex,
        int pageSize,
        DataPage<GameInfo> games,
        CancellationToken stoppingToken) =>
        SaveInCacheAsync(
            GetSportTypeCacheItemKey(sportType, pageIndex, pageSize),
            games,
            stoppingToken
        );

    public Task<DataPage<GameInfo>> GetGamesByCompetitionTypeAsync(
        string competition,
        int pageIndex,
        int pageSize,
        out bool existsInCache,
        CancellationToken stoppingToken) =>
        GetFromCacheAsync(
            GetCompetitionCacheItemKey(competition, pageIndex, pageSize),
            out existsInCache,
            stoppingToken
        );

    public Task<DataPage<GameInfo>> SetGamesByCompetitionAsync(
        string competition,
        int pageIndex,
        int pageSize,
        DataPage<GameInfo> games,
        CancellationToken stoppingToken) =>
        SaveInCacheAsync(
            GetCompetitionCacheItemKey(competition, pageIndex, pageSize),
            games,
            stoppingToken
        );

    private Task<DataPage<GameInfo>> GetFromCacheAsync(
        string objectKey,
        out bool existsInCache,
        CancellationToken stoppingToken)
    {
        existsInCache = false;
        throw new System.NotImplementedException();
    }
    
    private Task<DataPage<GameInfo>> SaveInCacheAsync(
        string objectKey,
        DataPage<GameInfo> games,
        CancellationToken stoppingToken)
    {
        throw new System.NotImplementedException();
    }
    
    private static string GetSportTypeCacheItemKey(string sportType, int pageIndex, int pageSize) =>
        $"sport:{sportType}:{pageIndex}:{pageSize}";

    private static string GetCompetitionCacheItemKey(string competition, int pageIndex, int pageSize) =>
        $"competition:{competition}:{pageIndex}:{pageSize}";
}