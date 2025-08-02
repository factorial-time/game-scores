using System;
using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;
using GameScores.GamesStorageService.Storage.Contracts;

namespace GameScores.GamesStorageService.Storage.PostgreSql;

public class GamesStorage : IGamesStorage
{
    public Task<GameInfo?> GetByIdAsync(Guid gameId, CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }

    public Task<DataPage<GameInfo>> GetListAsync(int pageIndex, int pageSize, CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(Guid gameId, Game game, CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}