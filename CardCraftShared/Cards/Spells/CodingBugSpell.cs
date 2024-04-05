namespace CardCraftShared.Cards.Spells;

public class CodingBugSpell : BaseSpell
{
    public CodingBugSpell(): base (
        1,
        "Bug",
        "Deal 1 damage to enemy Hero",
        CardRarityEnum.COMMON,
        "codingbugspell.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        enemyPlayer.Hero.TakeDamage(1);
    }
}