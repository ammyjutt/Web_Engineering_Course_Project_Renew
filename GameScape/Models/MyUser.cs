using Microsoft.AspNetCore.Identity;

namespace GameScape.Models
{
    public class MyUser : IdentityUser
    {
        public string Country { get; set; }



    }
}
