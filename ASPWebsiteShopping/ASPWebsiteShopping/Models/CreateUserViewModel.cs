using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ASPWebsiteShopping.Models
{
    public class CreateUserViewModel
    {
        public UserViewModel User { get; set; }
        public virtual List<IdentityRole>? Roles { get; set; }
    }
}
