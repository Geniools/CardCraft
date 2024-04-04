namespace CardCraftShared.Cards.Spells;

public class SevenSevenThreeSpell : BaseSpell
{
    public SevenSevenThreeSpell() : base(
        10,
        "773",
        "Secret spell that deals 6 damage to the enemy hero",
        CardRarityEnum.LEGENDARY,
        "sevenseventhreespell.png"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        enemyPlayer.Hero.TakeDamage(8);
    }
}