using MPMobile.Entity;
using SQLite;


namespace MPMobile.Data
{
    public class OffDataBase
    {
        SQLiteAsyncConnection Database;

        public OffDataBase()
        {
            if (Database is not null)
                return;
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            Database.CreateTableAsync<OffLineEntity>().Wait();
        }

        public async Task<int> SaveItemAsync(OffLineEntity item)
        {
            return await Database.InsertAsync(item);
        }

        public async Task<IEnumerable<OffLineEntity>> GetLastConsumerAsync()
        {
           return await Database.Table<OffLineEntity>().ToListAsync();
        }
       
    }
}
