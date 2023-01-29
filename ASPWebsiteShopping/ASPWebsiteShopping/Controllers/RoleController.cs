using ASPWebsiteShopping.Data;
using ASPWebsiteShopping.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;

namespace ASPWebsiteShopping.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            List<IdentityRole> roles = _roleManager.Roles.ToList();
            return View("Views/Admin/Role/Index.cshtml", roles);
        }
        public IActionResult Create()
        {
            var model = new CreateRoleViewModel();
            return View("Views/Admin/Role/Create.cshtml", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoleViewModel createRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole { Name=createRoleViewModel.Name};
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {                
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View("Views/Admin/Role/Create.cshtml", createRoleViewModel);
        }
        public async Task<IActionResult> Edit(string? id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var model = new EditRoleViewModel();


            model.Name = role.Name;
            model.Id = role.Id;

            return View("Views/Admin/Role/Edit.cshtml", model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditRoleViewModel editRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(editRoleViewModel.Id);
                role.Name = editRoleViewModel.Name;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View("Views/Admin/Role/Edit.cshtml", editRoleViewModel);
        }
       


        public async Task Delete(string? id)
        {
            var role =await _roleManager.FindByIdAsync(id);

            //
            await _roleManager.DeleteAsync(role);

        }
    }
}
