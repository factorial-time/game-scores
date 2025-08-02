using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;
using GameScores.GamesStorageService.GameUpdatesLoader.DataProcessor.Contracts;
using GameScores.GamesStorageService.Storage.Contracts;

namespace GameScores.GamesStorageService.GameUpdatesLoader.DataProcessor;

public class DataProcessor : IDataProcessor
{
    private readonly IGamesStorage _gamesStorage;
    
    public DataProcessor(IGamesStorage gamesStorage)
    {
        _gamesStorage = gamesStorage;
    }
    
    public Task<Game> ProcessGameAsync(Game game, CancellationToken stoppingToken)
    {
        throw new System.NotImplementedException();
    }
}