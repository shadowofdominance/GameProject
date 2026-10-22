using GameProject.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
const string getGameEndpointName = "GetGame";

List<GameDto> games =
[
    new (1, "Mafia 1", "Open World", 25.00M, new DateOnly(2008, 6, 27)),
    new (2, "Mafia 2", "Open World", 25.00M, new DateOnly(2012, 6, 27)),
    new (3, "Mafia 3", "Open World", 25.00M, new DateOnly(2016, 6, 27))
];

// GET /games
app.MapGet("/games", () => games);

// Get /games/1
app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(getGameEndpointName);

// POST /games
app.MapPost("/games", (CreateGameDto newGame) =>
    {
        GameDto game = new(
        games.Count +1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
            );
        games.Add(game);
        
        return Results.CreatedAtRoute(getGameEndpointName, new {id  = game.Id}, game);
    }
);

// PUT /games/1
app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) =>
{
    var index = games.FindIndex((game => game.Id == id));
    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate);
    
    return Results.NoContent();
});

// DELETE /games/1
app.MapDelete("games/{id}", (int id) =>
{
    games.RemoveAll(game => game.Id == id);
    
    return Results.NoContent();
});

app.Run();