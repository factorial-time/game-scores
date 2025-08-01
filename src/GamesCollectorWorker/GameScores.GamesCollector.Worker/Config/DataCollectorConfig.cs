using System;

namespace GameScores.GamesCollector.Worker.Config;

public sealed record DataCollectorConfig
{
    public Uri TargetUrl { get; init; }
    
    public TimeSpan PullTimeout { get; init; }
}