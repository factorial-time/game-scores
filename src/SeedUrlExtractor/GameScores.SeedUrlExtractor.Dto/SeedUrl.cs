using System;

namespace GameScores.SeedUrlExtractor.Dto;

public record SeedUrl
{
    public Uri DataSourceUrl { get; init; }
    
    public Uri TargetUrl { get; init; }
}