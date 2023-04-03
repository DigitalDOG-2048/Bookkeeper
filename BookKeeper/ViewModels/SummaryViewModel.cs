using System;
using BookKeeper.Services;
using Syncfusion.Maui.Calendar;

namespace BookKeeper.ViewModels;

public partial class SummaryViewModel : BaseViewModel
{
	BalanceService balanceService;
    private readonly Task initTask;

    public ObservableCollection<Balance> BalanceList { get; set; } = new();
    public ObservableCollection<string> YearList { get; set; } = new();

    public SummaryViewModel(BalanceService balanceService)
    {
        this.balanceService = balanceService;

        GetYearList();
    }

	[ObservableProperty]
	string year = DateTime.Now.Year.ToString();
    
    [ObservableProperty]
    Balance yearBalance;
    [ObservableProperty]
    decimal expensesPercentage;
    [ObservableProperty]
    decimal incomePercentage;

    partial void OnYearBalanceChanged(Balance value)
    {
        decimal expenses = YearBalance.ExpensesAmount;
        decimal income = YearBalance.IncomeAmount;
        decimal total = -expenses + income;
        ExpensesPercentage = expenses == 0 ? 0 : -expenses / total;
        IncomePercentage = income == 0 ? 0 : income / total;
    }

partial void OnYearChanged(string value)
    {
        GetBalanceListAsync();
    }

    public void GetYearList()
    {
        foreach (int year in Constants.YearRange)
        {
            YearList.Add(year.ToString());
        }
    }

    [RelayCommand]
    public async Task GetBalanceListAsync()
    {
		try
        {
            List<Balance> response = await balanceService.GetBalanceList(year);

            if (BalanceList?.Count > 0)
                BalanceList.Clear();

            foreach (Balance balance in response)
            {
                BalanceList.Add(balance);
            }

            YearBalance = balanceService.GetYearBalance(year, BalanceList);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }
}

