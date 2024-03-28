using CardCraftClient.ViewModel;

namespace CardCraftClient.View;

public partial class BasePage : ContentPage
{
	public BasePage(BaseViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;
    }

#if WINDOWS
    private Microsoft.UI.Windowing.AppWindow GetAppWindow(MauiWinUIWindow window)
    {
        var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
        var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
        var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);
        return appWindow;
    }
#endif

    private void OnToggleFullscreenClicked(object sender, EventArgs e)
    {
#if WINDOWS
		var window = GetParentWindow().Handler.PlatformView as MauiWinUIWindow;
        
        var appWindow = GetAppWindow(window);

        switch (appWindow.Presenter)
        {
            case Microsoft.UI.Windowing.OverlappedPresenter overlappedPresenter:
                if (overlappedPresenter.State == Microsoft.UI.Windowing.OverlappedPresenterState.Maximized)
                {
                    overlappedPresenter.SetBorderAndTitleBar(true, true);
                    overlappedPresenter.Restore();
                }
                else
                {
                    overlappedPresenter.SetBorderAndTitleBar(false, false);
                    overlappedPresenter.Maximize();
                }

                break;
        }
#endif
    }
}