using TaskManagementSystem.Enum;

namespace TaskManagementSystem.DTOs.TaskFilterDTOs
{
    public class TaskFilterDto
    {
        public Enum.TaskStatus? Status { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskPriority? Priority { get; set; }
    }
}
