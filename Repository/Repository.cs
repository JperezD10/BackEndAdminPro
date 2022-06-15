using Abstractions;

namespace Repository
{
    public class Repository<T> : IRepository<T>
    {
        private readonly IDbContext<T> _dbContext;

        public Repository(IDbContext<T> dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            _dbContext.Delete(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _dbContext.DeleteAsync(id);
        }

        public IList<T> GetAll()
        {
            return _dbContext.GetAll();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _dbContext.GetAllAsync();
        }

        public T GetById(int id)
        {
            return _dbContext.GetById(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.GetByIdAsync(id);
        }

        public T Save(T entity)
        {
            return _dbContext.Save(entity);
        }

        public async Task<T> SaveAsync(T entity)
        {
            return await _dbContext.SaveAsync(entity);
        }
    }
}