using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.DTOs.TaskFilterDTOs;
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

        public async Task<bool> DeleteTask(TaskEntity task)
        {
            

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TaskEntity?> GetEntityById(Guid taskId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.TaskId == taskId);

            if(task is null)
            {
                return null;
            }
            return task;
        }

        public async Task<List<TaskEntity>> GetTasks(TaskFilterDto filter, int pageNumber, int pageSize, string userId)
        {

            var tasks =await  _context.Tasks.ToListAsync();
            if(filter.Status.HasValue)
            {
                tasks = tasks.Where(x => x.Status == filter.Status.Value).ToList();
            }

            if(filter.DueDate.HasValue)
            {
                tasks = tasks.Where(x => x.DueDate == filter.DueDate.Value).ToList(); ;
            }

            if (filter.Priority.HasValue)
            {
                tasks = tasks.Where(x => x.Priority == filter.Priority.Value).ToList();
            }

            tasks = tasks.Where(x => x.UserId == userId)
                    .OrderBy(x => x.DueDate)
                    .ToList();

            tasks = tasks.Skip((pageNumber - 1) * pageSize)
                     .Take(pageSize)
                     .ToList();

            return tasks;
        }

        public async Task<TaskEntity?> UpdateTask(Guid taskId, TaskEntity updateTask)
        {
            var task = await GetEntityById(taskId); 

            if(task is null)
            {
                return null;
            }

            #region Update
            task.Title = updateTask.Title;  
            task.Description = updateTask.Description;  
            task.DueDate = updateTask.DueDate;
            task.Status = updateTask.Status;
            task.Priority = updateTask.Priority;
            task.UpdatedAt = DateTime.Now;
            #endregion

            _context.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }
    }
}
