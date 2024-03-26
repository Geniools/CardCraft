using CardCraftShared;

namespace CardCraftServer.Model;

public class OnlineGameManager
{
    private const int REQUIRED_PLAYERS = 2;
    private readonly List<Player> _players;
    public List<Player> Players => this._players;
    public string LobbyCode { get; init; }

    public OnlineGameManager(string lobbyCode)
    {
        this.LobbyCode = lobbyCode;
        this._players = new();
        this.GameStarted = false;
    }

    private bool GameStarted { get; set; }

    public void AddPlayer(Player player)
    {
        if (this._players.Count < REQUIRED_PLAYERS)
        {
            this._players.Add(player);
        }
        else
        {
            throw new MaxPlayersReachedException("Maximum number of players reached!");
        }
    }

    public void RemovePlayer(string connectionId)
    {
        this._players.RemoveAll(player => player.ConnectionId == connectionId);
    }

    public bool CanStartGame()
    {
        return this._players.Count == REQUIRED_PLAYERS;
    }

    public override bool Equals(object? obj)
    {
        if (obj is OnlineGameManager other)
        {
            return this.LobbyCode == other.LobbyCode;
        }

        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return this.LobbyCode.GetHashCode();
    }
}
