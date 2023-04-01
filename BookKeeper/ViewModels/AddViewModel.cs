using System;
using Type = BookKeeper.Models.Type;

namespace BookKeeper.ViewModels;

[QueryProperty(nameof(AccountBook), "AccountBook")]
public partial class AddViewModel : BaseViewModel
{
	public AddViewModel()
	{
        this.record = new Record
        {
            Type = "Dining",
            Amount = (decimal)0,
            DateTime = DateTime.Now,
            isExpenses = true
        };
	}

    [ObservableProperty]
	Record record;

    [ObservableProperty]
    AccountBook accountBook;

    [ObservableProperty]
    string radioSelectionValue;

	[RelayCommand]
	async Task AddAsync()
	{
        record.isExpenses = radioSelectionValue == "Expenses" ? true : false;
        record.AccountBookName = accountBook.AccountBookName;

        // add to db
        await Shell.Current.DisplayAlert("", "Successfully added a record!", "OK");
    }
}

