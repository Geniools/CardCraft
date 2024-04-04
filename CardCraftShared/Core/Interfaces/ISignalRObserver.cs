using CardCraftShared;

namespace CardCraftClient.Core.Interfaces;

public interface ISignalRObserver
{
    public Task OnGameJoined(Player player, Player? otherPlayer);
    public Task OnGameStarted();
    public Task OnGameEnded();
    public Task OnCardPlayed(IBaseCard card);
    public Task OnEnemyPlayerUpdated(EnemyPlayerUpdateMessage message);
    public Task OnTurnStarted(bool isFirstTurn);
}