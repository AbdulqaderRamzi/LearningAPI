using GameStore.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

GameEndpoints.MapGamesEndpoints(app);

app.Run();
