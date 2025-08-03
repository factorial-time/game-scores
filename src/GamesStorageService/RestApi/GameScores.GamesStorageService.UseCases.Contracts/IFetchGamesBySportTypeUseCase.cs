using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;

namespace GameScores.GamesStorageService.UseCases.Contracts;

public interface IFetchGamesBySportTypeUseCase
{
    Task<DataPage<GameInfo>> HandleAsync(string sportType, int pageIndex, int pageSize, CancellationToken stoppingToken);
}