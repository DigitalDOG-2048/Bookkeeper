// code from https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-7.0
using System;
namespace BookKeeper;

public static class Constants
{
    public const string DatabaseFilename = "BookKeeper.db3";

    public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    public const int DefaultExpensesType = 2;
    public const int DefaultIncomeType = 2;

    public const int DefaultAccountID = 0;

    public static IEnumerable<int> MonthRange = Enumerable.Range(1, 12);
    public static IEnumerable<int> YearRange = Enumerable.Range(DateTime.Now.Year-20, 21);
}

