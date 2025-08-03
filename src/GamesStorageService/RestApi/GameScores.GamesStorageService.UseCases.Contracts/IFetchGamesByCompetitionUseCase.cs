using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;

namespace GameScores.GamesStorageService.UseCases.Contracts;

public interface IFetchGamesByCompetitionUseCase
{
    Task<DataPage<GameInfo>> HandleAsync(string competition, int pageIndex, int pageSize, CancellationToken stoppingToken);
}