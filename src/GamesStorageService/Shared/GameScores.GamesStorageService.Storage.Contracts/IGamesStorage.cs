using System;
using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;

namespace GameScores.GamesStorageService.Storage.Contracts;

public interface IGamesStorage
{
    Task<GameInfo?> GetByIdAsync(Guid gameId, CancellationToken stoppingToken);

    Task<DataPage<GameInfo>> GetListAsync(int pageIndex, int pageSize, CancellationToken stoppingToken);
    
    Task SaveAsync(Guid gameId, Game game, CancellationToken stoppingToken);
}