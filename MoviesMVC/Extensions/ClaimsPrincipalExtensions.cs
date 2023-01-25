using System.Security.Claims;

namespace MoviesMVC.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetEmail(this ClaimsPrincipal user)
        {
            return user.Identity?.Name;
        }
    }
}