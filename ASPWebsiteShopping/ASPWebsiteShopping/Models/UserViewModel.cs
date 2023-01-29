using System.ComponentModel.DataAnnotations;

namespace ASPWebsiteShopping.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Roles = new List<string>();
        }
        public string? Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public IList<string> Roles { get; set; }
    }
}
