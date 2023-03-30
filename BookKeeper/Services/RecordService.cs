namespace BookKeeper.Services;

public class RecordService
{
    //HttpClient httpClient;
    public RecordService()
    {
        //httpClient = new HttpClient();

    }

    List<Record> recordList = new();
    List<AccountBook> accountBookList = new();

    public async Task<List<Record>> GetRecords()
    {
        if (recordList?.Count > 0)
            return recordList;

        // Get recordList from database
        recordList.Add(new Record {
            RecordID = 1,
            Type = "Dining",
            Note = "Soup",
            Amount = (decimal)-9.99,
            DateTime = DateTime.Now,
            AccountBookID = 1,
            AccountBookName = "Default"
        });
        
        return recordList;
    }

    public async Task<List<AccountBook>> GetAccountBookList()
    {
        if (accountBookList?.Count > 0)
            return accountBookList;

        // get account books from database
        accountBookList.Add(new AccountBook { AccountBookID = 1, AccountBookName = "Default" });
        accountBookList.Add(new AccountBook { AccountBookID = 2, AccountBookName = "Trip" });

        if (accountBookList.Count == 0)
        {
            accountBookList.Add(new AccountBook { AccountBookID = 1, AccountBookName = "Default" });
            // add default account book to database
        }

        return accountBookList;
    }
}

