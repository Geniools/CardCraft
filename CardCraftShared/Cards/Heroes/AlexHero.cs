namespace CardCraftShared.Cards.Heroes;

internal class AlexHero : BaseHero
{
    private const string Image = "alexhero.jpeg";
    private const string Name = "Alex";
    private const ColorEnum Color = ColorEnum.Blue;
    private const string Description = "The hero Alex is a strong and powerful hero that can take a lot of damage.";

    public AlexHero() : base(DefaultHealth, Color, Image, Name, Description)
    {
    }

    public AlexHero(int health) : base(health, Color, Image, Name, Description)
    {
    }
}
