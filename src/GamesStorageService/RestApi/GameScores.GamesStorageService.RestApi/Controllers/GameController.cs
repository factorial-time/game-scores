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
    private readonly IGetGameUseCase _getGameUseCase;
    
    private readonly IFetchGamesBySportTypeUseCase _fetchGamesBySportTypeUseCase;
    
    private readonly IFetchGamesByCompetitionUseCase _fetchGamesByCompetitionUseCase;
    
    public GameController(
        IGetGameUseCase getGameUseCase,
        IFetchGamesBySportTypeUseCase fetchGamesBySportTypeUseCase,
        IFetchGamesByCompetitionUseCase fetchGamesByCompetitionUseCase)
    {
        _getGameUseCase = getGameUseCase;
        _fetchGamesBySportTypeUseCase = fetchGamesBySportTypeUseCase;
        _fetchGamesByCompetitionUseCase = fetchGamesByCompetitionUseCase;
    }
    
    [HttpGet("{gameId:guid}")]
    public async Task<ActionResult<GameResponse>> GetById(Guid gameId, CancellationToken stoppingToken)
    {
        Dto.GameInfo game = await _getGameUseCase.Handle(gameId, stoppingToken);
        
        return Ok(game.ToResponse());
    }
    
    [HttpGet("sport/{sportType}")]
    public async Task<ActionResult<PageResponse<GameResponse>>> GetPageBySportType(
        [FromRoute] string sportType,
        [FromQuery] GetPageRequest request,
        CancellationToken stoppingToken)
    {
        Dto.DataPage<Dto.GameInfo> games = await _fetchGamesBySportTypeUseCase.HandleAsync(
            sportType,
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
    
    [HttpGet("competition/{competition}")]
    public async Task<ActionResult<PageResponse<GameResponse>>> GetPageByCompetition(
        [FromRoute] string competition,
        [FromQuery] GetPageRequest request,
        CancellationToken stoppingToken)
    {
        Dto.DataPage<Dto.GameInfo> games = await _fetchGamesByCompetitionUseCase.HandleAsync(
            competition,
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
}