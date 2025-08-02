namespace GameScores.GamesStorageService.RestApi.Models;

public record PageResponse<T>
{
    public IEnumerable<T> Items { get; init; }
    
    public int Total { get; init; }
}