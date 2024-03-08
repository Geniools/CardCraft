using CardCraftClient.View;
using CardCraftClient.ViewModel;

namespace CardCraftClient;

public partial class GamePage : BasePage
{
    public GamePage(GamePageViewModel vm) : base(vm)
    {
        InitializeComponent();
    }
}

