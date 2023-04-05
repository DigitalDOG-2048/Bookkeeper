using System.Text.RegularExpressions;
using BookKeeper.Data;
using Microsoft.Maui.Controls;
using SQLite;
using Syncfusion.Maui.Calendar;

namespace BookKeeper.Services;

public class RecordService
{
    RecordDatabase recordDatabase = App.RecordDatabase;

    public async Task<List<Record>> GetRecordsByDateRangeAsync(CalendarDateRange calendarDateRange, int accountBookID)
    {
        DateTime startDate, endDate;

        if (calendarDateRange == null)
        {
            DateTime now = DateTime.Now;
            startDate = new DateTime(now.Year, 1, 1);
            endDate = new DateTime(now.Year, 12, 30);
        }
        else
        {
            startDate = (DateTime)calendarDateRange.StartDate;
            endDate = (DateTime)calendarDateRange.EndDate;
        }

        return await recordDatabase.GetRecordsByDateRangeAsync(startDate, endDate, accountBookID);
    }

    public async Task<List<Record>> GetYearRecordsByKeywordAsync(int year, string keyword, int accountBookID)
    {
        if (!Constants.YearRange.Contains(year))
            year = DateTime.Now.Year;

        DateTime firstDayOfYear = new DateTime(year, 1, 1);
        DateTime lastDayOfYear = new DateTime(year, 12, 30);

        if (keyword == null || keyword == "")
        {
            return await recordDatabase.GetRecordsByDateRangeAsync(firstDayOfYear, lastDayOfYear, accountBookID);
        }

        List<string> keywordList = keyword.Split(' ').ToList();
        return await recordDatabase.GetRecordsByKeywords(firstDayOfYear, lastDayOfYear, keywordList, accountBookID);
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

    public async Task<int> AddRecordAsync(Record record)
    {
        return await recordDatabase.InsertRecordAsync(record);
    }

    public async Task<int> EditRecordAsync(Record record)
    {
        return await recordDatabase.UpdateRecordAsync(record);
    }

    public async Task<int> DeleteRecordByIDAsync(Guid id)
    {
        return await recordDatabase.DeleteRecordAsync(id);
    }
}

