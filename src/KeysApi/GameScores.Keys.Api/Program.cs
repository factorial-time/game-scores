using GameScores.Keys.Api.Controllers;
using GameScores.Keys.Storage.Contracts;
using GameScores.Keys.Storage.Redis;
using GameScores.Keys.UseCases;
using GameScores.Keys.UseCases.Contracts;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<IConnectionMultiplexer>(_ =>
        ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("KeysStorage"))
    )
    .AddSingleton<IStorage, Storage>()
    .AddSingleton<IObtainKeyUseCase, ObtainKeyUseCase>()
    .AddGrpc();

var app = builder.Build();

app.MapGrpcService<KeysController>();

await app.RunAsync();