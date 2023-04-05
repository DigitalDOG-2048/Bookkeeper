using CommunityToolkit.Maui.Views;
using BookKeeper.Services;
using Syncfusion.Maui.Calendar;
using System;

namespace BookKeeper.ViewModels;

[QueryProperty("CalendarDateRange", "CalendarDateRange")]
public partial class RecordsViewModel : BaseViewModel
{
    RecordService recordService;
    AccountBookService accountBookService;

    public ObservableCollection<Record> Records { get; set; } = new();
    public ObservableCollection<AccountBook> AccountBookList { get; set; } = new();

    public RecordsViewModel(RecordService recordService, AccountBookService accountBookService)
    {
        Title = "Records";
        this.recordService = recordService;
        this.accountBookService = accountBookService;
    }

    [ObservableProperty]
    int accountBookID = -1;

    [ObservableProperty]
    int recordId;

    [ObservableProperty]
    CalendarDateRange calendarDateRange;

    partial void OnAccountBookIDChanged(int value)
    {
        GetRecordsAsync();
    }

    [RelayCommand]
    async Task GetAccountBookListAsync()
    {
        if (AccountBookList.Count > 0)
        {
            AccountBookList.Clear();
        }

        var response = await accountBookService.GetAccountBookListAsync();
        if (response?.Count > 0)
        {
            foreach (var item in response)
                AccountBookList.Add(item);
        }
    }

    [RelayCommand]
    async Task PopupCalendarAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(CalendarPopup)}", false);
    }

    [RelayCommand]
    async Task GoToAddAsync()
    {
        if (AccountBookID == -1)
        {
            showErrorAlert("Please select an account book first!");
            return;
        }

        await Shell.Current.GoToAsync($"{nameof(AddPage)}?AccountBookID={AccountBookID}", false);

        await GetRecordsAsync();
    }

    [RelayCommand]
    async Task GoToDetailAsync(Record record)
    {
        if (record == null)
            return;

        if (AccountBookID == -1)
        {
            showErrorAlert("Please select an account book first!");
            return;
        }

        AccountBook accountBook = await accountBookService.GetAccountBookAsync(AccountBookID);

        await Shell.Current.GoToAsync($"{nameof(DetailPage)}", true,
            new Dictionary<string, object>
            {
                { "Record", record },
                { "AccountBookName", AccountBookList[AccountBookID].AccountBookName }
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
            List<Record> response = await recordService.GetRecordsByDateRangeAsync(calendarDateRange, AccountBookID);

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
    async Task DeleteAsync(Record record)
    {
        if (Records.Contains(record))
        {
            Records.Remove(record);
        }

        // delete the record from database
        try
        {
            int res = await recordService.DeleteRecordByIDAsync(record.ID);
            if (res == -1)
                await Shell.Current.DisplayAlert("Error", "Failed to delete in database", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }
}

