using TaskManagementSystem.Data;
using TaskManagementSystem.Interfaces.ITaskRepo;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Repositories.TaskRepo
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskEntity> CreateTask(TaskEntity task)
        {
            await _context.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public Task<bool> DeleteTask(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public Task<TaskEntity> UpdateTask(Guid taskId, TaskEntity updateTask)
        {
            throw new NotImplementedException();
        }
    }
}
