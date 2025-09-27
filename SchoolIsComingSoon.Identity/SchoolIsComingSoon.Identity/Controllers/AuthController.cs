using Duende.IdentityModel;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolIsComingSoon.Identity.Common.Constants;
using SchoolIsComingSoon.Identity.Models;
using SchoolIsComingSoon.Identity.Services;
using System.Security.Claims;

namespace SchoolIsComingSoon.Identity.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AuthController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IIdentityServerInteractionService interactionService) =>
            (_signInManager, _userManager, _interactionService) =
            (signInManager, userManager, interactionService);

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userManager.FindByNameAsync(viewModel.Username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View(viewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(viewModel.Username,
                viewModel.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect(viewModel.ReturnUrl);
            }
            ModelState.AddModelError(string.Empty, "Login error");
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var viewModel = new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = new AppUser
            {
                UserName = viewModel.Username,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email
            };

            var result = await _userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action(
                    "ConfirmEmail",
                    "Auth",
                    new ConfirmEmailViewModel { UserId = user.Id, Code = code, ReturnUrl = viewModel.ReturnUrl },
                    protocol: HttpContext.Request.Scheme);

                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(viewModel.Email, "Завершение регистрации",
                    $"Подтвердите регистрацию, перейдя по <a href='{callbackUrl}'>ссылке</a>.");

                return RedirectToAction("WaitingForEmailConfirmation");
            }
            ModelState.AddModelError(string.Empty, "Error occurred");
            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailViewModel viewModel)
        {
            if (viewModel.UserId != null && viewModel.Code != null)
            {
                var user = await _userManager.FindByIdAsync(viewModel.UserId);

                if (user != null)
                {
                    var result = await _userManager.ConfirmEmailAsync(user, viewModel.Code);

                    if (result.Succeeded)
                    {
                        _userManager.AddToRoleAsync(user, Roles.User).GetAwaiter().GetResult();
                        var claims = _userManager.AddClaimsAsync(user, new Claim[]
                        {
                            new Claim(JwtClaimTypes.Name, user.UserName),
                            new Claim(JwtClaimTypes.Name, user.LastName),
                            new Claim(JwtClaimTypes.Name, user.FirstName),
                            new Claim(JwtClaimTypes.Name, user.Email),
                            new Claim(JwtClaimTypes.Role, Roles.User)
                        }).Result;

                        await _signInManager.SignInAsync(user, false);
                        return Redirect(viewModel.ReturnUrl);
                    }
                }
            }

            ModelState.AddModelError(string.Empty, "Error occurred");
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult WaitingForEmailConfirmation()
        {
            ViewBag.IntermediatePage = true;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}