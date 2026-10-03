using Microsoft.AspNetCore.Identity;

namespace Auth.Models
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole()
        {
            Id = Guid.NewGuid().ToString();
        }

    }
}
