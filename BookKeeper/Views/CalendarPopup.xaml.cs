using System;
using CommunityToolkit.Maui.Views;
using Mopups.Interfaces;
using Mopups.Services;
using Syncfusion.Maui.Calendar;

namespace BookKeeper.Views;

public partial class CalendarPopup
{
    private IPopupNavigation _popupNavigation;

    public CalendarPopup()
	{
        InitializeComponent();
        //BindingContext = viewModel;
    }

    void DateRangeCalendar_SelectionChanged(System.Object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
    {
    }

    async void SelectButton_Clicked(System.Object sender, System.EventArgs e)
    {
        CalendarDateRange calendarDateRange = this.DateRangeCalendar.SelectedDateRange;
        await Shell.Current.GoToAsync($"{nameof(RecordsPage)}", false,
            new Dictionary<string, object>
            {
                {"CalendarDateRange", calendarDateRange}
            });
    }

    void CancelButton_Clicked(System.Object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("..", true);
    }
}
