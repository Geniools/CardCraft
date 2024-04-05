namespace CardCraftShared.Cards.Spells;

public class NiceArgumentSpell : BaseSpell
{
    public NiceArgumentSpell() : base(
        8,
        "Nice Argument",
        "Remove 1 enemy minion Card from the Board",
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