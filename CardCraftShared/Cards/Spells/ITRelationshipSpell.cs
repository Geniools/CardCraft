namespace CardCraftShared.Cards.Spells;

public class ITRelationshipSpell : BaseSpell
{
    public ITRelationshipSpell(): base(
        1,
        "IT Relationship",
        "We love each other :) \n Gain 2 Health",
        CardRarityEnum.COMMON,
        "itrelationshipspell.jpg"
    )
    { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        player.GiveHealth(2);
    }
}