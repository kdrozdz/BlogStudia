using Microsoft.AspNetCore.Identity;

namespace BlogProject.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
