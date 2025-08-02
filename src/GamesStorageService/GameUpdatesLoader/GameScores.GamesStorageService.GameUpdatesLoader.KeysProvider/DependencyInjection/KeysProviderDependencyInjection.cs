using System.Net.Http;
using GameScores.GamesStorageService.GameUpdatesLoader.KeysProvider.Config;
using GameScores.GamesStorageService.GameUpdatesLoader.KeysProvider.Contracts;
using GameScores.Keys.Api.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GameScores.GamesStorageService.GameUpdatesLoader.KeysProvider.DependencyInjection;

public static class KeysProviderDependencyInjection
{
    public static IServiceCollection AddKeysProvider(this IServiceCollection services, IConfiguration config)
    {
        IConfigurationSection endpointConfigSection = config.GetRequiredSection("KeysApi");
        var keysApiConfig = new KeysApiConfig();
        endpointConfigSection.Bind(keysApiConfig);
        services.Configure<KeysApiConfig>(endpointConfigSection);
        
        services.AddGrpcClient<KeysService.KeysServiceClient>((p, o) =>
                {
                    o.Address = p.GetRequiredService<IOptions<KeysApiConfig>>().Value.Endpoint;
					
                    // if (!string.IsNullOrEmpty(o.Address.AbsolutePath))
                    // {
                    //     var handler = new SubdirectoryHandler(new HttpClientHandler(), o.Address.AbsolutePath);
                    //     o.ChannelOptionsActions.Add(o => o.HttpHandler = handler);
                    // }
                }
            )
            //	Uncomment only for local development.
            .ConfigurePrimaryHttpMessageHandler(() =>
            	{
            		var handler = new HttpClientHandler();
            		handler.ServerCertificateCustomValidationCallback = 
            			HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            
            		return handler;
            	}
            );

        services.AddSingleton<IKeysProvider, KeysProvider>();
        
        return services;
    }
}