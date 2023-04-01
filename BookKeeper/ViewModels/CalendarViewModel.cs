using System;
using BookKeeper.Views;
using Mopups.Interfaces;
using Syncfusion.Maui.Calendar;

namespace BookKeeper.ViewModels;

public partial class CalendarViewModel : BaseViewModel
{
    //private IPopupNavigation _popupNavigation;

    public CalendarViewModel(CalendarDateRange calendarDateRange)
	{
        //this._popupNavigation = popupNavigation;
        this.calendarDateRange = calendarDateRange;
    }

    [ObservableProperty]
    CalendarDateRange calendarDateRange;

    [RelayCommand]
    async Task SelectAsync()
    {
        await Shell.Current.DisplayAlert("StartDate: " + calendarDateRange?.StartDate?.ToString("dd/MM/yyyy"),
            "EndDate: " + calendarDateRange?.EndDate?.ToString("dd/MM/yyyy"), "OK");

        await Shell.Current.GoToAsync("..", true,
            new Dictionary<string, object>
            {
                {"CalendarDateRange", calendarDateRange}
            });
        //await Shell.Current.GoToAsync($"{nameof(RecordsPage)}", true,
        //    new Dictionary<string, object>
        //    {
        //        {"CalendarDateRange", calendarDateRange}
        //    });
        //await _popupNavigation.PopAsync();
    }

    [RelayCommand]
    async Task CancelAsync()
    {
        //DateTime now = DateTime.Now;
        //var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
        //selectedDateRange.StartDate = firstDayOfMonth;
        //selectedDateRange.EndDate = firstDayOfMonth.AddMonths(1).AddDays(-1);

        //await _popupNavigation.PopAsync();

        await Shell.Current.GoToAsync("..", true);
    }
}

