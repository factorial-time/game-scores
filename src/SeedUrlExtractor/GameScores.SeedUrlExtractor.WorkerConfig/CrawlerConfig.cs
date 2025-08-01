using System;

namespace GameScores.SeedUrlExtractor.WorkerConfig;

public sealed record CrawlerConfig
{
    public Uri TargetUrl { get; init; }
    
    public TimeSpan RefreshInterval { get; init; }
}