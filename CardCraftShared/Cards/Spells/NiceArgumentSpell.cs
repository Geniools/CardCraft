namespace CardCraftShared.Cards.Spells;

public class NiceArgumentSpell : BaseSpell
{
    public NiceArgumentSpell() : base(
        8,
        "Nice Argument...",
        "... However",
        CardRarityEnum.EPIC,
        "goodargumentspell2.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        if (board.EnemySide.Count > 0)
        {
            board.EnemySide.RemoveAt(0);
        }
    }
}