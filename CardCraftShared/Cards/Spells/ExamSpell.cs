namespace CardCraftShared.Cards.Spells;

public class ExamSpell : BaseSpell
{
    public ExamSpell() : base(
        3,
        "Exam",
        "Ay blayt",
        CardRarityEnum.COMMON,
        "examspell.jpeg"
    )
    { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        throw new NotImplementedException();
    }
}