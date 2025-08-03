using System;
using GameScores.SeedUrlExtractor.DataStorage.Contracts;
using GameScores.SeedUrlExtractor.DataStorage.Redis;
using GameScores.SeedUrlExtractor.UrlExtractor.Contracts;
using GameScores.SeedUrlExtractor.UrlExtractor.LiveScoresCom;
using GameScores.SeedUrlExtractor.UseCases;
using GameScores.SeedUrlExtractor.UseCases.Contracts;
using GameScores.SeedUrlExtractor.Worker.BackgroundJobs;
using GameScores.SeedUrlExtractor.Worker.DependencyInjection;
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
                .AddCrawler(ctx.Configuration.GetRequiredSection("Crawler"))
                .AddSingleton<IUrlExtractorUseCase, UrlExtractorUseCase>()
                .AddSingleton<IUrlExtractor, UrlExtractor>()
                .AddSingleton<ISeedUrlStorage, SeedUrlStorage>()
                .AddHostedService<CrawlerJob>();
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