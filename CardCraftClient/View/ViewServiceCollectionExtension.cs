namespace CardCraftClient.View;

public static class ViewServiceCollectionExtension
{
    public static IServiceCollection RegisterViews(this IServiceCollection services)
    {
        services.AddTransient<StartPage>();

        return services;
    }
}