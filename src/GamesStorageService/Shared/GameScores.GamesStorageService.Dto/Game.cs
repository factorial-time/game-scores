using System;
using System.Collections.Generic;

namespace GameScores.GamesStorageService.Dto;

public record Game
{
    public string SportType { get; init; }
    
    public string CompetitionName { get; init; }
    
    public IEnumerable<string> Teams { get; init; }
    
    public DateTime EventDate { get; init; }
}