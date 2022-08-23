using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Domain.Db.Abstrasctions
{
    public interface IRepository<T>
        where T : class
    {
        Task<T> Get(long id);
        Task<IEnumerable<T>> GetAll();
        Task<T> First(string condition, object param = null);
        Task<IEnumerable<T>> Where(string condition, object param = null);
        Task<int> Add(T entity);
        Task<int> Delete(long id);
        Task<int> Update(T entity);
        Task<int> Update(string condition, object conditionParams, object columnsSet);
        Task<T> Upsert(T entity, object columnsSet);
    }
}