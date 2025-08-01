using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesCollector.SeedUrlProvider.Contracts;

namespace GameScores.GamesCollector.SeedUrlProvider;

public class MockSeedUrlProvider : ISeedUrlProvider
{
    public Task<IEnumerable<Uri>> FetchSeedUrlsAsync(CancellationToken stoppingToken)
    {
        IEnumerable<Uri> urls =
        [
            new("https://www.livescores.com/football/england/premier-league"),
            new("https://www.livescores.com/football/italy/serie-a"),
            new("https://www.livescores.com/football/spain/laliga"),
            new("https://www.livescores.com/football/world-cup-qualification"),
            new("https://www.livescores.com/basketball/israel"),
            new("https://www.livescores.com/basketball/nba")
        ];

        return Task.FromResult(urls);
    }
}