namespace CardCraftShared.Cards.Spells;

public class NiceArgumentSpell : BaseSpell
{
    public NiceArgumentSpell() : base(
        8,
        "Nice Argument",
        "However... \n Deal 5 Damage to all enemy minions",
        CardRarityEnum.EPIC,
        "goodargumentspell2.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        while (board.EnemySide.Count > 0)
        {
            board.EnemySide[0].TakeDamage(5);
        }
    }
}