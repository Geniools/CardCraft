namespace CardCraftShared.Cards.Spells;

public class ExamSpell : BaseSpell
{
    public ExamSpell() : base(
        3,
        "Exam",
        "Remove 2 Mana points from enemy Hero",
        CardRarityEnum.COMMON,
        "examspell.jpeg"
    )
    { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        enemyPlayer.RemoveMana(2);
    }
}