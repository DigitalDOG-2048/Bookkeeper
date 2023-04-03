using SQLite;

namespace BookKeeper.Models;

[Table("AccountBooks")]
public class AccountBook
{
    [PrimaryKey, AutoIncrement, Column("ID")]
    public int ID { get; set; }

    [MaxLength(250), Unique]
    public string AccountBookName { get; set; }

    //[OneToMany]
    //public List<Record.ID> Records { get; set; }
}