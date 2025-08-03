using System;

namespace GameScores.SeedUrlExtractor.Worker.Config;

public sealed record CrawlerConfig
{
    public Uri TargetUrl { get; init; }
    
    public TimeSpan RefreshInterval { get; init; }
}