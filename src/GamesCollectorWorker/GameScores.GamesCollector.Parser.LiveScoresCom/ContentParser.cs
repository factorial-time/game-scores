using GameScores.GamesCollector.Dto;
using GameScores.GamesCollector.Parser.Contracts;

namespace GameScores.GamesCollector.Parser.LiveScoresCom;

public class ContentParser : IContentParser
{
    public IEnumerable<Game> ExtractGames(Stream content)
    {
        yield return new Game
        {
            SportType = "Football",
            CompetitionName = " Premier League",
            Teams = ["Liverpool", "AFC Bournemouth"],
            EventDate = DateTime.UtcNow
        };
    }
}