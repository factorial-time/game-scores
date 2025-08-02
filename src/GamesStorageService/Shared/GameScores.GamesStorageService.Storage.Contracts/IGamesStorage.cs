using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;

namespace GameScores.GamesStorageService.Storage.Contracts;

public interface IGamesStorage
{
    Task<Game?> GetByIdAsync(Guid gameId, CancellationToken stoppingToken);

    Task<DataPage<Game>> GetListAsync(int pageIndex, int pageSize);
    
    Task SaveAsync(Guid gameId, Game game, CancellationToken stoppingToken);
}