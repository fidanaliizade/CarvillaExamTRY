using Microsoft.AspNetCore.Identity;

namespace CarvillaExamTRY.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
