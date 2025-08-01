using GameScores.SeedUrlExtractor.Dto;
using GameScores.SeedUrlExtractor.UrlExtractor.Contracts;

namespace GameScores.SeedUrlExtractor.UrlExtractor.LiveScoresCom;

public class UrlExtractor : IUrlExtractor
{
    public Task<IEnumerable<SeedUrl>> ExtractAsync(Uri baseUrl, CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}