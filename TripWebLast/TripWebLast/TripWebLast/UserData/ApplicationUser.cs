using Microsoft.AspNetCore.Identity;

namespace TripWebLast.Data;

public class ApplicationUser : IdentityUser
{
    public string SecurityQuestion { get; set; }
    public string SecurityAnswer { get; set; }
}