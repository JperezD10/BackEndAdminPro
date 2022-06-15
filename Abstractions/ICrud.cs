using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions
{
    public interface ICrud<T>
    {
        //hace las dos: inserta y actualiza
        T Save(T entity);
        Task<T> SaveAsync(T entity);

        IList<T> GetAll();

        Task<IList<T>> GetAllAsync();

        T GetById(int id);

        Task<T> GetByIdAsync(int id);

        void Delete(int id);

        Task DeleteAsync(int id);
    }
}
