using System;
using System.Collections.Generic;

namespace GameScores.GamesStorageService.Storage.PostgreSql.Models;

internal sealed class Game
{
    public int Id { get; init; }
    
    public Guid ExternalId { get; init; }
    
    public string SportType { get; init; }
    
    public string CompetitionName { get; init; }
    
    public IEnumerable<string> Teams { get; init; }
    
    public DateTime EventDate { get; init; }
    
    public DateTime CreatedAt { get; init; }
    
    public DateTime? UpdatedAt { get; init; }
}