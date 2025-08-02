namespace GameScores.GamesStorageService.RestApi.Models.Game;

public record GameResponse
{
    public Guid Id { get; init; }
    
    public string SportType { get; init; }
    
    public string CompetitionName { get; init; }
    
    public IEnumerable<string> Teams { get; init; }
    
    public DateTime EventDate { get; init; }
}