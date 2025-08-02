using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;

namespace GameScores.GamesStorageService.UseCases.Contracts;

public interface IFetchGamesUseCase
{
    Task<DataPage<GameInfo>> HandleAsync(int pageIndex, int pageSize, CancellationToken stoppingToken);
}