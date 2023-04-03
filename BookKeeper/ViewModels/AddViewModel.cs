using System;
using System.Text.RegularExpressions;
using BookKeeper.Services;

namespace BookKeeper.ViewModels;

[QueryProperty("AccountBookIDStr", "AccountBookIDStr")]
public partial class AddViewModel : BaseViewModel
{
    RecordService recordService;
    List<string> expensesTypeStrList = new();
    List<string> incomeTypeStrList = new();

    public ObservableCollection<string> RecordTypeStrList { get; set; } = new();

    public AddViewModel(RecordService recordService)
    {
        this.recordService = recordService;

        dateTime = DateTime.Now;
        recordService.GetRecordTypeLists(expensesTypeStrList, incomeTypeStrList);
        radioSelectionValue = "Expenses";

        Reset();
    }

    private void Reset()
    {
        amount = (decimal)0;
        remarks = "";

        foreach (string type in expensesTypeStrList)
            RecordTypeStrList.Add(type);
    }

    [ObservableProperty]
    string remarks;
    [ObservableProperty]
    decimal amount;
    [ObservableProperty]
    DateTime dateTime;

    [ObservableProperty]
    string accountBookIDStr;

    //[ObservableProperty]
    //int typeSelectionIndex;

    public int TypeSelectionIndex => Constants.DefaultExpensesType;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TypeSelectionIndex))]
    string radioSelectionValue;

    //bool isExpenses = true;

    partial void OnRadioSelectionValueChanged(string value)
    {
        if (RecordTypeStrList.Count != 0)
            RecordTypeStrList.Clear();

        if (value == "Expenses")
        {
            foreach (string type in expensesTypeStrList)
                RecordTypeStrList.Add(type);

            //typeSelectionIndex = Constants.DefaultExpensesType;
        }
        else
        {
            foreach (string type in incomeTypeStrList)
                RecordTypeStrList.Add(type);

            //typeSelectionIndex = Constants.DefaultIncomeType;
        }
    }

    [RelayCommand]
    async Task AddAsync()
    {
        bool isExpenses;
        int accountBookID;
        ExpensesType expensesType = (ExpensesType)Constants.DefaultExpensesType;
        IncomeType incomeType = (IncomeType)Constants.DefaultIncomeType;
        string type, camelCase;

        // check input
        if (remarks == null)
            remarks = "";
        if (accountBookIDStr == null)
            accountBookID = Helper.global_account_book_id;
        else
            accountBookID = Int32.Parse(accountBookIDStr);
        if (amount  < 0)
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

        // add the record to database
        Guid guid = Guid.NewGuid();
        Record newRecord = new Record
        {
            ID = guid,
            Type = type,
            ExpensesType = expensesType,
            IncomeType = incomeType,
            IsExpenses = isExpenses,
            Remarks = remarks,
            Amount = amount,
            DateTime = dateTime,
            AccountBookID = accountBookID
        };

        int res = await recordService.AddRecordAsync(newRecord);

        if (res <= 0)
            await Shell.Current.DisplayAlert("Fail", "Something went wrong while adding record", "OK");
        else
        {
            await Shell.Current.DisplayAlert("Success", "Record Added!", "OK");

            Reset();
            //await Shell.Current.GoToAsync("..", true,
            //    new Dictionary<string, object>
            //    {
            //        { "RecordId", newRecord.ID }
            //    });
        }
    }
}