using CardCraftClient.ViewModel;

namespace CardCraftClient.View;

public partial class HeroPage : BasePage
{
    public HeroPage(HeroPageViewModel vm) : base(vm)
    {
        InitializeComponent();
    }
}