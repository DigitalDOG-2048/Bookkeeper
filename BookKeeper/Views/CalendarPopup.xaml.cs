using CommunityToolkit.Maui.Views;
using Syncfusion.Maui.Calendar;

namespace BookKeeper.Views;

public partial class CalendarPopup : Popup
{

    public CalendarPopup()
	{
        InitializeComponent();
    }

    private DateTime startDate;
    private DateTime endDate;

    private void OnCalendarSelectionChanged(System.Object sender, CalendarSelectionChangedEventArgs e)
    {

    }

    private void CancelButton_Clicked(System.Object sender, System.EventArgs e)
    {
        this.Close();
    }

    private void SelectButton_Clicked(System.Object sender, System.EventArgs e)
    {
        this.Close();
    }
}
