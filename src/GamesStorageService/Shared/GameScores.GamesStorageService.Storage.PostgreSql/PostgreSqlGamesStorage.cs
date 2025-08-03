using System;
using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;
using GameScores.GamesStorageService.Storage.Contracts;

namespace GameScores.GamesStorageService.Storage.PostgreSql;

public class PostgreSqlGamesStorage : IGamesStorage
{
    public Task<GameInfo?> GetByIdAsync(Guid gameId, CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }

    public Task<DataPage<GameInfo>> GetListBySportTypeAsync(
        string sportType,
        int pageIndex,
        int pageSize,
        CancellationToken stoppingToken)
    {
        //  ToDo: sport types aren't updated too often.
        //  It's better to avoid filtering by sportType name in SQL query.
        //  For better performance, mapping Sport Type -> Sport Type Id should be cached in memory.
        throw new NotImplementedException();
    }

    public Task<DataPage<GameInfo>> GetListByCompetitionAsync(
        string competition,
        int pageIndex,
        int pageSize,
        CancellationToken stoppingToken)
    {
        //  ToDo: competitions aren't updated too often.
        //  It's better to avoid filtering by competition name in SQL query.
        //  For better performance, mapping Competition -> Competition Id should be cached in memory
        //  and it could be updated by interval, say once a second.
        throw new NotImplementedException();
    }

    public Task SaveAsync(Guid gameId, Game game, CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}