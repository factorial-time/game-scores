using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;

namespace GameScores.GamesStorageService.Cache.Contracts;

public interface IGamesCache
{
    Task<DataPage<GameInfo>> GetGamesBySportTypeAsync(
        string sportType,
        int pageIndex,
        int pageSize,
        out bool existsInCache,
        CancellationToken stoppingToken);
    
    Task<DataPage<GameInfo>> SetGamesBySportTypeAsync(
        string sportType,
        int pageIndex,
        int pageSize,
        DataPage<GameInfo> games,
        CancellationToken stoppingToken);
    
    Task<DataPage<GameInfo>> GetGamesByCompetitionTypeAsync(
        string competition,
        int pageIndex,
        int pageSize,
        out bool existsInCache,
        CancellationToken stoppingToken);
    
    Task<DataPage<GameInfo>> SetGamesByCompetitionAsync(
        string competition,
        int pageIndex,
        int pageSize,
        DataPage<GameInfo> games,
        CancellationToken stoppingToken);
}