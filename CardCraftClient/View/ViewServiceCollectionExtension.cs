namespace CardCraftClient.View;

public static class ViewServiceCollectionExtension
{
    public static IServiceCollection RegisterViews(this IServiceCollection services)
    {
        services.AddTransient<StartPage>();
        services.AddTransient<GamePage>();
        services.AddTransient<LobbyPage>();
        services.AddTransient<HeroPage>();
        services.AddTransient<DeckPage>();

        return services;
    }
}