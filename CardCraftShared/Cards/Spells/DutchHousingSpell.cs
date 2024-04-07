namespace CardCraftShared.Cards.Spells;

public class DutchHousingSpell : BaseSpell
{
    public DutchHousingSpell(): base(
        3,
        "Dutch housing",
        "Well, good luck finding housing.\n Deal 3 damage to enemy Hero. If enemy has played 3 or more minions, deal 5 instead.",
        CardRarityEnum.COMMON,
        "rentedroomspell.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        if (board.EnemySide.Count >= 3)
        {
            enemyPlayer.Hero.TakeDamage(5);
        }
        else
        {
            enemyPlayer.Hero.TakeDamage(3);
        }
    }
}