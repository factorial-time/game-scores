using System.Threading;
using System.Threading.Tasks;
using GameScores.GamesCollector.Dto;

namespace GameScores.GamesCollector.Notifier.Contracts;

public interface INotifier
{
    Task NotifyAsync(Game game, CancellationToken stoppingToken);
}