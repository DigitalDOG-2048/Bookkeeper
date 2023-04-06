// code learned https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-7.0
using SQLite;

namespace BookKeeper.Data;

public class AccountBookDatabase
{
    SQLiteAsyncConnection database;

    private async Task Init()
    {
        if (database != null)
            return;

        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

        await database.CreateTableAsync<AccountBook>();
    }

    public async Task<List<AccountBook>> GetAllAccountBooks()
    {
        await Init();

        return await database.Table<AccountBook>().ToListAsync();
    }

    public async Task<AccountBook> GetAccountBookByID(int id)
    {
        await Init();

        return await database.Table<AccountBook>().Where(i => i.ID == id).FirstOrDefaultAsync();
    }

    public async Task<int> InsertAccountBookAsync(AccountBook accountBook)
    {
        await Init();

        return await database.InsertAsync(accountBook);
    }

    public async Task<int> UpdateAccountBookAsync(AccountBook accountBook)
    {
        await Init();

        return await database.UpdateAsync(accountBook);
    }

    public async Task<int> DeleteAccountBookAsync(int id)
    {
        await Init();

        return await database.DeleteAsync(new AccountBook { ID = id });
    }
}

