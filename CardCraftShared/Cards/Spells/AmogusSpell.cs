namespace CardCraftShared.Cards.Spells;

public class AmogusSpell : BaseSpell
{
    public AmogusSpell() : base
        (
            5,
            "Amogus", 
            "Get control over an enemy minion",
            CardRarityEnum.RARE, 
            "amogusspell.jpg"
        ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        throw new NotImplementedException();
    }
}