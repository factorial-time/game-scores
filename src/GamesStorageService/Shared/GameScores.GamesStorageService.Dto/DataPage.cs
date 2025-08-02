using System.Collections.Generic;
using System.Linq;

namespace GameScores.GamesStorageService.Dto;

public record DataPage<T>
{
    public IEnumerable<T> Items { get; init; } = Enumerable.Empty<T>();
    
    public int Total { get; init; }
}