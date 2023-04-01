namespace BookKeeper.Models;

public class Record
{
    public int RecordID { get; set; }
	public string Type { get; set; }
	public string Note { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public int AccountBookID { get; set; }
    public string AccountBookName { get; set; }
    public bool isExpenses { get; set; }

    //public string GetMonthString()
    //{
    //    return DateTime.Month.ToString();
    //}
}

