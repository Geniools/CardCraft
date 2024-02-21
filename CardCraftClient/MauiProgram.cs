﻿using CardCraftClient.View;
using CardCraftClient.ViewModel;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

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

#if DEBUG
    	builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
