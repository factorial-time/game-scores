using GameScores.GamesStorageService.UseCases;
using GameScores.GamesStorageService.UseCases.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddScoped<IGetGameUseCase, GetGameUseCase>()
    .AddScoped<IFetchGamesUseCase, FetchGamesUseCase>()
    .AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();