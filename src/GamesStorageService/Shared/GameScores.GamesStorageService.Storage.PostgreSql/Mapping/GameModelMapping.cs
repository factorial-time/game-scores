namespace GameScores.GamesStorageService.Storage.PostgreSql.Mapping;

internal static class GameModelMapping
{
    public static Dto.GameInfo ToDto(this Models.Game game) => new()
    {
        Id = game.ExternalId,
        SportType = game.SportType,
        CompetitionName = game.CompetitionName,
        Teams = game.Teams,
        EventDate = game.EventDate
    };
}