namespace CardCraftClient.ViewElements;

public partial class BaseSpellTemplate : ContentView
{
    public BaseSpellTemplate()
    {
        InitializeComponent();
    }

    // Name of the card ========================================
    public static readonly BindableProperty NameProperty =
        BindableProperty.Create(nameof(Name), typeof(string), typeof(BaseMinionTemplate), null);

    public string Name
    {
        get => (string)GetValue(NameProperty);
        set => SetValue(NameProperty, value);
    }

    // Image of the card ========================================
    public static readonly BindableProperty ImageProperty =
        BindableProperty.Create(nameof(Image), typeof(ImageSource), typeof(BaseMinionTemplate), null);

    public ImageSource Image
    {
        get => (ImageSource)GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    // Description of the card ==================================
    public static readonly BindableProperty DescriptionProperty =
        BindableProperty.Create(nameof(Description), typeof(string), typeof(BaseMinionTemplate), null);

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    // Color of the card ========================================
    public static readonly BindableProperty ColorProperty =
        BindableProperty.Create(nameof(Color), typeof(string), typeof(BaseMinionTemplate), string.Empty);

    public string Color
    {
        get => (string)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    // Text color of the card ========================================
    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(string), typeof(BaseMinionTemplate), string.Empty);

    public string TextColor
    {
        get => (string)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    // ManaCost of the card ========================================
    public static readonly BindableProperty ManaCostProperty =
        BindableProperty.Create(nameof(ManaCost), typeof(int), typeof(BaseMinionTemplate), 0);

    public int ManaCost
    {
        get => (int)GetValue(ManaCostProperty);
        set => SetValue(ManaCostProperty, value);
    }
}