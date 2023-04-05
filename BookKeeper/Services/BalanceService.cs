using System;
using System.Collections.Generic;
using BookKeeper.Data;
namespace BookKeeper.Services;

public class BalanceService
{
	List<Balance> balanceList = new();

    RecordDatabase recordDatabase = App.RecordDatabase;

    public BalanceService()
	{
    }

    public async Task<List<Balance>> GetBalanceList(int year, int accountBookID)
	{
		if (balanceList.Count > 0)
			balanceList.Clear();

		foreach (int month in Constants.MonthRange)
        {
			DateTime firstDayOfMonth = new DateTime(year, month, 1);
			DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

			List<Record> monthRecords = await recordDatabase.GetRecordsByDateRangeAsync(firstDayOfMonth, lastDayOfMonth, accountBookID);

            decimal monthExpensesAmount = 0;
            decimal monthIncomeAmount = 0;
            foreach (Record record in monthRecords)
			{
				if (record.IsExpenses)
					monthExpensesAmount += record.Amount;
				else
					monthIncomeAmount += record.Amount;
			}

			balanceList.Add(new Balance
			{
				Year = year,
				Month = month,
				ExpensesAmount = monthExpensesAmount,
				IncomeAmount = monthIncomeAmount,
				BalanceAmount = monthExpensesAmount + monthIncomeAmount,
			});
		}

		return balanceList;
	}

    public Task<List<Balance>> GetBalanceList(string year, int accountBookID)
	{
		return GetBalanceList(Int32.Parse(year), accountBookID);
	}

	public Balance GetYearBalance(string year, ObservableCollection<Balance> monthBalanceList)
	{
        decimal income = 0, expenses = 0;
        foreach (Balance balance in monthBalanceList)
        {
            income += balance.IncomeAmount;
            expenses += balance.ExpensesAmount;
        }

		return new Balance
		{
			Year = Int32.Parse(year),
			Month = 0,
			ExpensesAmount = expenses,
			IncomeAmount = income,
			BalanceAmount = expenses + income,
		};
    }
}

