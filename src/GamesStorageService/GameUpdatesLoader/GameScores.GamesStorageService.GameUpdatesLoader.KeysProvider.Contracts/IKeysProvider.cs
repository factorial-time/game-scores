using System;
using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;

namespace GameScores.GamesStorageService.GameUpdatesLoader.KeysProvider.Contracts;

public interface IKeysProvider
{
    Task<Guid> GetKeyAsync(Game game, CancellationToken stoppingToken);
}