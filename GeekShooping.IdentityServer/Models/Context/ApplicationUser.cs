using Microsoft.AspNetCore.Identity;

namespace GeekShooping.IdentityServer.Models.Context
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
