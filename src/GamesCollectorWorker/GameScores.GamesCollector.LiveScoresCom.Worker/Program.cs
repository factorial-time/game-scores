using System;
using GameScores.GamesCollector.Downloader;
using GameScores.GamesCollector.Downloader.Contracts;
using GameScores.GamesCollector.Notifier.ApacheKafka;
using GameScores.GamesCollector.Notifier.Contracts;
using GameScores.GamesCollector.Parser.Contracts;
using GameScores.GamesCollector.Parser.LiveScoresCom;
using GameScores.GamesCollector.SeedUrlProvider;
using GameScores.GamesCollector.SeedUrlProvider.Contracts;
using GameScores.GamesCollector.ServiceDiscovery.Redis;
using GameScores.GamesCollector.UrlProcessor;
using GameScores.GamesCollector.UrlProcessor.Contracts;
using GameScores.GamesCollector.Worker.BackgroundJobs;
using GameScores.GamesCollector.Worker.DependencyInjection;
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
                .AddDataCollector(ctx.Configuration)
                .AddServiceDiscovery(ctx.Configuration)
                .AddSingleton<ISeedUrlProvider, MockSeedUrlProvider>()
                .AddSingleton<IUrlProcessor, UrlProcessor>()
                .AddSingleton<IWebPageDownloader, WebPageDownloader>()
                .AddSingleton<IContentParser, ContentParser>()
                .AddSingleton<INotifier, Notifier>()
                .AddHostedService<SchedulerJob>();
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