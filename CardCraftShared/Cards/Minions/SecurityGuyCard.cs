namespace CardCraftShared.Cards.Minions;

public class SecurityGuyCard : BaseMinion
{
    public SecurityGuyCard() : base(
        4,
        6,
        6,
        "Security Guy",
        "You need to leave the room, the university is being closed. This card throws one enemy card back to the deck",
        CardRarityEnum.RARE,
        "securityguycard.jpg"
    ) { }
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
    }
}