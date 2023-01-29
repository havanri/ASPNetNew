using System.Security.Claims;

namespace ASPWebsiteShopping.Data
{
    public static class ApplicationClaimTypes
    {
        public static List<Claim> claims = new List<Claim>()
        {
            new Claim("Create Product","Create Product"),
            new Claim("Edit Product","Edit Product"),
            new Claim("List Product","List Product"),
            new Claim("Delete Product","Delete Product"),
        };
    }
}
