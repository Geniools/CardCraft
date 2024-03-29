using CardCraftClient.ViewModel;

namespace CardCraftClient.View;

public partial class DeckPage : BasePage
{
    public DeckPage(DeckPageViewModel vm) : base(vm)
    {
        InitializeComponent();
    }
}