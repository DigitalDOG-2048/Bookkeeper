using System;
using BookKeeper.Views;
using Syncfusion.Maui.Calendar;

namespace BookKeeper.ViewModels;

public partial class CalendarViewModel : BaseViewModel
{
    //private IPopupNavigation _popupNavigation;

    [ObservableProperty]
    CalendarDateRange selectedDateRange;

    [RelayCommand]
    async Task SelectAsync()
    {
        await Shell.Current.GoToAsync("..", false,
            new Dictionary<string, object>
            {
                {"CalendarDateRange", selectedDateRange}
            });
        //await _popupNavigation.PopAsync();
    }

    [RelayCommand]
    async Task CancelAsync()
    {
        //await _popupNavigation.PopAsync();

        await Shell.Current.GoToAsync("..", false);
    }
}

