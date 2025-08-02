using GameScores.GamesStorageService.RestApi.Models.Game;

namespace GameScores.GamesStorageService.RestApi.Mapping.Game;

internal static class GameResponseMapping
{
    public static GameResponse ToResponse(this Dto.GameInfo game) => new()
    {
        Id = game.Id,
        CompetitionName = game.CompetitionName,
        Teams = game.Teams,
        EventDate = game.EventDate
    };
}