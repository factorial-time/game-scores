using GameScores.GamesCollector.Downloader.Contracts;

namespace GameScores.GamesCollector.Downloader;

public class WebPageDownloader : IWebPageDownloader
{
    public Task<Stream> DownloadAsync(Uri url, CancellationToken stoppingToken)
    {
        return Task.FromResult<Stream>(new MemoryStream());
    }
}