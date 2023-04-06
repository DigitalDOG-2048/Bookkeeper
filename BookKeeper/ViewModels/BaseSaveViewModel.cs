using System;
using System.Text.RegularExpressions;
using BookKeeper.Services;

namespace BookKeeper.ViewModels;

[QueryProperty("AccountBookID", "AccountBookID")]
public partial class BaseSaveViewModel : BaseViewModel
{
    public RecordService recordService;
    List<string> expensesTypeStrList = new();
    List<string> incomeTypeStrList = new();

    public ObservableCollection<string> RecordTypeStrList { get; set; } = new();

    public BaseSaveViewModel(RecordService recordService)
    {
        this.recordService = recordService;

        dateTime = DateTime.Now;
        recordService.GetRecordTypeLists(expensesTypeStrList, incomeTypeStrList);
        radioSelectionValue = "Expenses";
    }

    public void Reset()
    {
        amount = (decimal)0;
        remarks = "";

        foreach (string type in expensesTypeStrList)
            RecordTypeStrList.Add(type);
    }

    [ObservableProperty]
    public string remarks;
    [ObservableProperty]
    public decimal amount;
    [ObservableProperty]
    public DateTime dateTime;

    [ObservableProperty]
    public int accountBookID;

    public int TypeSelectionIndex => Constants.DefaultExpensesType;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TypeSelectionIndex))]
    public string radioSelectionValue;

    //bool isExpenses = true;

    partial void OnRadioSelectionValueChanged(string value)
    {
        if (RecordTypeStrList.Count != 0)
            RecordTypeStrList.Clear();

        if (value == "Expenses")
        {
            foreach (string type in expensesTypeStrList)
                RecordTypeStrList.Add(type);
        }
        else
        {
            foreach (string type in incomeTypeStrList)
                RecordTypeStrList.Add(type);
        }
    }

    public async Task<Record> CreateValidRecord(Guid id)
    {
        bool isExpenses;
        ExpensesType expensesType = (ExpensesType)Constants.DefaultExpensesType;
        IncomeType incomeType = (IncomeType)Constants.DefaultIncomeType;
        string type, camelCase;

        // check input
        if (remarks == null)
            remarks = "";

        if (amount == 0)
            // alert message display https://github.com/dotnet/maui/discussions/3518
            await Shell.Current.DisplayAlert("Wrong Input", "Please enter the amount", "OK");

        if (radioSelectionValue == "Expenses")
        {
            isExpenses = true;
            expensesType = (ExpensesType)TypeSelectionIndex;
            camelCase = Enum.GetName(typeof(ExpensesType), expensesType);
            amount = Math.Round(-amount, 2);
        }
        else
        {
            isExpenses = false;
            incomeType = (IncomeType)TypeSelectionIndex;
            camelCase = Enum.GetName(typeof(IncomeType), incomeType);
        }
        type = Regex.Replace(camelCase, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();

        Record record = new Record
        {
            ID = id,
            Type = type,
            ExpensesType = expensesType,
            IncomeType = incomeType,
            IsExpenses = isExpenses,
            Remarks = remarks,
            Amount = amount,
            DateTime = dateTime,
            AccountBookID = AccountBookID
        };
        return record;
    }
}