using System.Text.RegularExpressions;
using SQLite;

namespace BookKeeper.Models;

[Table("Records")]
public class Record
{   
    //sqlite database usage from https://jesseliberty.com/2022/08/01/learning-net-maui-part-15-sqlite/
    [PrimaryKey, AutoIncrement, Column("ID")]
    public Guid ID { get; set; }

    public ExpensesType ExpensesType { get; set; }
    public IncomeType IncomeType { get; set; }
    public string Type { get; set; }
    public bool IsExpenses { get; set; }
    public string Remarks { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }

    //[Column("AccountBookID"), ForeignKey(typeof(AccountBook))]
    public int AccountBookID { get; set; }
}

public enum ExpensesType
{
    Childcare,
    Debt,
    Entertainment,
    Giving,
    Groceries,
    HealthAndFitness,
    Housing,
    Investments,
    Insurance,
    Miscellaneous,
    PetCare,
    PhoneBill,
    Restaurants,
    Transportation,
    Utilities,
}

public enum IncomeType
{
    CapitalGains,
    Dividend,
    Earned,
    Interest,
    Profit,
    Rental,
    Royalty,
}