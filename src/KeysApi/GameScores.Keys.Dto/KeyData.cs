using System.Collections.Generic;

namespace GameScores.Keys.Dto;

public record KeyData(string SportType, string CompetitionName, IEnumerable<string> Teams);