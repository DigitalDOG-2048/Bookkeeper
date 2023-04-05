using CommunityToolkit.Maui.Views;
using BookKeeper.Services;
using BookKeeper.Views;
using Syncfusion.Maui.Calendar;
using System;

namespace BookKeeper.ViewModels;

[QueryProperty("CalendarDateRange", "CalendarDateRange")]
//[QueryProperty("RecordId", "RecordId")]
public partial class RecordsViewModel : BaseViewModel
{
    RecordService recordService;
    //IPopupNavigation popupNavigation;

    public ObservableCollection<Record> Records { get; set; } = new();
    public ObservableCollection<AccountBook> AccountBookList { get; set; } = new();

    public RecordsViewModel(RecordService recordService)
    {
        Title = "Records";
        this.recordService = recordService;
        //this.popupNavigation = popupNavigation;
        // todo
        //this.accountBook = new AccountBook { AccountBookName = "Personal", ID = 1 };

        GetAccountBookList();
        GetRecordsAsync();
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AccountBookID))]
    int selectedIndex = 0;

    [ObservableProperty]
    int recordId;

    [ObservableProperty]
    CalendarDateRange calendarDateRange;
    
    public int AccountBookID => SelectedIndex + 1;

    partial void OnSelectedIndexChanged(int value)
    {
        Helper.global_account_book_id = selectedIndex;
    }

    //partial void OnRecordIdChanged(int value)
    //{
    //    GetRecordsAsync();
    //}

    //partial void OnCalendarDateRangeChanged(CalendarDateRange value)
    //{
    //    GetRecordsAsync();
    //}

    async void GetAccountBookList()
    {
        var response = await recordService.GetAccountBookList();
        if (response?.Count > 0)
        {
            foreach (var item in response)
                AccountBookList.Add(item);
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
        await Shell.Current.GoToAsync($"{nameof(AddPage)}?AccountBookID={AccountBookID}", false);

        await GetRecordsAsync();
    }

    [RelayCommand]
    async Task GoToDetailAsync(Record record)
    {
        if (record == null)
            return;

        this.recordId = record.ID;
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
            List<Record> response = await recordService.GetRecordsByDateRangeAsync(calendarDateRange);

            if (Records.Count != 0)
                Records.Clear();

            foreach (Record record in response)
                Records.Add(record);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
	}

    [RelayCommand]
    async Task Delete(Record record)
    {
        if (Records.Contains(record))
        {
            Records.Remove(record);
        }

        // delete the record from database
        await recordService.DeleteRecordByIdAsync(record.ID);
    }
}

