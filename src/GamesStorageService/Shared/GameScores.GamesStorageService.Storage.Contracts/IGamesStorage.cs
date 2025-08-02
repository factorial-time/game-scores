using System;
using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;

namespace GameScores.GamesStorageService.Storage.Contracts;

public interface IGamesStorage
{
    Task SaveGameAsync(Guid gameId, Game game, CancellationToken stoppingToken);
}