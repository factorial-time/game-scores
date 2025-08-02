using System;
using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;
using GameScores.GamesStorageService.GameUpdatesLoader.DataProcessor.Contracts;
using GameScores.GamesStorageService.GameUpdatesLoader.KeysProvider.Contracts;
using GameScores.GamesStorageService.Storage.Contracts;

namespace GameScores.GamesStorageService.GameUpdatesLoader.DataProcessor;

public class DataProcessor : IDataProcessor
{
    private readonly IKeysProvider _keysProvider;
    
    private readonly IGamesStorage _gamesStorage;
    
    public DataProcessor(IKeysProvider keysProvider, IGamesStorage gamesStorage)
    {
        _keysProvider = keysProvider;
        _gamesStorage = gamesStorage;
    }
    
    public async Task<Game> ProcessGameAsync(Game game, CancellationToken stoppingToken)
    {
        Guid key = await _keysProvider.GetKeyAsync(game, stoppingToken);
        throw new System.NotImplementedException();
    }
}