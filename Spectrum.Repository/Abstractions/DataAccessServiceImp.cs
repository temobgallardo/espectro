using Spectrum.Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spectrum.Repository.Abstractions
{
    public class DataAccessServiceImp : BaseDataAccessService, IDataAccessService<User>
    {
        public DataAccessServiceImp(IDatabaseDeviceLocation databasePath) : base(databasePath)
        {
        }

        public async Task<IList<User>> GetEntitiesAsync()
        {
            var db = await GetInstance();
            return await db.Table<User>().ToListAsync().ConfigureAwait(false);
        }

        public async Task<int> SaveEntityAsync(User entity)
        {
            var db = await GetInstance();
            if (entity.Id != 0)
            {
                return await db.UpdateAsync(entity).ConfigureAwait(false);
            }
            else
            {
                return await db.InsertAsync(entity).ConfigureAwait(false); ;
            }
        }

        public async Task<int> DeleteAllEntitiesAsync()
        {
            var db = await GetInstance();
            return await db.DeleteAllAsync<User>().ConfigureAwait(false); ;
        }

        public async Task<bool> VerifyCredentials(User credentials)
        {
            var db = await GetInstance();
            var table = db.Table<User>();
            var result = table.Where(x => x.UserName == credentials.UserName && x.Password == credentials.Password);
            //var query = $"select first() from {db.Table<User>()} where  UserName == {credentials.UserName} and Password == {credentials.Password}";
            //var user = await db.FindWithQueryAsync<User>(query);
            return await result.CountAsync() >= 1;
        }

        public async Task<bool> AlreadyRegistered(User entity)
        {
            var db = await GetInstance();
            var table = db.Table<User>();
            var result = table.Where(x => x.UserName == entity.UserName 
            || x.FirstName == entity.FirstName 
            || x.LastName == entity.LastName 
            || x.PhoneNumber == entity.PhoneNumber);
            return await result.CountAsync() >= 1;
        }
    }
}
