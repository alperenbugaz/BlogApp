using BlogApp.Data.Concrete;
using BlogApp.ViewModels;
using IdentityApp.Data.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

public class AccountController:Controller
{   
    private readonly UserManager<BlogAppUser> _userManager;
    private readonly SignInManager<BlogAppUser> _signInManager;

    public AccountController(UserManager<BlogAppUser> userManager,SignInManager<BlogAppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login()
    {   

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if(ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if(user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);


                if(result.Succeeded)
                {
                    return RedirectToAction("Index","Home");
                }
            }

            ModelState.AddModelError("","Invalid user name or password");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {  

        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if(ModelState.IsValid)
        {
            var user = new BlogAppUser()
            {
                UserName = model.UserName,
                Name = model.Name,
                Surname = model.Surname,
                ProfileImageUrl = "/images/ProfilePhoto/pp.png"
            };

            var result = await _userManager.CreateAsync(user,model.Password);

            if(result.Succeeded)
            {
                return RedirectToAction("Login","Account");
            }

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("",error.Description);
                
            }
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index","Home");
    }

}