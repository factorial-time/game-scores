using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GameScores.GamesCollector.Downloader.Contracts;

public interface IWebPageDownloader
{
    Task<Stream> DownloadAsync(Uri url, CancellationToken stoppingToken);
}