using Microsoft.EntityFrameworkCore;
using WebApi.DataBase;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        SqlContext _context;
        DbSet<T> _dataSet;
        public GenericRepository(SqlContext context)
        {
            _context = context;
            _dataSet = _context.Set<T>();
        }
        public async Task<T> Create(T obj)
        {
            _dataSet.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task Delete(long id)
        {
            var existingItem = await GetById(id);
            if (existingItem == null)
                return;


            _dataSet.Remove(existingItem);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<List<T>> GetAll()
        {

            return await _dataSet.ToListAsync();
        }

        public async Task<T> GetById(long id)
        {
            return await _dataSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> Update(T obj)
        {
            var existingItem = await GetById(obj.Id);

            if (existingItem != null)
            {
                _dataSet.Entry(existingItem).CurrentValues.SetValues(obj);
                await _context.SaveChangesAsync();
                return existingItem;

            }
            return null;
        }

        public async Task<bool> Exists(long id)
        {
            return await _dataSet.CountAsync(x => x.Id == id) >= 1;
        }

    }
}
