namespace GameScores.GamesStorageService.RestApi.Models;

public record GetPageRequest
{
    public int PageIndex { get; init; } = 1;

    public int PageSize { get; init; } = 25;
}