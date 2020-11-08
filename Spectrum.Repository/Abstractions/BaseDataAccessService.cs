using Spectrum.Repository.Entities;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spectrum.Repository.Abstractions
{
    public abstract class BaseDataAccessService
    {
        private SQLiteAsyncConnection db = null;
        private readonly IDatabaseDeviceLocation _databasePath;
        public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database accesss
        SQLite.SQLiteOpenFlags.SharedCache;

        public BaseDataAccessService(IDatabaseDeviceLocation databasePath)
        {
            _databasePath = databasePath;
        }

        protected async Task<SQLiteAsyncConnection> GetInstance(string filePath = null)
        {
            if (filePath == null)
            {
                filePath = "Spectrum.db3";
            }

            if (db == null)
            {
                db = new SQLiteAsyncConnection(_databasePath.GetPath(filePath), Flags);
                await db.CreateTableAsync<User>();
            }

            return db;
        }
    }
}
