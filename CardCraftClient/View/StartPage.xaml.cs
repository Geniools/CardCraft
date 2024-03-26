using CardCraftClient.ViewModel;

namespace CardCraftClient.View;

public partial class StartPage : BasePage
{
    public StartPage(StartPageViewModel vm) : base(vm)
    {
        InitializeComponent();
    }
}