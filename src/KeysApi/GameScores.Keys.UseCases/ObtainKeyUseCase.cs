using System;
using System.Threading;
using System.Threading.Tasks;
using GameScores.Keys.Dto;
using GameScores.Keys.Storage.Contracts;
using GameScores.Keys.UseCases.Contracts;

namespace GameScores.Keys.UseCases;

public class ObtainKeyUseCase : IObtainKeyUseCase
{
    private readonly IStorage _storage;

    public ObtainKeyUseCase(IStorage storage)
    {
        _storage = storage;
    }

    public Task<string> ObtainKeyAsync(KeyData data, CancellationToken stoppingToken) =>
        _storage.ObtainKeyAsync(Guid.NewGuid().ToString(), data, stoppingToken);
}