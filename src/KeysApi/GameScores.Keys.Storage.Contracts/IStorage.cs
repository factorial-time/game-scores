using System.Threading;
using System.Threading.Tasks;
using GameScores.Keys.Dto;

namespace GameScores.Keys.Storage.Contracts;

public interface IStorage
{
    Task<string> ObtainKeyAsync(KeyData data, CancellationToken stoppingToken);
}