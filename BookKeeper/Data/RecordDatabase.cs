using SQLite;

namespace BookKeeper.Data
{
	public class RecordDatabase
    {
        SQLiteAsyncConnection database;

        private async Task Init()
        {
            if (database != null)
                return;

            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            // for testing
            await database.DropTableAsync<Record>();

            await database.CreateTableAsync<Record>();
        }

        public async Task<List<Record>> GetRecordsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            await Init();

            // todo
            List<Record> records = await database.Table<Record>().ToListAsync();
            //return await database.QueryAsync<Record>("SELECT * FROM [Record] WHERE ...");

            return records;
        }

        public async Task<int> SaveRecordAsync(Record record)
        {
            await Init();

            if (record.ID != 0)
            {
                // Update an existing record
                return await database.UpdateAsync(record);
            }
            else
            {
                // Save a new record
                return await database.InsertAsync(record);
            }
        }

        public async Task<int> DeleteRecordAsync(int id)
        {
            await Init();

            return await database.DeleteAsync(new Record { ID = id });
        }
    }
}

