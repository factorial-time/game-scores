using System;

namespace GameScores.GamesStorageService.GameUpdatesLoader.KeysProvider.Config;

public record KeysApiConfig
{
    public Uri Endpoint { get; init; }
}