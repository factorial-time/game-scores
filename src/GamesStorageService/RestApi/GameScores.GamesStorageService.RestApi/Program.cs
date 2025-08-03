using GameScores.GamesStorageService.Cache.Contracts;
using GameScores.GamesStorageService.Cache.Redis;
using GameScores.GamesStorageService.Storage.Contracts;
using GameScores.GamesStorageService.Storage.PostgreSql;
using GameScores.GamesStorageService.UseCases;
using GameScores.GamesStorageService.UseCases.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddScoped<IGetGameUseCase, GetGameUseCase>()
    .AddScoped<IFetchGamesBySportTypeUseCase, FetchGamesBySportTypeUseCase>()
    .AddScoped<IFetchGamesByCompetitionUseCase, FetchGamesByCompetitionUseCase>()
    .AddScoped<IGamesStorage, PostgreSqlGamesStorage>()
    .AddSingleton<IGamesCache, RedisGamesCache>()
    .AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();