namespace CardCraftShared.Cards.Spells;

public class AlcoholismSpell : BaseSpell
{
    public AlcoholismSpell() : base(
        6,
        "Alcoholism",
        "Casual Tuesday, gain 2 health",
        CardRarityEnum.RARE,
        "alcoholismspell.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        throw new NotImplementedException();
    }
}