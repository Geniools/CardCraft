namespace CardCraftShared.Cards.Spells;

public class DutchHousingSpell : BaseSpell
{
    public DutchHousingSpell(): base(
        3,
        "Dutch housing",
        "No sleep for you, hero takes 2 damage",
        CardRarityEnum.COMMON,
        "rentedroomspell.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        throw new NotImplementedException();
    }
}