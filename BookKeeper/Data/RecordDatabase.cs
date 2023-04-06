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

        private DateTime ConvertDate(DateTime dateTime, bool isStart)
        {
            if (isStart)
            {
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
            } else {
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
            }
        }

        public async Task<List<Record>> GetRecordsByDateRangeAsync(DateTime startDate, DateTime endDate, int accountBookID)
        {
            await Init();

            startDate = ConvertDate(startDate, true);
            endDate = ConvertDate(endDate, false);

            if (accountBookID < 0)
            {
                object[] queryArgs = { startDate, endDate };
                return await database.QueryAsync<Record>(
                    "SELECT * FROM Records WHERE DateTime BETWEEN ? AND ? ORDER BY DateTime DESC",
                    queryArgs);
            }
            else
            {
                object[] queryArgs = { accountBookID, startDate, endDate };
                return await database.QueryAsync<Record>(
                        "SELECT * FROM Records WHERE AccountBookID == ? AND DateTime BETWEEN ? AND ? ORDER BY DateTime DESC",
                        queryArgs);
            }
        }

        public async Task<List<Record>> GetRecordsByKeywords(DateTime startDate, DateTime endDate, List<string> keywords, int accountBookID)
        {
            await Init();

            startDate = ConvertDate(startDate, true);
            endDate = ConvertDate(endDate, false);

            List<object> queryArgs = new();
            queryArgs.Add(startDate);
            queryArgs.Add(endDate);

            if (accountBookID < 0)
            {
                string commandText = "SELECT * FROM Records WHERE DateTime BETWEEN ? AND ? AND (1=0";
                foreach (string keyword in keywords)
                {
                    string likeKeyword = "%" + keyword + "%";
                    commandText += " OR Type LIKE ? OR Remarks LIKE ?";
                    queryArgs.Add(likeKeyword);
                    queryArgs.Add(likeKeyword);
                }
                commandText += ") ORDER BY DateTime DESC";
                return await database.QueryAsync<Record>(commandText, queryArgs.ToArray());
            }
            else
            {
                string commandText = "SELECT * FROM Records WHERE DateTime BETWEEN ? AND ? AND AccountBookID == ? AND (1=0";
                queryArgs.Add(accountBookID);
                foreach (string keyword in keywords)
                {
                    string likeKeyword = "%" + keyword + "%";
                    commandText += " OR Type LIKE ? OR Remarks LIKE ?";
                    queryArgs.Add(likeKeyword);
                    queryArgs.Add(likeKeyword);
                }
                commandText += ") ORDER BY DateTime DESC";
                return await database.QueryAsync<Record>(commandText, queryArgs.ToArray());
            }
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

