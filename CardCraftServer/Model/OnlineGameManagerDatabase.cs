using CardCraftShared;

namespace CardCraftServer.Model;

public class OnlineGameManagerDatabase
{
    private readonly ILogger<OnlineGameManagerDatabase> _logger;
    private readonly List<OnlineGameManager> _onlineGames;

    public OnlineGameManagerDatabase(ILogger<OnlineGameManagerDatabase> logger)
    {
        this._onlineGames = new();
        this._logger = logger;


        this._logger.LogInformation(
            "==================================\n" +
                   "OnlineGameManagerDatabase created!\n" + 
                   "==================================\n"
            );
    }
    
    /// <summary>
    /// Joins a game with the given lobbyCode. Adds the player to the game.
    /// If the game does not exist, creates a new game.
    /// </summary>
    /// <param name="lobbyCode">The lobby code of the game</param>
    /// <param name="player">The player that joins the game</param>
    public void JoinGame(string lobbyCode, Player player)
    {
        // Check if a game with such a lobbyCode already exists
        OnlineGameManager? game = this.GetGame(lobbyCode);
        if (game is null)
        {
            // If not, create a new game
            game = new OnlineGameManager(lobbyCode);
            this.AddGame(game);
        }

        // Add the player to the game
        game.AddPlayer(player);

        this.LogGamesAndPlayers();
    }

    public void RemovePlayer(string connectionId)
    {
        OnlineGameManager? gameToRemove = null;

        this._onlineGames.ForEach(game =>
        {
            game.RemovePlayer(connectionId);

            if (game.Players.Count == 0)
            {
                gameToRemove = game;
            }
        });

        // Remove the game if there are no players left
        if (gameToRemove is not null)
        {
            this.RemoveGame(gameToRemove);
        }

        this.LogGamesAndPlayers();
    }

    /// <summary>
    /// Adds a game to the database.
    /// </summary>
    /// <param name="game">The game to be added</param>
    /// <exception cref="GameAlreadyExistsException">If the game already exists</exception>
    private void AddGame(OnlineGameManager game)
    {
        // Check if the game already exists
        if (this._onlineGames.Contains(game))
        {
            throw new GameAlreadyExistsException("Game already exists!");
        }

        this._onlineGames.Add(game);
    }

    private void RemoveGame(OnlineGameManager game)
    {
        this._onlineGames.Remove(game);
    }

    public OnlineGameManager? GetGame(string lobbyCode)
    {
        return this._onlineGames.Find(game => game.LobbyCode == lobbyCode);
    }

    public Player? GetOtherPlayerFromGame(string lobbyCode, string connectionId)
    {
        OnlineGameManager? game = this.GetGame(lobbyCode);

        if (game is null)
        {
            return null;
        }

        return game.Players.Find(player => player.ConnectionId != connectionId);
    }


    /// <summary>
    /// A helper method that logs all games and players in the database.
    /// </summary>
    private void LogGamesAndPlayers()
    {
        string message = "==================================\n" +
                         "OnlineGameManagerDatabase games:\n";

        this._onlineGames.ForEach(game =>
        {
            message += $"\nGame Lobby Code: {game.LobbyCode}\n";
            game.Players.ForEach(player =>
            {
                message += $"Player: {player.Name} | {player.ConnectionId}\n";
            });
        });
        message += "==================================\n";

        this._logger.LogInformation(message);
    }
}
