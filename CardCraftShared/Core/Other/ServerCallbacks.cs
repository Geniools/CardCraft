namespace CardCraftShared.Core.Other;

public static class ServerCallbacks
{
    // Callbacks sent to the server
    public const string JoinGame = "JoinGame";
    public const string LeaveGame = "LeaveGame";

    // Callbacks coming from the server
    public const string GameLeft = "GameLeft";
    public const string GameJoined = "GameJoined";
    public const string GameStarted = "GameStarted";
    public const string ErrorMessage = "ErrorMessage";
}
