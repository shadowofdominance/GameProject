using GameProject.Data;
using GameProject.Dtos;
using GameProject.Enpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();

// Connection String before the app object creation
var connectionString = "Data Source=GameStore.db";
builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();

app.MapGamesEnpoints();

app.Run();