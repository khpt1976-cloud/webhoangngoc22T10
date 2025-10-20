using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using OrchardCore.Users;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;
using OrchardCore.Email;
using HoangNgoc.UserProfile.Services;
using HoangNgoc.UserProfile.ViewModels;
using HoangNgoc.JobPosting.Services;

namespace HoangNgocCMS.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<IUser> _userManager;
        private readonly SignInManager<IUser> _signInManager;
        private readonly IUserProfileService _userProfileService;
        private readonly IJobApplicationService _jobApplicationService;
        private readonly IEmailService _emailService;

        public AccountController(
            IUserService userService,
            UserManager<IUser> userManager,
            SignInManager<IUser> signInManager,
            IUserProfileService userProfileService,
            IJobApplicationService jobApplicationService,
            IEmailService emailService)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _userProfileService = userProfileService;
            _jobApplicationService = jobApplicationService;
            _emailService = emailService;
        }

        // GET: /account/login
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToLocal(returnUrl);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        // POST: /account/login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            ViewData["ReturnUrl"] = model.ReturnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email) as User;
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                // Track login
                await _userProfileService.TrackUserLoginAsync(user.UserId);
                
                return RedirectToLocal(model.ReturnUrl);
            }

            if (result.RequiresTwoFactor)
            {
                return RedirectToAction("LoginWith2fa", new { returnUrl = model.ReturnUrl, rememberMe = model.RememberMe });
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Account is locked out. Please try again later.");
            }
            else if (result.IsNotAllowed)
            {
                ModelState.AddModelError("", "Account is not allowed to sign in. Please confirm your email.");
            }
            else
            {
                ModelState.AddModelError("", "Invalid email or password.");
            }

            return View(model);
        }

        // GET: /account/register
        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToLocal(returnUrl);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        // POST: /account/register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            ViewData["ReturnUrl"] = model.ReturnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if email already exists
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email is already registered.");
                return View(model);
            }

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Create user profile
                await _userProfileService.CreateUserProfileAsync(new UserProfileCreateModel
                {
                    UserId = user.UserId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Country = model.Country,
                    UserType = model.UserType,
                    AllowMarketing = model.AllowMarketing,
                    AllowDataProcessing = model.AllowDataProcessing
                });

                // Send confirmation email
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", 
                    new { userId = user.UserId, token = token }, Request.Scheme);

                await _emailService.SendEmailConfirmationAsync(model.Email, model.FirstName, callbackUrl);

                // Sign in user
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("RegisterSuccess");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        // GET: /account/register-success
        [HttpGet]
        public IActionResult RegisterSuccess()
        {
            return View();
        }

        // GET: /account/confirm-email
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest("Invalid email confirmation request.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            
            return View(result.Succeeded ? "ConfirmEmailSuccess" : "ConfirmEmailError");
        }

        // GET: /account/profile
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User) as User;
            if (user == null)
            {
                return NotFound();
            }

            var profile = await _userProfileService.GetUserProfileAsync(user.UserId);
            var applications = await _jobApplicationService.GetUserApplicationsAsync(user.UserId);
            var savedJobs = await _jobApplicationService.GetUserSavedJobsAsync(user.UserId);

            var viewModel = new UserProfileViewModel
            {
                User = user,
                Profile = profile,
                Applications = applications,
                SavedJobs = savedJobs,
                ApplicationsCount = applications.Count,
                SavedJobsCount = savedJobs.Count
            };

            return View(viewModel);
        }

        // POST: /account/update-profile
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(UpdateProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid data provided." });
            }

            try
            {
                var user = await _userManager.GetUserAsync(User) as User;
                if (user == null)
                {
                    return Json(new { success = false, message = "User not found." });
                }

                await _userProfileService.UpdateUserProfileAsync(user.UserId, new UserProfileUpdateModel
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    Location = model.Location,
                    Website = model.Website,
                    Bio = model.Bio,
                    Skills = model.Skills
                });

                return Json(new { success = true, message = "Profile updated successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while updating your profile." });
            }
        }

        // POST: /account/change-password
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid data provided." });
            }

            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Json(new { success = false, message = "User not found." });
                }

                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                
                if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    return Json(new { success = true, message = "Password changed successfully." });
                }

                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Json(new { success = false, message = errors });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while changing your password." });
            }
        }

        // POST: /account/upload-avatar
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadAvatar(IFormFile avatar)
        {
            if (avatar == null || avatar.Length == 0)
            {
                return Json(new { success = false, message = "No file selected." });
            }

            // Validate file type and size
            var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png" };
            if (!allowedTypes.Contains(avatar.ContentType.ToLower()))
            {
                return Json(new { success = false, message = "Only JPEG and PNG images are allowed." });
            }

            if (avatar.Length > 2 * 1024 * 1024) // 2MB
            {
                return Json(new { success = false, message = "File size must be less than 2MB." });
            }

            try
            {
                var user = await _userManager.GetUserAsync(User) as User;
                if (user == null)
                {
                    return Json(new { success = false, message = "User not found." });
                }

                var avatarUrl = await _userProfileService.UploadUserAvatarAsync(user.UserId, avatar);
                
                return Json(new { success = true, avatarUrl = avatarUrl, message = "Avatar updated successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while uploading your avatar." });
            }
        }

        // POST: /account/logout
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: /account/forgot-password
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // POST: /account/forgot-password
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return RedirectToAction("ForgotPasswordConfirmation");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account", 
                new { token = token }, Request.Scheme);

            await _emailService.SendPasswordResetEmailAsync(model.Email, callbackUrl);

            return RedirectToAction("ForgotPasswordConfirmation");
        }

        // GET: /account/reset-password
        [HttpGet]
        public IActionResult ResetPassword(string token = null)
        {
            if (token == null)
            {
                return BadRequest("A token must be supplied for password reset.");
            }

            var model = new ResetPasswordViewModel { Token = token };
            return View(model);
        }

        // POST: /account/reset-password
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        // Helper method
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}