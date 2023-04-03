using System;
namespace BookKeeper.Models;

public class Balance
{
	public int Year { get; set; }
	public int Month { get; set; }
	public decimal ExpensesAmount { get; set; }
	public decimal IncomeAmount { get; set; }
	public decimal BalanceAmount { get; set; }
}

