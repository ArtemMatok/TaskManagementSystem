using TaskManagementSystem.DTOs.TaskFilterDTOs;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Interfaces.ITaskRepo
{
    public interface ITaskRepository
    {
        Task<TaskEntity> CreateTask(TaskEntity task);
        Task<TaskEntity?> UpdateTask(Guid taskId, TaskEntity updateTask);
        Task<bool> DeleteTask(TaskEntity task);
        Task<TaskEntity?> GetEntityById(Guid taskId);
        Task<List<TaskEntity>> GetTasks(TaskFilterDto filter, int pageNumber, int pageSize, string userId);
    }
}
