namespace CardCraftShared;

public interface IBaseCard
{
    public int ManaCost { get; set; }
    public string Image { get; init; }

    public CardRarityEnum Rarity { get; set; }

    public string Name { get; init; }

    public string Description { get; init; }
}

