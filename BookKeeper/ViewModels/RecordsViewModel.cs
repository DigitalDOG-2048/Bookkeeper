using CommunityToolkit.Maui.Views;
using BookKeeper.Services;
using BookKeeper.Views;
using Mopups.Services;
using Mopups.Interfaces;
using Syncfusion.Maui.Calendar;
using System;
using Xamarin.Google.Crypto.Tink.Mac;

namespace BookKeeper.ViewModels;

[QueryProperty("CalendarDateRange", "CalendarDateRange")]
public partial class RecordsViewModel : BaseViewModel
{
    RecordService recordService;
    IPopupNavigation popupNavigation;

    public ObservableCollection<Record> Records { get; set; } = new();
    public ObservableCollection<AccountBook> AccountBookList { get; set; } = new();

    [ObservableProperty]
    CalendarDateRange calendarDateRange;

    [ObservableProperty]
    AccountBook accountBook;

    public RecordsViewModel(RecordService recordService, IPopupNavigation popupNavigation)
	{
        Title = "Records";
        this.recordService = recordService;
        this.popupNavigation = popupNavigation;

        GetAccountBookList();
        GetRecordsAsync();
    }

    async void GetAccountBookList()
    {
        var response = await recordService.GetAccountBookList();
        if (response?.Count > 0)
        {
            foreach (var accountBook in response)
                AccountBookList.Add(accountBook);
        }
    }

    [RelayCommand]
    async Task PopupCalendarAsync()
    {
        //var result = await Shell.Current.ShowPopupAsync(new CalendarPopup(popupResult));
        //await Shell.Current.DisplayAlert("Result", $"Result: {result}", "OK");
        //await popupNavigation.PushAsync(new CalendarPopup());
        await Shell.Current.GoToAsync($"{nameof(CalendarPopup)}", true);
    }

    [RelayCommand]
    async Task GoToAddAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(AddPage)}", false,
            new Dictionary<string, object>
        {
            {"AccountBook", accountBook}
        });
    }

    [RelayCommand]
    async Task GoToDetailAsync(Record record)
    {
        if (record == null)
            return;

        await Shell.Current.GoToAsync($"{nameof(DetailPage)}", true,
            new Dictionary<string, object>
        {
            {"Record", record}
        });
    }

    [RelayCommand]
    async Task GetRecordsAsync()
	{
		if (IsBusy)
			return;

        try
        {
            IsBusy = true;
            var response = calendarDateRange == null ?
                await recordService.GetRecords() :
                await recordService.GetRecordsOfSelectedDateRange(
                    (DateTime)calendarDateRange.StartDate,
                    (DateTime)calendarDateRange.EndDate);

            if (Records.Count != 0)
                Records.Clear();

            foreach (var record in response)
                Records.Add(record);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
	}

    //[RelayCommand]
    //async Task Add()
    //{

    //}

    [RelayCommand]
    void Delete(Record record)
    {
        if (Records.Contains(record))
        {
            Records.Remove(record);
        }
    }
}

