using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameScores.GamesCollector.SeedUrlProvider.Contracts;

public interface ISeedUrlProvider
{
    Task<IEnumerable<Uri>> FetchSeedUrlsAsync(CancellationToken stoppingToken);
}