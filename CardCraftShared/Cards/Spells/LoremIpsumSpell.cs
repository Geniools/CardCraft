namespace CardCraftShared.Cards.Spells;

public class LoremIpsumSpell : BaseSpell
{
    public LoremIpsumSpell() : base(
        0,
        "Lorem Ipsum",
        "Here's some data to help you debug. \n Gain 1 mana",
        CardRarityEnum.COMMON,
        "loremipsumspell.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        player.GiveMana(1);
    }
}