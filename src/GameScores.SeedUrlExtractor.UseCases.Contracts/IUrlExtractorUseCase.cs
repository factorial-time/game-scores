using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameScores.SeedUrlExtractor.UseCases.Contracts;

public interface IUrlExtractorUseCase
{
    Task ExtractUrlAsync(Uri baseUrl, CancellationToken stoppingToken);
}