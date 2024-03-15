using CardCraftClient.ViewModel;

namespace CardCraftClient.View;

public partial class LobbyPage : BasePage
{
	public LobbyPage(LobbyPageViewModel vm) : base(vm)
	{
		InitializeComponent();
	}

    private async void StartGameButton_Click(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GamePage));
    }
}