using ASPWebsiteShopping.Data;
using ASPWebsiteShopping.Models;
using ASPWebsiteShopping.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)),ServiceLifetime.Transient);


builder.Services.AddDatabaseDeveloperPageExceptionFilter();



builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = true;

} ).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
//Depency Injection
//builder.Services.AddScoped<ICategoryService, CategoryService>();//share doi tuong refresh=>chuyen trang thai doi tuong A->B
//builder.Services.AddTransient<ICategoryService, CategoryService>();//new Obj khoi tao moi
builder.Services.AddScoped<ICategoryService, CategoryService>();//instance A
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IProductTagService, ProductTagService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<IAttributeService, AttributeService>();
builder.Services.AddScoped<ISpeciesService, SpeciesService>();
builder.Services.AddScoped<IProductSpeciesService, ProductSpeciesService>();
builder.Services.AddAuthorization(options =>
{
    //Role
    options.AddPolicy("ProductRole", policy => policy.RequireRole("Admin","Content","Manager"));
    //ClaimUser
    options.AddPolicy("CreateProduct", policy => policy.RequireClaim("Create Product"));
    options.AddPolicy("ListProduct", policy => policy.RequireClaim("List Product"));
    options.AddPolicy("EditProduct", policy => policy.RequireClaim("Edit Product"));
    options.AddPolicy("DeleteProduct", policy => policy.RequireClaim("Delete Product"));

    options.AddPolicy("DeleteCategory", policy => policy.RequireClaim("Delete Category"));
    options.AddPolicy("CreateCategory", policy => policy.RequireClaim("Create Category"));
    options.AddPolicy("EditCategory", policy => policy.RequireClaim("Edit Category"));
    options.AddPolicy("ListCategory", policy => policy.RequireClaim("List Category"));

});
/*builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = new PathString("/Account/Denied");
        options.LoginPath = new PathString("/Account/Login");
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
    });*/
/*builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();*/

/*builder.Services.AddIdentity<ModelIdentityUser,IdentityRole>();*/


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

//route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UI}/{action=Index}/{id?}");

//
app.MapRazorPages();

app.Run();
