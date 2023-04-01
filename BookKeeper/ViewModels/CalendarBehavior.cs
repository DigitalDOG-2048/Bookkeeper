using System;
using Mopups.Interfaces;
using Mopups.Services;
using Syncfusion.Maui.Calendar;

namespace BookKeeper.ViewModels;

public class CalendarBehavior : Behavior<SfCalendar>
{
    private SfCalendar sfCalendar;
    private DateTime startDate;
    private DateTime endDate;
    //IPopupNavigation popupNavigation;

    protected override void OnAttachedTo(SfCalendar bindable)
	{
        base.OnAttachedTo(bindable);
        this.sfCalendar = bindable;
        this.sfCalendar.SelectionChanged += SfCalendar_SelectionChanged;
	}

    private void SfCalendar_SelectionChanged(object sender, CalendarSelectionChangedEventArgs e)
    {
        if (sfCalendar.SelectedDateRange != null)
        {
            startDate = (DateTime)sfCalendar.SelectedDateRange.StartDate;
            if (sfCalendar.SelectedDateRange.EndDate != null)
                endDate = (DateTime)sfCalendar.SelectedDateRange.EndDate;
            else
                endDate = (DateTime)sfCalendar.SelectedDateRange.StartDate;
        }

        //Shell.Current.DisplayAlert("StartDate:" + " " + startDate.ToString("dd/MM/yyyy"), "EndDate:" + " " + endDate.ToString("dd/MM/yyyy"), "OK");
    }

    protected override void OnDetachingFrom(SfCalendar bindable)
    {
        base.OnDetachingFrom(bindable);

        if (sfCalendar != null)
        {
            sfCalendar.SelectionChanged -= SfCalendar_SelectionChanged;
        }

        sfCalendar = null;
    }
}

