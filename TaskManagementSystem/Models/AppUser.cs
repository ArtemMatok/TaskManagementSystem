using Microsoft.AspNetCore.Identity;

namespace TaskManagementSystem.Models
{
    public class AppUser:IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }
        //Relation
        public List<TaskEntity> Tasks = new List<TaskEntity>();
    }
}
