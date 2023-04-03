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

            await database.CreateTableAsync<Record>();
        }

        public async Task<List<Record>> GetRecordsByKeywords(DateTime startDate, DateTime endDate, List<string> keywords)
        {
            await Init();

            // todo
            List<Record> records = await database.Table<Record>().ToListAsync();

            return records;
        }

        public async Task<List<Record>> GetRecordsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            await Init();

            // todo
            List<Record> records = await database.Table<Record>().ToListAsync();
            //return await database.QueryAsync<Record>("SELECT * FROM [Record] WHERE ...");

            return records;
        }

        public async Task<int> UpdateRecordAsync(Record record)
        {
            await Init();

            return await database.UpdateAsync(record);
        }

        public async Task<int> InsertRecordAsync(Record record)
        {
            await Init();

            return await database.InsertAsync(record);
        }

        public async Task<int> DeleteRecordAsync(Guid id)
        {
            await Init();

            return await database.DeleteAsync(new Record { ID = id });
        }
    }
}

