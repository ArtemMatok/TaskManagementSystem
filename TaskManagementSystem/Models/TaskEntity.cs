using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Enum;


namespace TaskManagementSystem.Models
{
    public class TaskEntity
    {
        [Key]
        public Guid TaskId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Enum.TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        //Relation
        public string UserId { get; set; }
        public AppUser User { get; set; }


    }
}
