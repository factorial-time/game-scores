using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;

namespace GameScores.GamesStorageService.GameUpdatesLoader.DataProcessor.Contracts;

public interface IDataProcessor
{
    Task ProcessGameAsync(Game game, CancellationToken stoppingToken);
}