using System.ComponentModel.DataAnnotations;

namespace ASPWebsiteShopping.Models
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
