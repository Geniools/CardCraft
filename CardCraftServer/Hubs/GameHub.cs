using CardCraftServer.Model;
using Microsoft.AspNetCore.SignalR;
using CardCraftShared;
using CardCraftShared.Core.Other;

namespace CardCraftServer.Hubs;

public class GameHub : Hub
{
    private readonly OnlineGameManagerDatabase _onlineGameManagerDatabase;

    public GameHub(OnlineGameManagerDatabase onlineGameManagerDatabase)
    {
        this._onlineGameManagerDatabase = onlineGameManagerDatabase;
    }

    public async Task JoinGame(Player player, string lobbyCode)
    {
        try
        {
            player.ConnectionId = this.Context.ConnectionId;
            // Joins the game with the given lobby code. Also performs the necessary checks.
            this._onlineGameManagerDatabase.JoinGame(lobbyCode, player);
            Player? otherPlayer = this._onlineGameManagerDatabase.GetOtherPlayerFromGame(lobbyCode, player.ConnectionId);

            // Adds the player to a group with the lobby code for easier communication
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, lobbyCode);

            await this.Clients.Group(lobbyCode).SendAsync(ServerCallbacks.GameJoined, lobbyCode, player, otherPlayer);

            // Check if the game can be started
            if (this._onlineGameManagerDatabase.GetGame(lobbyCode)!.CanStartGame())
            {
                await this.Clients.Group(lobbyCode).SendAsync(ServerCallbacks.GameStarted, lobbyCode);
            }
        }
        catch (Exception e)
        {
            this._onlineGameManagerDatabase.LogString(e.Message);
            // await this.Clients.Client(this.Context.ConnectionId).SendAsync(ServerCallbacks.ErrorMessage, e.Message);
            await this.Clients.Caller.SendAsync(ServerCallbacks.ErrorMessage, e.Message);
        }
    }

    public async Task LeaveGame()
    {
        this._onlineGameManagerDatabase.LogString("Leave Game triggered: " + this.Context.ConnectionId);

        // Remove the player from the game
        this._onlineGameManagerDatabase.RemovePlayer(this.Context.ConnectionId);

        // Remove the player from the group
        string lobbyCode = this._onlineGameManagerDatabase.GetLobbyCodeFromConnectionId(this.Context.ConnectionId);
        await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, lobbyCode);
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        // Remove the player from the game
        string lobbyCode = this._onlineGameManagerDatabase.GetLobbyCodeFromConnectionId(this.Context.ConnectionId);
        Player? player = this._onlineGameManagerDatabase.GetPlayerFromConnectionId(this.Context.ConnectionId);

        try
        {
            if (player is not null)
            {
                this._onlineGameManagerDatabase.LogString($"Player {player.Name} left the game with lobby code {lobbyCode}!");
            }

            this.LeaveGame();

            this.Clients.Group(lobbyCode).SendAsync(ServerCallbacks.GameLeft, player);
            this.Clients.Group(lobbyCode).SendAsync(ServerCallbacks.ErrorMessage, $"{player.Name} left the game!");
        }
        catch (Exception e)
        {
            this._onlineGameManagerDatabase.LogString(e.Message);
        }

        return base.OnDisconnectedAsync(exception);
    }
}
