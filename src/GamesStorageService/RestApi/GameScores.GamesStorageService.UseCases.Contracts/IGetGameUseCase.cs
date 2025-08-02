using System;
using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;

namespace GameScores.GamesStorageService.UseCases.Contracts;

public interface IGetGameUseCase
{
    Task<GameInfo?> Handle(Guid gameId, CancellationToken stoppingToken);
}