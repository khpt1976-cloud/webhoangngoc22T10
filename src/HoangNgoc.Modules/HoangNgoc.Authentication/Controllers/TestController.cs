using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Modules;
using OrchardCore.Users;
using OrchardCore.Users.Models;

namespace HoangNgoc.Authentication.Controllers;

[Feature("HoangNgoc.Authentication")]
public class TestController : Controller
{
    private readonly SignInManager<IUser> _signInManager;
    private readonly UserManager<IUser> _userManager;

    public TestController(
        SignInManager<IUser> signInManager,
        UserManager<IUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var model = new
        {
            IsAuthenticated = User.Identity?.IsAuthenticated ?? false,
            UserName = User.Identity?.Name ?? "Anonymous",
            Claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList()
        };

        return Json(model);
    }

    [Authorize]
    public IActionResult Secure()
    {
        return Json(new
        {
            Message = "This is a secure endpoint",
            User = User.Identity?.Name,
            Roles = User.Claims.Where(c => c.Type == "role").Select(c => c.Value).ToList()
        });
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password, string? returnUrl = null)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ViewBag.Error = "Username and password are required";
            return View();
        }

        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            ViewBag.Error = "Invalid username or password";
            return View();
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
        if (result.Succeeded)
        {
            return Redirect(returnUrl ?? "/");
        }

        ViewBag.Error = "Invalid username or password";
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/");
    }
}