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
    
    public async Task ProcessGameAsync(Game game, CancellationToken stoppingToken)
    {
        Guid gameId = await _keysProvider.GetKeyAsync(game, stoppingToken);
        await _gamesStorage.SaveAsync(gameId, game, stoppingToken);
    }
}