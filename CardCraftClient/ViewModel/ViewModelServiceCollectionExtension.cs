namespace CardCraftClient.ViewModel;

public static class ViewModelServiceCollectionExtension
{
    public static IServiceCollection RegisterViewModels(this IServiceCollection services)
    {
        services.AddTransient<StartPageViewModel>();
        services.AddTransient<LobbyPageViewModel>();
        services.AddTransient<GamePageViewModel>();

        return services;
    }
}