using System;
using BookKeeper.Services;
using BookKeeper.Views;
using Syncfusion.Maui.Calendar;

namespace BookKeeper.ViewModels;

public partial class SearchViewModel : BaseViewModel
{
    RecordService recordService;
    AccountBookService accountBookService;

    public ObservableCollection<AccountBook> AccountBookList { get; set; } = new();
    public ObservableCollection<Record> Records { get; set; } = new();
    public ObservableCollection<string> YearList { get; set; } = new();

    public SearchViewModel(RecordService recordService, AccountBookService accountBookService)
	{
        this.recordService = recordService;
        this.accountBookService = accountBookService;

        GetYearList();
    }

    [ObservableProperty]
    int accountBookID = -1;

    [ObservableProperty]
    string keyword;

    [ObservableProperty]
    string year = DateTime.Now.Year.ToString();

    [ObservableProperty]
    AccountBook accountBook;

    public void GetYearList()
    {
        foreach (int year in Constants.YearRange)
        {
            YearList.Add(year.ToString());
        }
    }

    [RelayCommand]
    public async Task GetAccountBookListAsync()
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
    public async Task SearchAsync()
    {
        await GetRecordsAsync(Keyword);
    }

    [RelayCommand]
    async Task GetRecordsAsync(string keyword)
    {
        try
        {
            List<Record> response = await recordService.GetYearRecordsByKeywordAsync(Int32.Parse(Year), Keyword, AccountBookID);

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
        await recordService.DeleteRecordByIDAsync(record.ID);
    }
}

