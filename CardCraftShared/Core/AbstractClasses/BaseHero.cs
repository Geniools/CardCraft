using CardCraftShared.Core.Interfaces;
using CardCraftShared.Core.Other;

namespace CardCraftShared;

public abstract class BaseHero : IAttackable
{
    protected const int DefaultHealth = 30;
    // UI properties for the hero
    public string Color { get; init; }
    public string TextColor { get; init; }
    public string Image { get; init; }
    private string _name;
    public string Name {
        get => this._name;
        init
        {
            if (value.Length > 10)
            {
                throw new ArgumentException("Name is too long");
            }

            this._name = value;
        }
    }

    private string _description;
    public string Description
    {
        get => this._description;
        init
        {
            if (value.Length > 100)
            {
                throw new ArgumentException("Description is too long");
            }

            this._description = value;
        }
    }

    public int Health { get; set; }

    protected BaseHero(ColorEnum color, string image, string name, string description)
    {
        this.Health = DefaultHealth;
        this.Color = ViewCardColor.GetColor(color);
        this.TextColor = ViewCardColor.GetTextColor(color);

        this.Image = image;
        this.Name = name;
        this.Description = description;
    }

    public void AttackTarget(IAttackable target)
    {
        throw new NotImplementedException();
    }
    
    public void TriggerHeroPower()
    {
        throw new NotImplementedException();
    }
}
