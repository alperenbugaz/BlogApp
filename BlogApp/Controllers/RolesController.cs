using BlogApp.Data.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

[Authorize (Roles = "Admin")]
public class RolesController:Controller
{   
    private readonly RoleManager<BlogAppRole> _roleManager;
    private readonly UserManager<BlogAppUser> _userManager;

    public RolesController(RoleManager<BlogAppRole> roleManager,UserManager<BlogAppUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;


    }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var roleUsers = new Dictionary<string, List<string>>();
            foreach (var role in roles)
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                roleUsers[role.Id] = usersInRole.Select(u => u.UserName).ToList();
            }

            ViewBag.RoleUsers = roleUsers;
            return View(roles);
        }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(BlogAppRole model)
    {   
        if(ModelState.IsValid)
        {        
        IdentityResult result = await _roleManager.CreateAsync(model);
        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }
        else
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);

        }
        return View(model);

    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {   
        BlogAppRole role = await _roleManager.FindByIdAsync(id);
        if (role != null)
        {   
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                ViewBag.Users = usersInRole;
            return View(role);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(BlogAppRole model)
    {
        if (ModelState.IsValid)
        {
            IdentityResult result = await _roleManager.UpdateAsync(model);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        BlogAppRole role = await _roleManager.FindByIdAsync(id);
        if (role != null)
        {
            IdentityResult result = await _roleManager.DeleteAsync(role);
        }
        return RedirectToAction("Index");
    }
}