using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameScores.GamesCollector.UrlProcessor.Contracts;

public interface IUrlProcessor
{
    Task ProcessAsync(Uri url, CancellationToken stoppingToken);
}