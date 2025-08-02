using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;
using GameScores.GamesStorageService.UseCases.Contracts;

namespace GameScores.GamesStorageService.UseCases;

public class FetchGamesUseCase : IFetchGamesUseCase
{
    public Task<DataPage<GameInfo>> HandleAsync(int pageIndex, int pageSize, CancellationToken stoppingToken)
    {
        throw new System.NotImplementedException();
    }
}