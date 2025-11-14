using SQLite;
using MauiAppProjeto2.Models;

namespace MauiAppProjeto2.Helper
{
    public class SQLiteDatabaseHelper
    {
  
        readonly SQLiteAsyncConnection _db;

   
        public SQLiteDatabaseHelper(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<User>().Wait();
        }
    }
}
