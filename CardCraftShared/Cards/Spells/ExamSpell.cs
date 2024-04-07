namespace CardCraftShared.Cards.Spells;

public class ExamSpell : BaseSpell
{
    public ExamSpell() : base(
        4,
        "Exam",
        "HAHAHAHAHAHA\n Good luck passing Design Patterns :) \n Deal 2 Damage to all enemy Minions",
        CardRarityEnum.COMMON,
        "examspell.jpeg"
    )
    { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        foreach (BaseMinion minion in board.EnemySide)
        {
            minion.TakeDamage(2);
        }
    }
}