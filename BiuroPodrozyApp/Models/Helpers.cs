using System.Security.Claims;

namespace BiuroPodrozyApp.Models
{
    public static class Helpers
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
