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
            //Category
            new Claim("Delete Category","Delete Category"),
             new Claim("Edit Category","Edit Category"),
              new Claim("Create Category","Create Category"),
               new Claim("List Category","List Category"),
        };
    }
}
