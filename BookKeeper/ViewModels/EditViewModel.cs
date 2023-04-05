using System;
using System.Text.RegularExpressions;
using BookKeeper.Services;

namespace BookKeeper.ViewModels;

[QueryProperty("Record", "Record")]
public partial class EditViewModel : BaseViewModel
{
    public ObservableCollection<string> RecordTypeStrList { get; set; } = new();

    RecordService recordService;
    List<string> expensesTypeStrList = new();
    List<string> incomeTypeStrList = new();

    public EditViewModel(RecordService recordService)
    {
        this.recordService = recordService;

        dateTime = DateTime.Now;
        recordService.GetRecordTypeLists(expensesTypeStrList, incomeTypeStrList);
    }

    [ObservableProperty]
    public string remarks;
    [ObservableProperty]
    public decimal amount;
    [ObservableProperty]
    public DateTime dateTime;

    [ObservableProperty]
    public int accountBookID;

    [ObservableProperty]
    //[NotifyPropertyChangedFor(nameof(TypeSelectionIndex))]
    public string radioSelectionValue;

    [ObservableProperty]
    Record record;

    [ObservableProperty]
    public int typeSelectionIndex;

    partial void OnRecordChanged(Record value)
    {
        Remarks = value.Remarks;
        Amount = value.Amount;
        DateTime = value.DateTime;
        RadioSelectionValue = value.IsExpenses ? "Expenses" : "Income";
    }

    [RelayCommand]
    async Task EditAsync()
    {
        Record newRecord = await CreateValidRecord(record.ID);
        int res = await recordService.EditRecordAsync(newRecord);

        if (res <= 0)
            await Shell.Current.DisplayAlert("Fail", "Something went wrong while editing record", "OK");
        else
        {
            await Shell.Current.DisplayAlert("Success", "Record Updated!", "OK");

            await Shell.Current.GoToAsync("..", true,
                new Dictionary<string, object>
                {
                    {"Record", newRecord}
                });
        }
    }

    partial void OnRadioSelectionValueChanged(string value)
    {
        if (RecordTypeStrList.Count != 0)
            RecordTypeStrList.Clear();

        if (value == "Expenses")
        {
            foreach (string type in expensesTypeStrList)
                RecordTypeStrList.Add(type);
            TypeSelectionIndex = (int)record.ExpensesType;
        }
        else
        {
            foreach (string type in incomeTypeStrList)
                RecordTypeStrList.Add(type);
            TypeSelectionIndex = (int)record.IncomeType;
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

