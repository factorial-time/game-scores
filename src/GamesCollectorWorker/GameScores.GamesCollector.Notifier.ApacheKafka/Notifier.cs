using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesCollector.Dto;
using GameScores.GamesCollector.Notifier.Contracts;

namespace GameScores.GamesCollector.Notifier.ApacheKafka;

public class Notifier : INotifier
{
    public Task NotifyAsync(Game game, CancellationToken stoppingToken) => Task.CompletedTask;
}