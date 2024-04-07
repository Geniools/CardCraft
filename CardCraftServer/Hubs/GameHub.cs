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

        this._onlineGameManagerDatabase.OnGameEnd += (lobbyCode) =>
        {
            _ = this.EndGame(lobbyCode);
        };
    }

    public async Task EndTurn()
    {
        try
        {
            string lobbyCode = this._onlineGameManagerDatabase.GetLobbyCodeFromConnectionId(this.Context.ConnectionId);

            await this.Clients.OthersInGroup(lobbyCode).SendAsync(ServerCallbacks.StartTurn, false);
        }
        catch (Exception e)
        {
            await this.Clients.Caller.SendAsync(ServerCallbacks.ErrorMessage, e.Message);
        }
    }

    public async Task PickRandomPlayerToStartTurn()
    {
        try
        {
            Player player = this._onlineGameManagerDatabase.GetRandomPlayerFromConnectionId(this.Context.ConnectionId);

            await this.Clients.Client(player.ConnectionId).SendAsync(ServerCallbacks.StartTurn, true);
        }
        catch (GameAlreadyStarted)
        {
        }
        catch (Exception e)
        {
            await this.Clients.Caller.SendAsync(ServerCallbacks.ErrorMessage, e.Message);
        }
    }

    public async Task UpdateMinion(MinionCardUpdatedMessage message)
    {
        try
        {
            string lobbyCode = this._onlineGameManagerDatabase.GetLobbyCodeFromConnectionId(this.Context.ConnectionId);

            await this.Clients.OthersInGroup(lobbyCode).SendAsync(ServerCallbacks.MinionUpdated, message);
        }
        catch (Exception e)
        {
            await this.Clients.Caller.SendAsync(ServerCallbacks.ErrorMessage, e.Message);
        }
    }

    public async Task UpdateEnemyPlayerHero(EnemyPlayerHeroUpdateMessage message)
    {
        try
        {
            string lobbyCode = this._onlineGameManagerDatabase.GetLobbyCodeFromConnectionId(this.Context.ConnectionId);

            await this.Clients.OthersInGroup(lobbyCode).SendAsync(ServerCallbacks.EnemyPlayerHeroUpdated, message);
        }
        catch (Exception e)
        {
            await this.Clients.Caller.SendAsync(ServerCallbacks.ErrorMessage, e.Message);
        }
    }

    public async Task UpdateCardAmountEnemyPlayer(EnemyPlayerCardAmountUpdateMessage message)
    {
        try
        {
            string lobbyCode = this._onlineGameManagerDatabase.GetLobbyCodeFromConnectionId(this.Context.ConnectionId);

            await this.Clients.OthersInGroup(lobbyCode).SendAsync(ServerCallbacks.EnemyPlayerCardAmountUpdated, message);
        }
        catch (Exception e)
        {
            await this.Clients.Caller.SendAsync(ServerCallbacks.ErrorMessage, e.Message);
        }
    }

    public async Task PlayCard(CardMessage cardMessage)
    {
        try
        {
            string lobbyCode = this._onlineGameManagerDatabase.GetLobbyCodeFromConnectionId(this.Context.ConnectionId);

            await this.Clients.OthersInGroup(lobbyCode).SendAsync(ServerCallbacks.CardPlayed, cardMessage);
        }
        catch (Exception e)
        {
            await this.Clients.Caller.SendAsync(ServerCallbacks.ErrorMessage, e.Message);
        }
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

        // Remove the player from the group
        Player? player = this._onlineGameManagerDatabase.GetPlayerFromConnectionId(this.Context.ConnectionId);
        string? lobbyCode = this._onlineGameManagerDatabase.GetLobbyCodeFromConnectionId(this.Context.ConnectionId);

        // Remove the player from the game
        this._onlineGameManagerDatabase.RemovePlayer(this.Context.ConnectionId);

        if (player is null || lobbyCode is null)
        {
            return;
        }

        await this.Clients.GroupExcept(lobbyCode, this.Context.ConnectionId).SendAsync(ServerCallbacks.GameEnded);
        await this.Clients.GroupExcept(lobbyCode, this.Context.ConnectionId).SendAsync(ServerCallbacks.ErrorMessage, $"{player.Name} left the game!");

        await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, lobbyCode);

        if (player is not null)
        {
            this._onlineGameManagerDatabase.LogString($"Player {player.Name} left the game with lobby code {lobbyCode}!");
        }
    }

    public async Task EndGame(string lobbyCode)
    {
        await this.Clients.Group(lobbyCode).SendAsync(ServerCallbacks.GameEnded);
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        try
        {
            _ = LeaveGame();
        }
        catch (Exception e)
        {
            string message = $"=========Error on disconnect==========\n {e.Message} \n =================================";
            this._onlineGameManagerDatabase.LogString(message);
        }

        return base.OnDisconnectedAsync(exception);
    }
}
