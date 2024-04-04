namespace CardCraftShared.Core.Other;

public static class ServerCallbacks
{
    // Callbacks sent to the server
    public const string JoinGame = "JoinGame";
    public const string LeaveGame = "LeaveGame";
    public const string PlayCard = "PlayCard";
    public const string UpdateEnemyPlayer = "UpdateEnemyPlayer";

    // Callbacks coming from the server
    public const string GameJoined = "GameJoined";
    public const string GameStarted = "GameStarted";
    public const string GameEnded = "GameEnded";
    public const string ErrorMessage = "ErrorMessage";
    public const string CardPlayed = "CardPlayed";
    public const string EnemyPlayerUpdated = "EnemyPlayerUpdated";
}
