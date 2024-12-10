using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_SQLSERVER.Abstractions.Interfaces
{
    public interface IRepository<T>
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();

    }
}
