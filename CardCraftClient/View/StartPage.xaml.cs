using CardCraftClient.ViewModel;

namespace CardCraftClient.View;

public partial class StartPage : BasePage
{
    public StartPage(StartPageViewModel vm) : base(vm)
    {
        InitializeComponent();
    }

    private void ChooseHeroButton_OnClicked(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(HeroPage));
    }
}