using GameProject.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games =
[
    new (1, "Mafia 1", "Open World", 25.00M, new DateOnly(2008, 6, 27)),
    new (2, "Mafia 2", "Open World", 25.00M, new DateOnly(2012, 6, 27)),
    new (3, "Mafia 3", "Open World", 25.00M, new DateOnly(2016, 6, 27))
];

// GET /games
app.MapGet("/games", () => games);

// Get /games/1
app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id));
app.Run();