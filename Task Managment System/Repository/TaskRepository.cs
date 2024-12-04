using Task_Managment_System.Data;
using Task_Managment_System.Models;
using Task_Managment_System.Repository.IRepository;

namespace Task_Managment_System.Repository
{
    public class TaskRepository : Repository<Tasks>, ITaskRepository
    {

        private readonly ApplicationDbContext _db;

        public TaskRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._db = dbContext;
        }

        public async Task UpdateAsync(Tasks entity)
        {
            _db.Tasks.Update(entity);
            await SaveAsync();
        }
    }
}
