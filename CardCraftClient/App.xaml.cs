using CardCraftClient.View;

namespace CardCraftClient;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);

        var deviceWidth = DeviceDisplay.Current.MainDisplayInfo.Width;
        var deviceHeight = DeviceDisplay.Current.MainDisplayInfo.Height;

        window.MinimumHeight = deviceHeight;
        window.MinimumWidth = deviceWidth;

        return window;
    }
}
