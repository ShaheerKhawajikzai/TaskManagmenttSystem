using Task_Managment_System.Models;

namespace Task_Managment_System.Repository.IRepository
{
    public interface ITaskRepository : IRepositroy<Tasks>
    {
        Task UpdateAsync(Tasks entity);
    }
}
