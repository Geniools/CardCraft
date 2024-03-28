namespace CardCraftShared;

public abstract class BaseHero
{
    protected const int DefaultHealth = 30;
    public ColorEnum Color { get; init; }
    public string Image { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }

    public int Health { get; protected set; }

    protected BaseHero(int health, ColorEnum color, string image, string name, string description)
    {
        this.Health = health;
        this.Color = color;
        this.Image = image;
        this.Name = name;
        this.Description = description;
    }

    public void TriggerHeroPower()
    {
        throw new NotImplementedException();
    }
}
