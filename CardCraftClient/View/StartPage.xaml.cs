using CardCraftClient.ViewModel;

namespace CardCraftClient.View;

public partial class StartPage : ContentPage
{
    public StartPage(StartPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}