namespace CardCraftShared.Cards.Spells;

public class WomanCard : BaseMinion
{
    public WomanCard() : base(
        7,
            7,
        7,
        "A Woman",
        "Enemy hero cannot play any card this round",
        CardRarityEnum.EPIC,
        "womancard.png"
    ) { }
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
    }

}