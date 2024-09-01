using TaskManagementSystem.Models;

namespace TaskManagementSystem.Interfaces.IAuthToken
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
