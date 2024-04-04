namespace CardCraftShared.Cards.Spells;

public class TatedSpell : BaseSpell
{
    public TatedSpell() : base
    (
        9,
        "Tated",
        "You have been 'Tated'! Don't leave your laptop open!",
        CardRarityEnum.EPIC,
        "tatedspell.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        throw new NotImplementedException();
    }
}