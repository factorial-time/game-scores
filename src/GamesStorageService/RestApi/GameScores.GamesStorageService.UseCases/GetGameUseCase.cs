using System;
using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;
using GameScores.GamesStorageService.UseCases.Contracts;

namespace GameScores.GamesStorageService.UseCases;

public class GetGameUseCase : IGetGameUseCase
{
    public Task<GameInfo?> Handle(Guid gameId, CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}