using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GameScores.SeedUrlExtractor.Dto;

namespace GameScores.SeedUrlExtractor.UrlExtractor.Contracts;

public interface IUrlExtractor
{
    Task<IEnumerable<SeedUrl>> ExtractAsync(Uri baseUrl, CancellationToken stoppingToken);
}