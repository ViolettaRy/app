using Microsoft.Extensions.Logging;
using ShopApp.Models;
using ShopApp.Views;

namespace ShopApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
        builder.Services.AddSingleton(new AppRepository("AppNew.db"));
        builder.Services.AddSingleton<CategoryPage>();
        builder.Services.AddSingleton<AnimalPage>();
        builder.Services.AddSingleton<ProviderPage>();
        builder.Services.AddSingleton<ProductPage>();
        builder.Services.AddSingleton<ProfitPage>();
        builder.Services.AddSingleton<MarkPage>();
        builder.Services.AddSingleton<TemplatePage>();
        builder.Services.AddSingleton<RevenuePage>();
        builder.Services.AddSingleton<RevisionPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
	}
}
