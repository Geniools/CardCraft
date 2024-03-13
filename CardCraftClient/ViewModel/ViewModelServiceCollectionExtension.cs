namespace CardCraftClient.ViewModel;

public static class ViewModelServiceCollectionExtension
{
    public static IServiceCollection RegisterViewModels(this IServiceCollection services)
    {
        services.AddTransient<StartPageViewModel>();
        services.AddTransient<LobbyPageViewModel>();

        return services;
    }
}