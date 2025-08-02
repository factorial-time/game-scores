using GameScores.Keys.Dto;

namespace GameScores.Keys.UseCases.Contracts;

public interface IObtainKeyUseCase
{
    Task<string> ObtainKeyAsync(KeyData data, CancellationToken stoppingToken);
}