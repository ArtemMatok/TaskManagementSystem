using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services.AuthToken
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
