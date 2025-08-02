using System;
using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;
using GameScores.GamesStorageService.Storage.Contracts;
using GameScores.GamesStorageService.UseCases.Contracts;

namespace GameScores.GamesStorageService.UseCases;

public class GetGameUseCase : IGetGameUseCase
{
    private IGamesStorage _storage;

    public GetGameUseCase(IGamesStorage storage)
    {
        _storage = storage;
    }
    
    public Task<GameInfo?> Handle(Guid gameId, CancellationToken stoppingToken) =>
        _storage.GetByIdAsync(gameId, stoppingToken);
}