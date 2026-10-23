using GameProject.Dtos;
using GameProject.Enpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();

var app = builder.Build();

app.MapGamesEnpoints();

app.Run();