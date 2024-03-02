using CardCraftClient.View;
using CardCraftClient.ViewModel;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace CardCraftClient;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .Services
            .RegisterViews()
            .RegisterViewModels();

#if WINDOWS
        builder.ConfigureLifecycleEvents(events =>
        {
            // Make sure to add "using Microsoft.Maui.LifecycleEvents;" in the top of the file 
            events.AddWindows(windowsLifecycleBuilder =>
            {
                windowsLifecycleBuilder.OnWindowCreated(window =>
                {
                    window.ExtendsContentIntoTitleBar = false;
                    var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                    var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
                    var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);
                    switch (appWindow.Presenter)
                    {
                        case Microsoft.UI.Windowing.OverlappedPresenter overlappedPresenter:
                            overlappedPresenter.SetBorderAndTitleBar(true, true);
                            overlappedPresenter.Maximize();
                            break;
                    }
                });
            });
        });
#endif

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
