using Microsoft.AspNetCore.Identity;

namespace TaskManagementSystem.Models
{
    public class User:IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }   
        
    }
}
