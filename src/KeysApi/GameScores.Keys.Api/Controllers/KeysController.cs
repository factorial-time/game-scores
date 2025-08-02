using GameScores.Keys.Api.Contracts;
using GameScores.Keys.Dto;
using GameScores.Keys.UseCases.Contracts;
using Grpc.Core;

namespace GameScores.Keys.Api.Controllers;

public class KeysController: KeysService.KeysServiceBase
{
    private readonly IObtainKeyUseCase _obtainKeyUseCase;

    public KeysController(IObtainKeyUseCase obtainKeyUseCase)
    {
        _obtainKeyUseCase = obtainKeyUseCase;
    }
    
    public override async Task<KeyResponse> GetKey(GetKeyRequest request, ServerCallContext context)
    {
        KeyData keyData = new(request.SportType, request.CompetitionName, request.Teams);
        string key = await _obtainKeyUseCase.ObtainKeyAsync(keyData, context.CancellationToken);
        
        return new KeyResponse {Key = key};
    }
}