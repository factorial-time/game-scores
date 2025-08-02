using System;
using GameScores.GamesStorageService.GameUpdatesLoader.DataProcessor;
using GameScores.GamesStorageService.GameUpdatesLoader.DataProcessor.Contracts;
using GameScores.GamesStorageService.GameUpdatesLoader.KeysProvider.DependencyInjection;
using GameScores.GamesStorageService.GameUpdatesLoader.Worker.BackgroundJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

IHost host = Host.CreateDefaultBuilder(args)
    .UseEnvironment(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production")
    .ConfigureAppConfiguration((_, configBuilder) =>
        {
            configBuilder.AddJsonFile("appsettings.json", optional: true);
            configBuilder.AddEnvironmentVariables();
            configBuilder.AddCommandLine(args);
        }
    )
    .ConfigureServices((ctx, services) =>
        {
            services
                .AddKeysProvider(ctx.Configuration)
                .AddSingleton<IDataProcessor, DataProcessor>()
                .AddHostedService<GameUpdatesHandlerJob>();
        }
    )
    .ConfigureLogging((hostingContext, logging) =>
        {
            logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
            logging.AddConsole();
        }
    )
    .Build();

await host.RunAsync();