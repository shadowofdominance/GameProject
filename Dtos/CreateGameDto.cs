namespace GameProject.Dtos;

public abstract record CreateGameDto(
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
    );