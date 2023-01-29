using ASPWebsiteShopping.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public IActionResult Index()
		{
			if (User.Identity.IsAuthenticated)
			{
                return View("Views/Admin/Dashboard/Index.cshtml");
            }
            return RedirectToAction("Login");
        }
		[HttpGet]
		public IActionResult Login()
		{
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View("Views/Admin/Account/LogIn.cshtml");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
			if (ModelState.IsValid)
			{
				/*var usersignedUser = _userManager.FindByEmailAsync(model.Email);*/
                var result = await _signInManager.PasswordSignInAsync("riprodao@gmail.com", model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View("Views/Admin/Account/LogIn.cshtml");
        }
        public IActionResult Register()
        {
            return View("Views/Admin/Account/Register.cshtml");
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
			if (ModelState.IsValid)
			{
				var user = new IdentityUser { UserName = model.Email, Email = model.Email,EmailConfirmed=true};
				var result = await _userManager.CreateAsync(user,model.Password);
				if (result.Succeeded)
				{
					await _signInManager.SignInAsync(user,isPersistent:false);
					return RedirectToAction("Index");
				}
				foreach(var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
            return View("Views/Admin/Account/Register.cshtml");
        }
		
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("login", "Account");
		}
    }
}
