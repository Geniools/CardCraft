using CardCraftShared;

namespace CardCraftClient.Core.Interfaces;

public interface ISignalRObserver
{
    public Task OnGameJoined(Player player, Player otherPlayer);
    public Task OnGameLeft(Player player);
    public Task OnGameStarted();
    public Task OnGameEnded();
}