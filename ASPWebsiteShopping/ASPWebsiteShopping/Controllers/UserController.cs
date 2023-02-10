using ASPWebsiteShopping.Components;
using ASPWebsiteShopping.Data;
using ASPWebsiteShopping.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Claims;
using System.Web.WebPages;

namespace ASPWebsiteShopping.Controllers
{
	public class UserController : Controller
	{
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserController(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            /*var role = new IdentityRole();
            role.Name = "Admin";
            await _roleManager.CreateAsync(role);*/
            var users =_userManager.Users.ToList();
            ViewData["isSidebar"] = "User";
            return View("Views/Admin/User/Index.cshtml", users);
        }
        public IActionResult Create()
        {
            var model = new CreateUserViewModel();
            model.Roles = _roleManager.Roles.ToList();
            return View("Views/Admin/User/Create.cshtml",model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel createUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = createUserViewModel.User.UserName, Email = createUserViewModel.User.Email, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(user, createUserViewModel.User.Password);
                if (result.Succeeded)
                {
                    //role
                    if(createUserViewModel.User.Roles != null)
                    {
                        await _userManager.AddToRolesAsync(user, createUserViewModel.User.Roles);
                        await _signInManager.RefreshSignInAsync(user);
                    }
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View("Views/Admin/User/Create.cshtml", createUserViewModel);
        }
        public async Task<IActionResult> Edit(string? id)
        {
            var model = new EditUserViewModel();
            var userData = await _userManager.FindByIdAsync(id);
            model.User = new UserViewModel
            {
                Id = userData.Id,
                UserName = userData.UserName,
                Email = userData.Email,
                Password=userData.PasswordHash,
                Roles = await _userManager.GetRolesAsync(userData)
            };
            model.Roles = _roleManager.Roles.ToList();
            //convert html option
            OptionRoleEdit optionRoleEdit = new OptionRoleEdit(model.User.Roles,model.Roles);
            HtmlString htmlOptionRoles = new HtmlString(optionRoleEdit.returnOptionRole());
            ViewData["htmlOptionRoles"] = htmlOptionRoles;

            return View("Views/Admin/User/Edit.cshtml", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel editUserViewModel)
        {
            if (ModelState.IsValid)
            {
                
                var user = await _userManager.FindByIdAsync(editUserViewModel.User.Id);
                var listRole =await _userManager.GetRolesAsync(user);
                //
                user.UserName = editUserViewModel.User.UserName;
                user.Email = editUserViewModel.User.Email;
                var result = await _userManager.UpdateAsync(user);
                //
                if (result.Succeeded)
                {
                    //role
                    if (editUserViewModel.User.Roles != null)
                    {
                        //remove
                        var listRoleRemove = new List<string>();
                        foreach (var item in listRole)
                        {
                            if (!editUserViewModel.User.Roles.Contains(item))
                            {
                                listRoleRemove.Add(item);
                            }
                        }
                        await _userManager.RemoveFromRolesAsync(user, listRoleRemove);
                        //add
                        foreach(var item in editUserViewModel.User.Roles)
                        {
                            bool containItem = listRole.Any(e => e.Equals(item));
                            if (containItem==false)
                            {
                                await _userManager.AddToRoleAsync(user, item);
                            }
                        }
                        await _signInManager.RefreshSignInAsync(user);
                    }
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View("Views/Admin/User/Edit.cshtml", editUserViewModel);
        }
        public async Task Delete(string? id)
        {
            var user = await _userManager.FindByIdAsync(id);

            //
            await _userManager.DeleteAsync(user);
        }
        [HttpGet]
        public async Task<IActionResult> ManagerUserClaims(string? id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var existingClaim = await _userManager.GetClaimsAsync(user);

            var model = new UserClaimsViewModel()
            {
                UserId=id
            };

            foreach (var claim in ApplicationClaimTypes.claims)
            {
                UserClaim userClaims = new UserClaim
                {
                    ClaimType = claim.Type,
                };
                if (existingClaim.Any(c => c.Type == claim.Type))
                {
                    userClaims.IsSelected = true;
                }
                model.Claims.Add(userClaims);
            }
            return View("Views/Admin/Claim/AddUserClaim.cshtml", model);
        }
        [HttpPost]
        public async Task<IActionResult> ManagerUserClaims(UserClaimsViewModel userClaimsViewModel)
        {
            var user = await _userManager.FindByIdAsync(userClaimsViewModel.UserId);

            if (user == null)
            {
                return NotFound();
            }
            //remove
            var claims = await _userManager.GetClaimsAsync(user);
            var result = await _userManager.RemoveClaimsAsync(user, claims);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Không thể xóa tùy chọn quyền này cho người dùng");
                return View("Views/Admin/Claim/AddUserClaim.cshtml", userClaimsViewModel);
            }
            //add 
            result = await _userManager.AddClaimsAsync(user,userClaimsViewModel.Claims.Where(c=>c.IsSelected).Select(c=>new Claim(c.ClaimType,c.ClaimType)));
            /*            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                        authenticationManager.SignOut("ApplicationCookie");
                        authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);*/
            await _signInManager.RefreshSignInAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Không thể thêm tùy chọn quyền này cho người dùng");
                return View("Views/Admin/Claim/AddUserClaim.cshtml", userClaimsViewModel);
            }
            return RedirectToAction("Edit",new {Id=userClaimsViewModel.UserId});
        }

    }
}
