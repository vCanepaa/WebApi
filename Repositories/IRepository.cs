using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> Create(T obj);

        Task<T> Update(T obj);

        Task<T> GetById(long id);

        Task<List<T>> GetAll();

        Task Delete(long id);

        Task<bool> Exists(long id);
    }
}
