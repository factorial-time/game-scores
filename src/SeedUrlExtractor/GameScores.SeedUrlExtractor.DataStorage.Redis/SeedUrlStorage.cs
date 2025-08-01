using GameScores.SeedUrlExtractor.DataStorage.Contracts;
using GameScores.SeedUrlExtractor.Dto;

namespace GameScores.SeedUrlExtractor.DataStorage.Redis;

public class SeedUrlStorage : ISeedUrlStorage
{
    public Task SaveAsync(SeedUrl seedUrl, CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}