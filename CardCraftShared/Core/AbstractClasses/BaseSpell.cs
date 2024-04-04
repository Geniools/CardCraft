namespace CardCraftShared;

public abstract class BaseSpell : IBaseCard
{
    public int ManaCost { get; set; }
    public CardRarityEnum Rarity { get; init; }
    public string Name { get; init; }
    public string Description { get; set; }
    public string Image { get; init; }

    // Color and TextColor properties are used to determine the color of the card based on its rarity
    public string Color => Rarity switch
    {
        CardRarityEnum.COMMON => "#FFFFFF",
        CardRarityEnum.RARE => "#0070dd",
        CardRarityEnum.EPIC => "#a335ee",
        CardRarityEnum.LEGENDARY => "#ff8000",
        _ => "#FFFFFF"
    };
    public string TextColor => Rarity switch
    {
        CardRarityEnum.COMMON => "#000000",
        CardRarityEnum.RARE => "#FFFFFF",
        CardRarityEnum.EPIC => "#FFFFFF",
        CardRarityEnum.LEGENDARY => "#FFFFFF",
        _ => "#000000"
    };


    protected BaseSpell(int manaCost, string name, string description, CardRarityEnum rarity,string image)
    {
        this.ManaCost = manaCost;
        this.Name = name;
        this.Description = description;
        this.Rarity = rarity;
        this.Image = image;
    }
    public abstract void TriggerEffect(Player player, Player enemyPlayer, Board board);
}