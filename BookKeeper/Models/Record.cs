namespace BookKeeper.Models;

public class Record
{
    public int RecordID { get; set; }
	public string Type { get; set; }
	public string Note { get; set; }
    public decimal Amount { get; set; }
    public DateTime Time { get; set; }
    public int AccountBookID { get; set; }
    public string AccountBookName { get; set; }
}

