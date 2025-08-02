using System;

namespace GameScores.GamesStorageService.Dto;

public record SaveGameData : Game
{
    public Guid Id { get; init; }
}