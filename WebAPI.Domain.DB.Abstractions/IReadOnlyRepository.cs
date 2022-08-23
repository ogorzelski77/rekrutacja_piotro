using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Domain.Db.Abstrasctions
{
    public interface IReadOnlyRepository<T>
        where T : class
    {
        Task<T> Get(long id);
        Task<IEnumerable<T>> GetAll();
        Task<T> First(string condition, object param = null);
        Task<IEnumerable<T>> Where(string condition, object param = null);
    }
}