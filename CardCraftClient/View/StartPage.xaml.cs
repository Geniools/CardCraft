using CardCraftClient.ViewModel;

namespace CardCraftClient.View;

public partial class StartPage : BasePage
{
    public StartPage(StartPageViewModel vm) : base(vm)
    {
        InitializeComponent();
    }


    private async void StartGameButton_Click(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LobbyPage));
    }
}