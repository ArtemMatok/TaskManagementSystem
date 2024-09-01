using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using TaskManagementSystem.Enum;

namespace TaskManagementSystem.DTOs.TaskDTOs
{
    public class TaskDto
    {
        
       
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Enum.TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
    }
}
