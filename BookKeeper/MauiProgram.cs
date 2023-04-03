using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using BookKeeper.Views;
using BookKeeper.Services;
using CommunityToolkit.Maui;
using BookKeeper.Data;

namespace BookKeeper;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionCore()
            .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

#if DEBUG
        builder.Logging.AddDebug();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<RecordsPage>();
        builder.Services.AddTransient<DetailPage>();
        builder.Services.AddTransient<SummaryPage>();
        builder.Services.AddTransient<CalendarPopup>();
        builder.Services.AddTransient<AddPage>();
        builder.Services.AddTransient<SearchPage>();

        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<RecordsViewModel>();
        builder.Services.AddTransient<RecordDetailViewModel>();
        builder.Services.AddTransient<CalendarViewModel>();
        builder.Services.AddTransient<AddViewModel>();
        builder.Services.AddTransient<SummaryViewModel>();
        builder.Services.AddTransient<SearchViewModel>();

        builder.Services.AddSingleton<RecordService>();
        builder.Services.AddSingleton<BalanceService>();

        builder.Services.AddSingleton<RecordDatabase>(s =>
            ActivatorUtilities.CreateInstance<RecordDatabase>(s));

#endif

        return builder.Build();
    }
}