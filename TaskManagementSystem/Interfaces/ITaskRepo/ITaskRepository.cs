using TaskManagementSystem.Models;

namespace TaskManagementSystem.Interfaces.ITaskRepo
{
    public interface ITaskRepository
    {
        Task<TaskEntity> CreateTask(TaskEntity task);
        Task<TaskEntity> UpdateTask(Guid taskId, TaskEntity updateTask);
        Task<bool> DeleteTask(Guid taskId);
    }
}
