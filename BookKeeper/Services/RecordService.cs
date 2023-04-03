using System.Text.RegularExpressions;
using BookKeeper.Data;
using Microsoft.Maui.Controls;
using SQLite;
using Syncfusion.Maui.Calendar;

namespace BookKeeper.Services;

public class RecordService
{
    List<AccountBook> accountBookList = new();

    RecordDatabase recordDatabase = App.RecordDatabase;

    public async Task<List<Record>> GetRecordsByDateRangeAsync(CalendarDateRange calendarDateRange)
    {
        DateTime startDate, endDate;

        if (calendarDateRange == null)
        {
            DateTime now = DateTime.Now;
            startDate = new DateTime(now.Year, now.Month, 1);
            endDate = startDate.AddMonths(1).AddDays(-1);
        }
        else
        {
            startDate = (DateTime)calendarDateRange.StartDate;
            endDate = (DateTime)calendarDateRange.EndDate;
        }

        return await recordDatabase.GetRecordsByDateRangeAsync(startDate, endDate);
    }

    public async Task<List<AccountBook>> GetAccountBookList()
    {
        if (accountBookList?.Count > 0)
            return accountBookList;

        // get account books from database
        accountBookList.Add(new AccountBook { AccountBookName = "Personal", ID = 1 });
        accountBookList.Add(new AccountBook { AccountBookName = "Trip", ID = 2 });

        if (accountBookList.Count == 0)
        {
            accountBookList.Add(new AccountBook { AccountBookName = "Personal", ID = 1 });
            // add default account book to database
        }

        return accountBookList;
    }

    public AccountBook GetAccountBook(int id)
    {
        return accountBookList[id-1];
    }

    public void GetRecordTypeLists(List<string> expensesTypeList, List<string> incomeTypeList)
    {
        if (incomeTypeList.Count > 0)
            incomeTypeList.Clear();
        if (expensesTypeList.Count > 0)
            expensesTypeList.Clear();

        foreach (ExpensesType type in (ExpensesType[])Enum.GetValues(typeof(ExpensesType)))
        {
            string typeStr = Regex.Replace(Enum.GetName(typeof(ExpensesType), type),
                "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
            expensesTypeList.Add(typeStr);
        }
        foreach (IncomeType type in (IncomeType[])Enum.GetValues(typeof(IncomeType)))
        {
            string typeStr = Regex.Replace(Enum.GetName(typeof(IncomeType), type),
                "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
            incomeTypeList.Add(typeStr);
        }
    }

    public async Task<int> SaveRecordAsync(Record record)
    {
        return await recordDatabase.SaveRecordAsync(record);
    }

    public async Task DeleteRecordByIdAsync(int id)
    {
        try
        {
            int res = await recordDatabase.DeleteRecordAsync(id);
            if (res == -1)
                await Shell.Current.DisplayAlert("Error", "Failed to delete in database", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }
}

