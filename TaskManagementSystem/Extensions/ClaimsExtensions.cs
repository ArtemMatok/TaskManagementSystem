using System.Security.Claims;

namespace TaskManagementSystem.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            var name = user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"))?.Value;
            if(name is null)
            {
                return "test";//for testing
            }
            return name;
        }
    }
}
