using System.Linq.Expressions;
using Task_Managment_System.Models;

namespace Task_Managment_System.Repository.IRepository
{
    public interface IRepositroy<T> where T : class
    {

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool isTracked = true);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync();
    }
}
