using System.Text.RegularExpressions;
using SQLite;

namespace BookKeeper.Models;

[Table("Records")]
public class Record
{
    [PrimaryKey, AutoIncrement, Column("ID")]
    public int ID { get; set; }

    public ExpensesType ExpensesType { get; set; }
    public IncomeType IncomeType { get; set; }
    public string Type { get; set; }
    public bool IsExpenses { get; set; }
    public string Remarks { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }

    //[Column("AccountBookID"), ForeignKey(typeof(AccountBook))]
    public int AccountBookID { get; set; }

    //public static string RecordTypeToString(RecordType type)
    //{
    //    string camelCase = Enum.GetName(typeof(RecordType), type);
    //    return Regex.Replace(camelCase, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
    //}

    //public static RecordType stringToRecordType(string typeStr)
    //{
    //    string camelCase = Regex.Replace(typeStr, @"\s+", "");
    //    return (RecordType)Enum.Parse(typeof(RecordType), camelCase);
    //}
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