using CardCraftShared;

namespace CardCraftServer.Model;

public class OnlineGameManagerDatabase
{
    private readonly List<OnlineGameManager> _onlineGames;

    public OnlineGameManagerDatabase()
    {
        this._onlineGames = new();
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
    }

    public void RemovePlayer(string connectionId)
    {
        this._onlineGames.ForEach(game => game.RemovePlayer(connectionId));
    }

    /// <summary>
    /// Adds a game to the database.
    /// </summary>
    /// <param name="game">The game to be added</param>
    /// <exception cref="GameAlreadyExistsException">If the game already exists</exception>
    public void AddGame(OnlineGameManager game)
    {
        // Check if the game already exists
        if (this._onlineGames.Contains(game))
        {
            throw new GameAlreadyExistsException("Game already exists!");
        }

        this._onlineGames.Add(game);
    }

    public OnlineGameManager? GetGame(string lobbyCode)
    {
        return this._onlineGames.Find(game => game.LobbyCode == lobbyCode);
    }
}
