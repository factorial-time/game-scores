using System.Threading;
using System.Threading.Tasks;
using GameScores.SeedUrlExtractor.Dto;

namespace GameScores.SeedUrlExtractor.DataStorage.Contracts;

public interface ISeedUrlStorage
{
    Task SaveAsync(SeedUrl seedUrl, CancellationToken stoppingToken);
}