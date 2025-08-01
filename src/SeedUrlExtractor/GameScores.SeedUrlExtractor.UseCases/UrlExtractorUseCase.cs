using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GameScores.SeedUrlExtractor.DataStorage.Contracts;
using GameScores.SeedUrlExtractor.Dto;
using GameScores.SeedUrlExtractor.UrlExtractor.Contracts;
using GameScores.SeedUrlExtractor.UseCases.Contracts;
using Microsoft.Extensions.Logging;

namespace GameScores.SeedUrlExtractor.UseCases;

public class UrlExtractorUseCase : IUrlExtractorUseCase
{
    private readonly IUrlExtractor _extractor;
    
    private readonly ISeedUrlStorage _storage;
    
    private readonly ILogger _logger;

    public UrlExtractorUseCase(IUrlExtractor extractor, ISeedUrlStorage storage, ILogger<UrlExtractorUseCase> logger)
    {
        _extractor = extractor;
        _storage = storage;
        _logger = logger;
    }

    public async Task ExtractUrlAsync(Uri baseUrl, CancellationToken stoppingToken)
    {
        IEnumerable<SeedUrl> urls = await _extractor.ExtractAsync(baseUrl, stoppingToken);
        foreach (SeedUrl url in urls)
        {
            await _storage.SaveAsync(url, stoppingToken);
        }
    }
}