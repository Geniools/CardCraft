namespace CardCraftClient.View;

public partial class LobbyPage : ContentPage
{
	public LobbyPage()
	{
		InitializeComponent();
	}

    private void JoinButton_Clicked(object sender, EventArgs e)
    {
        lobbyEntry.IsVisible = true;
        lobbyEntry.Focus();
    }
}