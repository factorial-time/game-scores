using System;

namespace GameScores.GamesCollector.Worker;

internal sealed class ServiceIdentifierProvider
{
    private static readonly Lazy<ServiceIdentifierProvider> _instance = new(() => new ServiceIdentifierProvider());
    
    public static ServiceIdentifierProvider Instance => _instance.Value;
    
    private readonly string _serviceInstanceId;
    
    public string ServiceInstanceId => _serviceInstanceId;

    private ServiceIdentifierProvider()
    {
        _serviceInstanceId = Guid.NewGuid().ToString().Substring(0, 5);
    }
}