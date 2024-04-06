using CardCraftClient.View;

namespace CardCraftClient;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(StartPage), typeof(StartPage));
        Routing.RegisterRoute(nameof(LobbyPage), typeof(LobbyPage));
        Routing.RegisterRoute(nameof(HeroPage), typeof(HeroPage));
        Routing.RegisterRoute(nameof(DeckPage), typeof(DeckPage));
        Routing.RegisterRoute(nameof(GamePage), typeof(GamePage));
    }
}

