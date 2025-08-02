using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;
using GameScores.GamesStorageService.Storage.Contracts;
using GameScores.GamesStorageService.UseCases.Contracts;

namespace GameScores.GamesStorageService.UseCases;

public class FetchGamesUseCase : IFetchGamesUseCase
{
    private IGamesStorage _storage;

    public FetchGamesUseCase(IGamesStorage storage)
    {
        _storage = storage;
    }

    public Task<DataPage<GameInfo>> HandleAsync(int pageIndex, int pageSize, CancellationToken stoppingToken) =>
        _storage.GetListAsync(pageIndex, pageSize, stoppingToken);
}