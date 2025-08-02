using System;

namespace GameScores.GamesStorageService.Dto;

public record GameInfo : Game
{
    public Guid Id { get; init; }
}