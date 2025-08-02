using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;
using GameScores.GamesStorageService.GameUpdatesLoader.KeysProvider.Contracts;
using GameScores.Keys.Api.Contracts;

namespace GameScores.GamesStorageService.GameUpdatesLoader.KeysProvider;

public class KeysProvider : IKeysProvider
{
    private readonly KeysService.KeysServiceClient _client;

    public KeysProvider(KeysService.KeysServiceClient client)
    {
        _client = client;
    }
    
    public async Task<Guid> GetKeyAsync(Game game, CancellationToken stoppingToken)
    {
        var request = new GetKeyRequest
        {
            SportType = game.SportType,
            CompetitionName = game.CompetitionName
        };
        request.Teams.AddRange(game.Teams);

        var response = await _client.GetKeyAsync(request, cancellationToken: stoppingToken);
        
        return Guid.Parse(response.Key);
    }
}