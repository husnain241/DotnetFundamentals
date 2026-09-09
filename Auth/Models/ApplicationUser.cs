using Microsoft.AspNetCore.Identity;

namespace Auth.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
