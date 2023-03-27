using Android.Accounts;
using static Android.Provider.ContactsContract.CommonDataKinds;

namespace BookKeeper.Services;

public class RecordService
{
    //HttpClient httpClient;
    public RecordService()
    {
        //httpClient = new HttpClient();

    }

    List<RecordModel> recordList = new();
    List<AccountBookModel> accountBookList = new();

    public async Task<List<RecordModel>> GetRecords()
    {
        if (recordList?.Count > 0)
            return recordList;

        // Get recordList from database
        recordList.Add(new RecordModel {RecordId = 1, Type = "Dining", Note = "Soup", Amount = (decimal)-9.99, Time = DateTime.Now, AccountBookID = 1});
        
        return recordList;
    }

    public async Task<List<AccountBookModel>> GetAccountBookList()
    {
        if (accountBookList?.Count > 0)
            return accountBookList;

        // get account books from database
        accountBookList.Add(new AccountBookModel { AccountBookID = 1, AccountBookName = "Default" });
        accountBookList.Add(new AccountBookModel { AccountBookID = 2, AccountBookName = "Trip" });

        if (accountBookList.Count == 0)
        {
            accountBookList.Add(new AccountBookModel { AccountBookID = 1, AccountBookName = "Default" });
            // add default account book to database
        }

        return accountBookList;
    }
}

