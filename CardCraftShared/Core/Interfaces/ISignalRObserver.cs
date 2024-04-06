using CardCraftShared;

namespace CardCraftClient.Core.Interfaces;

public interface ISignalRObserver
{
    public Task OnGameJoined(Player player, Player? otherPlayer);
    public Task OnGameStarted();
    public Task OnGameEnded();
    public Task OnCardPlayed(IBaseCard card);
    public Task OnEnemyPlayerCardAmountUpdated(EnemyPlayerCardAmountUpdateMessage message);
    public Task OnEnemyPlayerHeroUpdated(EnemyPlayerHeroUpdateMessage message);
    public Task OnMinionUpdated(MinionCardUpdatedMessage message);
    public Task OnTurnStarted(bool isFirstTurn);
}