using Microsoft.VisualBasic;
using TaskManagementSystem.DTOs.TaskDTOs;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Mapper.TaskMap
{
    public static class TaskMapper
    {
        public static TaskEntity ToTask(this TaskDto taskDto, string userId)
        {
            return new TaskEntity()
            {
                TaskId = new Guid(),
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate =taskDto.DueDate,
                Status = taskDto.Status,
                Priority = taskDto.Priority,
                UserId = userId,
            };

        }

        public static TaskEntity ToUpdateTask(this TaskDto taskDto,Guid taskId, string userId)
        {
            return new TaskEntity()
            {
                TaskId = taskId,
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                Status = taskDto.Status,
                Priority = taskDto.Priority,
                UserId = userId,
            };
        }

        public static TaskResultDto ToTaskResultDto(this TaskEntity taskEntity)
        {
            return new TaskResultDto()
            {
                TaskId = taskEntity.TaskId,
                Title = taskEntity.Title,
                Description = taskEntity.Description,
                DueDate = taskEntity.DueDate,
                Status = taskEntity.Status,
                Priority = taskEntity.Priority,
                CreatedAt = taskEntity.CreatedAt,
                UpdatedAt = taskEntity.UpdatedAt,
                UserId = taskEntity.UserId,
                UserName = taskEntity.User.UserName
            };
        }
    }
}
