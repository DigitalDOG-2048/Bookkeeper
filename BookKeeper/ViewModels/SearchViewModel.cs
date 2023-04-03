using System;
using BookKeeper.Services;
using BookKeeper.Views;
using Syncfusion.Maui.Calendar;

namespace BookKeeper.ViewModels;

public partial class SearchViewModel : BaseViewModel
{
    RecordService recordService;

    public ObservableCollection<AccountBook> AccountBookList { get; set; } = new();
    public ObservableCollection<Record> Records { get; set; } = new();
    public ObservableCollection<string> YearList { get; set; } = new();

    public SearchViewModel(RecordService recordService)
	{
        this.recordService = recordService;

        GetAccountBookList();
        GetYearList();
    }

    [ObservableProperty]
    string keyword;

    [ObservableProperty]
    string year = DateTime.Now.Year.ToString();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AccountBookID))]
    int selectedIndex = 0;

    public int AccountBookID => SelectedIndex + 1;

    public void GetYearList()
    {
        foreach (int year in Constants.YearRange)
        {
            YearList.Add(year.ToString());
        }
    }

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
    public async Task SearchAsync()
    {
        // todo
    }

    [RelayCommand]
    async Task GetRecordsAsync(string keyword)
    {
        try
        {
            List<Record> response = await recordService.GetYearRecordsByKeywordAsync(Int32.Parse(Year), Keyword);

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

