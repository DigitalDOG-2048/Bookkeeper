using Microsoft.Extensions.Logging;
using BookKeeper.Views;
using BookKeeper.Services;

namespace BookKeeper;

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

#if DEBUG
		builder.Logging.AddDebug();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<RecordsPage>();
        builder.Services.AddTransient<DetailPage>();

        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<RecordsViewModel>();
        builder.Services.AddTransient<RecordDetailViewModel>();

        builder.Services.AddSingleton<RecordService>();
#endif


        return builder.Build();
	}
}

