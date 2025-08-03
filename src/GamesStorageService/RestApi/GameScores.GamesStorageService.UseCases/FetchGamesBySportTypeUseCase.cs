using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Cache.Contracts;
using GameScores.GamesStorageService.Dto;
using GameScores.GamesStorageService.Storage.Contracts;
using GameScores.GamesStorageService.UseCases.Contracts;

namespace GameScores.GamesStorageService.UseCases;

public class FetchGamesBySportTypeUseCase : IFetchGamesBySportTypeUseCase
{
    private readonly IGamesCache _cache;
    
    private IGamesStorage _storage;

    public FetchGamesBySportTypeUseCase(IGamesCache cache, IGamesStorage storage)
    {
        _cache = cache;
        _storage = storage;
    }

    public async Task<DataPage<GameInfo>> HandleAsync(
        string competition,
        int pageIndex,
        int pageSize,
        CancellationToken stoppingToken)
    {
        DataPage<GameInfo> dataPage = await _cache.GetGamesBySportTypeAsync(
            competition,
            pageIndex,
            pageSize,
            out bool existsInCache,
            stoppingToken
        );
        if (!existsInCache)
        {
            dataPage = await _storage.GetListBySportTypeAsync(competition, pageIndex, pageSize, stoppingToken);
            await _cache.SetGamesBySportTypeAsync(competition, pageIndex, pageSize, dataPage, stoppingToken);
        }
        
        return dataPage;
    }
}