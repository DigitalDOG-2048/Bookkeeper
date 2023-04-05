using System;
using BookKeeper.Data;

namespace BookKeeper.Services;

public class AccountBookService
{
    AccountBookDatabase accountBookDatabase = App.AccountBookDatabase;

    public AccountBookService()
	{
    }

    public async Task<List<AccountBook>> GetAccountBookListAsync()
    {
        // get account books from database
        List<AccountBook> accountBookList = await accountBookDatabase.GetAllAccountBooks();

        // add default account book
        if (accountBookList.Count == 0)
        {
            AccountBook defaultAccountBook = new AccountBook { AccountBookName = "Default", ID = 0 };
            await accountBookDatabase.InsertAccountBookAsync(defaultAccountBook);
            accountBookList.Add(defaultAccountBook);
            Helper.global_account_book_id = defaultAccountBook.ID;
        }

        return accountBookList;
    }

    public async Task<AccountBook> GetAccountBookAsync(int id)
    {
        return await accountBookDatabase.GetAccountBookByID(id);
    }

    public async Task<int> AddAccountBookAsync(AccountBook accountBook)
    {
        return await accountBookDatabase.InsertAccountBookAsync(accountBook);
    }

    public async Task<int> EditAccountBookAsync(AccountBook accountBook)
    {
        return await accountBookDatabase.UpdateAccountBookAsync(accountBook);
    }

    public async Task<int> DeleteAccountBookByIDAsync(int id)
    {
        return await accountBookDatabase.DeleteAccountBookAsync(id);
    }
}

