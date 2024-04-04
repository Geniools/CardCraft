namespace CardCraftShared.Core.Other;

public class JunkCard : IBaseCard
{
    public int ManaCost { get; set; }
    public string Image { get; init; }
    public CardRarityEnum Rarity { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
}