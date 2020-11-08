using Spectrum.Repository.Abstractions;
using Spectrum.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spectrum.Repository.Services
{
    public class FakeUserAccessDataService : IDataAccessService<User>
    {
        public async Task<int> DeleteAllEntitiesAsync()
        {
            return await Task.FromResult(1);
        }

        public async Task<IList<User>> GetEntitiesAsync()
        {
            var user = new User { 
                FirstName = "Rodrigo", 
                Id = 1, 
                LastName = "Rodriguez", 
                Password = "12345Abcd", 
                Start = DateTime.Today, 
                UserName = "RR" 
            };
            var users = new List<User> { user };
            return await Task.FromResult(users);
        }

        public async Task<int> SaveEntityAsync(User entity)
        {
            return await Task.FromResult(1);
        }

        public async Task<bool> VerifyCredentials(User credentials)
        {
            return await Task.FromResult(true);
        }
    }
}
