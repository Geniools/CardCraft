namespace CardCraftShared;

public abstract class BaseHero
{
    private int _health;
    private string _color;
    private string _image;
    private string _name;

    public BaseHero(int health, string color, string image, string name)
    {
        this._health = health;
        this._color = color;
        this._image = image;
        this._name = name;
    }

    public void TriggerHeroPower()
    {
        throw new NotImplementedException();
    }
}
