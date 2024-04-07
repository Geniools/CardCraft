namespace CardCraftShared.Cards.Spells;

public class SevenSevenThreeSpell : BaseSpell
{
    public SevenSevenThreeSpell() : base(
        10,
        "773",
        "Deal 10 damage to enemy Hero",
        CardRarityEnum.LEGENDARY,
        "sevenseventhreespell.png"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        enemyPlayer.Hero.TakeDamage(10);
    }
}