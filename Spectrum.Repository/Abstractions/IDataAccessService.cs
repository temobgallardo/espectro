using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spectrum.Repository.Abstractions
{
    public interface IDataAccessService<T>
    {
        Task<bool> AlreadyRegistered(T entity);
        Task<bool> VerifyCredentials(T credentials);
        Task<IList<T>> GetEntitiesAsync();
        Task<int> SaveEntityAsync(T entity);
        Task<int> DeleteAllEntitiesAsync();
    }
}