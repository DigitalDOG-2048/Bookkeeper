using System;
using CommunityToolkit.Maui.Views;
using Syncfusion.Maui.Calendar;

namespace BookKeeper.Views;

public partial class CalendarPopup
{
    public CalendarPopup(CalendarViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
    }
}
