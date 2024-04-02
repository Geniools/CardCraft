namespace CardCraftShared.Cards.Minions;

public class ChrisCard : BaseMinion
{
    public ChrisCard(): base(
        5,
        5,
        5,
        "Chris",
        "Removes one card from the enemy board that is less than 5 mana",
        CardRarityEnum.LEGENDARY,
        "chriscard.jpg"
    ) { }
}