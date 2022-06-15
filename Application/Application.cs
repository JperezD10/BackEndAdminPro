using Abstractions;
using Repository;

namespace Application
{
    public class Application<T> : IApplication<T> where T : class, IEntity
    {
        private readonly IRepository<T> _repository;

        public Application(IRepository<T> repository)
        {
            _repository = repository;
        }
        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public IList<T> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public T Save(T entity)
        {
            return _repository.Save(entity);
        }

        public async Task<T> SaveAsync(T entity)
        {
            return await _repository.SaveAsync(entity);
        }
    }
}