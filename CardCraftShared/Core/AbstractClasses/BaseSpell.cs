namespace CardCraftShared;

public abstract class BaseSpell : IBaseCard
{
    public int ManaCost { get; set; }
    public CardRarityEnum Rarity { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string Image { get; set; }

    protected BaseSpell(int manaCost, string name, string description, CardRarityEnum rarity,string image)
    {
        this.ManaCost = manaCost;
        this.Name = name;
        this.Description = description;
        this.Rarity = rarity;
        this.Image = image;
    }
    public abstract void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero);
}
