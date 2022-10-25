using Microsoft.AspNetCore.Identity;

namespace StoreApi.DATA.Entities
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
