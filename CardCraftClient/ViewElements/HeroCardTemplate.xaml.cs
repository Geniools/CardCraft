namespace CardCraftClient.ViewElements;

public partial class HeroCardTemplate : ContentView
{
    public HeroCardTemplate()
    {
        InitializeComponent();
    }

    // The following BindableProperties are needed to send custom data in the template when it is used in different parts of the project

    // Name of the hero ========================================
    public static readonly BindableProperty NameProperty =
        BindableProperty.Create(nameof(Name), typeof(string), typeof(HeroCardTemplate), null);

    public string Name
    {
        get => (string)GetValue(NameProperty);
        set => SetValue(NameProperty, value);
    }

    // Image of the hero ========================================
    public static readonly BindableProperty ImageProperty =
        BindableProperty.Create(nameof(Image), typeof(ImageSource), typeof(HeroCardTemplate), null);

    public ImageSource Image
    {
        get => (ImageSource)GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    // Description of the hero ==================================
    public static readonly BindableProperty DescriptionProperty =
        BindableProperty.Create(nameof(Description), typeof(string), typeof(HeroCardTemplate), null);

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    // Health of the hero ========================================
    public static readonly BindableProperty HealthProperty =
        BindableProperty.Create(nameof(Health), typeof(string), typeof(HeroCardTemplate), null);

    public string Health
    {
        get => (string)GetValue(HealthProperty);
        set => SetValue(HealthProperty, value);
    }

    // Color of the hero ========================================
    public static readonly BindableProperty ColorProperty =
        BindableProperty.Create(nameof(Color), typeof(string), typeof(HeroCardTemplate), string.Empty);

    public string Color
    {
        get => (string)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    // Text color of the hero ========================================
    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(string), typeof(HeroCardTemplate), string.Empty);

    public string TextColor
    {
        get => (string)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }
}