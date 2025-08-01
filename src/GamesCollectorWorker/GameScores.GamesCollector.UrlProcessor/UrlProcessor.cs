using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesCollector.Downloader.Contracts;
using GameScores.GamesCollector.Dto;
using GameScores.GamesCollector.Notifier.Contracts;
using GameScores.GamesCollector.Parser.Contracts;
using GameScores.GamesCollector.UrlProcessor.Contracts;

namespace GameScores.GamesCollector.UrlProcessor;

public class UrlProcessor : IUrlProcessor
{
    private readonly IWebPageDownloader _downloader;

    private readonly IContentParser _parser;
    
    private readonly INotifier _notifier;

    public UrlProcessor(IWebPageDownloader downloader, IContentParser parser, INotifier notifier)
    {
        _downloader = downloader;
        _parser = parser;
        _notifier = notifier;
    }
    
    public async Task ProcessAsync(Uri url, CancellationToken stoppingToken)
    {
        Stream content = await _downloader.DownloadAsync(url, stoppingToken);
        IEnumerable<Game> games = _parser.ExtractGames(content);
        foreach (Game game in games)
        {
            await _notifier.NotifyAsync(game, stoppingToken);
        }
    }
}