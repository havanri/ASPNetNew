using Microsoft.AspNetCore.Identity;

namespace ASPWebsiteShopping.Models
{
    public class EditUserViewModel
    {
        public UserViewModel User { get; set; }
        public virtual List<IdentityRole>? Roles { get; set; }
    }
}
