using Microsoft.VisualBasic;
using TaskManagementSystem.DTOs.TaskDTOs;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Mapper.TaskMap
{
    public static class TaskMapper
    {
        public static TaskEntity ToTask(this TaskDto taskDto, string userId)
        {
            var task = new TaskEntity()
            {
                TaskId = new Guid(),
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate =taskDto.DueDate,
                Status = taskDto.Status,
                Priority = taskDto.Priority,
                UserId = userId,
            };

            return task;
        }
    }
}
