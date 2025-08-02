using System;
using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesStorageService.Dto;
using GameScores.GamesStorageService.GameUpdatesLoader.DataProcessor.Contracts;
using Microsoft.Extensions.Hosting;

namespace GameScores.GamesStorageService.GameUpdatesLoader.Worker.BackgroundJobs;

public class GameUpdatesHandlerJob : BackgroundService
{
    private readonly IDataProcessor _processor;

    public GameUpdatesHandlerJob(IDataProcessor processor)
    {
        _processor = processor;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var game = new Game
            {
                SportType = "Football",
                CompetitionName = " Premier League",
                Teams = ["Liverpool", "AFC Bournemouth"],
                EventDate = DateTime.UtcNow
            };
            await _processor.ProcessGameAsync(game, stoppingToken);
            
            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }
}