using TaskManagementSystem.Enum;

namespace TaskManagementSystem.DTOs.TaskDTOs
{
    public class TaskResultDto
    {
        public Guid TaskId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Enum.TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
