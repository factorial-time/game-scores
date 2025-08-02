using GameScores.GamesStorageService.RestApi.Mapping.Game;
using GameScores.GamesStorageService.RestApi.Models;
using GameScores.GamesStorageService.RestApi.Models.Game;
using GameScores.GamesStorageService.UseCases.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameScores.GamesStorageService.RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private readonly IFetchGamesUseCase _fetchGamesUseCase;
    
    private readonly IGetGameUseCase _getGameUseCase;
    
    public GameController(IFetchGamesUseCase fetchGamesUseCase, IGetGameUseCase getGameUseCase)
    {
        _fetchGamesUseCase = fetchGamesUseCase;
        _getGameUseCase = getGameUseCase;
    }
    
    [HttpGet]
    public async Task<ActionResult<PageResponse<GameResponse>>> GetPage(
        [FromQuery] GetPageRequest request,
        CancellationToken stoppingToken)
    {
        Dto.DataPage<Dto.GameInfo> games = await _fetchGamesUseCase.HandleAsync(
            request.PageIndex,
            request.PageSize,
            stoppingToken
        );
        
        var response = new PageResponse<GameResponse>
        {
            Items = games.Items.Select(g => g.ToResponse()),
            Total = games.Total
        };
        
        return Ok(response);
    }

    [HttpGet("{gameId:guid}")]
    public async Task<ActionResult<GameResponse>> GetById(Guid gameId, CancellationToken stoppingToken)
    {
        Dto.GameInfo game = await _getGameUseCase.Handle(gameId, stoppingToken);
        
        return Ok(game.ToResponse());
    }
}