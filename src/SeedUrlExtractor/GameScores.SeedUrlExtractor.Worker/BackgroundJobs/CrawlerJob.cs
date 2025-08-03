using System;
using System.Threading;
using System.Threading.Tasks;
using GameScores.SeedUrlExtractor.UseCases.Contracts;
using GameScores.SeedUrlExtractor.Worker.Config;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GameScores.SeedUrlExtractor.Worker.BackgroundJobs;

public sealed class CrawlerJob : BackgroundService
{
    private readonly IUrlExtractorUseCase _useCase;
    
    private readonly CrawlerConfig _config;

    private readonly ILogger _logger;

    public CrawlerJob(IUrlExtractorUseCase useCase, IOptions<CrawlerConfig> config, ILogger<CrawlerJob> logger)
    {
        _useCase = useCase;
        _config = config.Value;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _useCase.ExtractUrlAsync(_config.TargetUrl, stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Error occured");
            }
            await Task.Delay(_config.RefreshInterval, stoppingToken);
        }
    }
}