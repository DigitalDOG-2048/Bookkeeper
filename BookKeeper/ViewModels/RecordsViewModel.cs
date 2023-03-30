using CommunityToolkit.Maui.Views;
using BookKeeper.Services;
using BookKeeper.Views;

namespace BookKeeper.ViewModels;

[QueryProperty("DateTime", "DateTime")]
public partial class RecordsViewModel : BaseViewModel
{
    RecordService recordService;
    public ObservableCollection<Record> Records { get; set; } = new();
    public ObservableCollection<AccountBook> AccountBookList { get; set; } = new();

    public RecordsViewModel(RecordService recordService)
	{
        Title = "Monthly Records";
        this.recordService = recordService;
        GetAccountBookList();
        GetRecordsAsync();
    }

    [ObservableProperty]
    DateTime dateTime = DateTime.Now;

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
    async void PopupCalendar(DateTime dateTime)
    {
        var result = await Shell.Current.ShowPopupAsync(new CalendarPopup());
    }

    [RelayCommand]
    async Task GoToAddAsync(DateTime dateTime)
    {
        await Shell.Current.GoToAsync($"{nameof(AddPage)}", true,
            new Dictionary<string, object>
            {
                {"DateTime", dateTime}
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
            var response = await recordService.GetRecords();

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

