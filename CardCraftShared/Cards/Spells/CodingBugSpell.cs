namespace CardCraftShared.Cards.Spells;

public class CodingBugSpell : BaseSpell
{
    public CodingBugSpell(): base (
        1,
        "Bug",
        "That's a bug!",
        CardRarityEnum.COMMON,
        "codingbugspell.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        throw new NotImplementedException();
    }
}