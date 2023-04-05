using System;
using AndroidX.Lifecycle;
using BookKeeper.Services;
using Microsoft.Maui.Controls;

namespace BookKeeper.ViewModels;

public partial class AccountViewModel : BaseViewModel
{
	//RecordService accountBookService;
    public AccountBookService accountBookService;

    public ObservableCollection<AccountBook> AccountBookList { get; set; } = new();

    public AccountViewModel(AccountBookService accountBookService)
    {
        this.accountBookService = accountBookService;
    }

    [ObservableProperty]
    AccountBook selectedItem;

    [RelayCommand]
    async Task GetAccountBookListAsync()
    {
        if (AccountBookList.Count > 0)
            AccountBookList.Clear();

        var response = await accountBookService.GetAccountBookListAsync();
        if (response?.Count > 0)
        {
            foreach (var item in response)
                AccountBookList.Add(item);
        }
    }

    [RelayCommand]
    async Task GoToAddAsync()
    {
        string result = await Shell.Current.DisplayPromptAsync("Create New Account Book", "",
            placeholder: "Enter name");

        if (result != null)
        {
            int id = AccountBookList.Count;
            AccountBook newAccountBook = new AccountBook { AccountBookName = result, ID = id };

            int res = await accountBookService.AddAccountBookAsync(newAccountBook);

            if (res <= 0)
                await Shell.Current.DisplayAlert("Fail", "Something went wrong while adding account book", "OK");
            else
            {
                await Shell.Current.DisplayAlert("Success", "Account Book Added!", "OK");
            }

            await GetAccountBookListAsync();
        }
    }

    [RelayCommand]
    async Task EditAsync(AccountBook accountBook)
    {
        string result = await Shell.Current.DisplayPromptAsync("Edit Name of Account Book", "",
            placeholder: "Enter new name");

        if (result != null)
        {
            accountBook.AccountBookName = result;
            int res = await accountBookService.EditAccountBookAsync(accountBook);

            if (res <= 0)
                await Shell.Current.DisplayAlert("Fail", "Something went wrong while editing account book name", "OK");
            else
            {
                await Shell.Current.DisplayAlert("Success", "Name Updated!", "OK");
            }

            await GetAccountBookListAsync();
        }
    }


    [RelayCommand]
    async Task DeleteAsync(AccountBook accountBook)
    {
        if (AccountBookList.Contains(accountBook))
        {
            AccountBookList.Remove(accountBook);
        }
        try
        {
            int res = await accountBookService.DeleteAccountBookByIDAsync(accountBook.ID);
            if (res == -1)
                await Shell.Current.DisplayAlert("Error", "Failed to delete in database", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }
}

