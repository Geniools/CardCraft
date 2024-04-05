namespace CardCraftShared.Cards.Minions;

public class TeacherCard : BaseMinion
{
    public TeacherCard() : base(
        8,
        8,
        8,
        "Teacher",
        "Introduction to pointers in C++ 10h version",
        CardRarityEnum.EPIC,
        "teachercard.jpg"
    ) { }
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
    }
}