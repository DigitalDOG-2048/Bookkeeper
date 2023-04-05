namespace BookKeeper;
using BookKeeper.Views;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(RecordsPage), typeof(RecordsPage));
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
        Routing.RegisterRoute(nameof(AddPage), typeof(AddPage));
        Routing.RegisterRoute(nameof(CalendarPopup), typeof(CalendarPopup));
        Routing.RegisterRoute(nameof(SummaryPage), typeof(SummaryPage));
    }
}

