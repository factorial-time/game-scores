using GameScores.GamesCollector.Dto;

namespace GameScores.GamesCollector.Parser.Contracts;

public interface IContentParser
{
    IEnumerable<Game> ExtractGames(Stream content);
}