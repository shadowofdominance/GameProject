using GameProject.Dtos;

namespace GameProject.Enpoints;

public static class GamesEnpoints
{
    const string getGameEndpointName = "GetGame";
    
    private static readonly List<GameDto> games =
    [
        new (1, "Mafia 1", "Open World", 25.00M, new DateOnly(2008, 6, 27)),
        new (2, "Mafia 2", "Open World", 25.00M, new DateOnly(2012, 6, 27)),
        new (3, "Mafia 3", "Open World", 25.00M, new DateOnly(2016, 6, 27))
    ];

    public static void MapGamesEnpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        
        // GET /games
        group.MapGet("/", () => games);

        // Get /games/1
        group.MapGet("/{id}", (int id) =>
            {
                var game = games.Find(game => game.Id == id);
                return game is null ? Results.NotFound() : Results.Ok(game);
            })
            .WithName(getGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
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
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex((game => game.Id == id));
            if (index == -1)
            {
                return Results.NotFound();
            }
            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate);
    
            return Results.NoContent();
        });

        // DELETE /games/1
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
    
            return Results.NoContent();
        });

    }
}