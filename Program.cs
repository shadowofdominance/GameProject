using GameProject.Data;
using GameProject.Enpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();
builder.AddGameStoreDb();

var app = builder.Build();

app.MapGamesEnpoints();

app.MigrateDb();

app.Run();
