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
            // Joins the game with the given lobby code. Also performs the necessary checks.
            this._onlineGameManagerDatabase.JoinGame(lobbyCode, player);

            // Adds the player to a group with the lobby code for easier communication
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, lobbyCode);

            await this.Clients.Group(lobbyCode).SendAsync(ServerCallbacks.GameJoined, lobbyCode, player);

            // Check if the game can be started
            if (this._onlineGameManagerDatabase.GetGame(lobbyCode)!.CanStartGame())
            {
                await this.Clients.Group(lobbyCode).SendAsync(ServerCallbacks.GameStarted, lobbyCode);
            }
        }
        catch (Exception e)
        {
            await this.Clients.Caller.SendAsync(ServerCallbacks.ErrorMessage, e.Message);
        }
        // catch (GameAlreadyExistsException e)
        // {
        //     await this.Clients.Caller.SendAsync(ServerCallbacks.ErrorMessage, e.Message);
        // }
        // catch (MaxPlayersReachedException e)
        // {
        //     await this.Clients.Caller.SendAsync(ServerCallbacks.ErrorMessage, e.Message);
        // }

    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        // Remove the player from the game
        this._onlineGameManagerDatabase.RemovePlayer(this.Context.ConnectionId);

        return base.OnDisconnectedAsync(exception);
    }
}
