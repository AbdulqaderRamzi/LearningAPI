namespace GameStore.Endpoints;
using GameStore.Dtos;

public static class GameEndpoints
{
    const string GET_GAME_ENDPOINT = "GetGame";

    private static readonly List<GameDto> games = [
        new (
                1,
                "Figter II",
                "Figter",
                19.99M,
            new DateOnly(1992, 7,15)),
            new (
                2,
                "Race II",
                "Race",
                19.99M,
                new DateOnly(2002, 6,5))
     ];

    public static WebApplication MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        // GET /games
        group.MapGet("/", () => games);

        // GET /games/1
        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GET_GAME_ENDPOINT);

        // POST /games  
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                        games.Count + 1,
                        newGame.Name,
                        newGame.Genre,
                        newGame.Price,
                        newGame.ReleasedDate
                    );
            games.Add(game);
            return Results.CreatedAtRoute(GET_GAME_ENDPOINT, new { id = game.Id }, game);
        });


        // PUT 
        group.MapPut("/{id}", (int id, UpdateGameDto modifiedGame) =>
        {
            int oldGameIdx = games.FindIndex(game => game.Id == id);
            if (oldGameIdx == -1)
                return Results.NotFound();
            games[oldGameIdx] = new GameDto(
                        id,
                        modifiedGame.Name,
                        modifiedGame.Genre,
                        modifiedGame.Price,
                        modifiedGame.ReleasedDate
                    );
            return Results.NoContent();
        });

        // DELETE games/1
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });
        return app;
    }
}
