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
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IIdentityServerInteractionService interactionService,
            EmailService emailService,
            IConfiguration configuration) =>
            (_signInManager, _userManager, _interactionService, _emailService, _configuration) =
            (signInManager, userManager, interactionService, emailService, configuration);

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ClientUrl = _configuration["ClientURL"];
            var viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            ViewBag.ClientUrl = _configuration["ClientURL"];
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
            ViewBag.ClientUrl = _configuration["ClientURL"];
            var viewModel = new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            ViewBag.ClientUrl = _configuration["ClientURL"];
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

                await _emailService.SendEmailAsync(viewModel.Email, "Завершение регистрации",
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
            ViewBag.ClientUrl = _configuration["ClientURL"];
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
            ViewBag.ClientUrl = _configuration["ClientURL"];
            ViewBag.IntermediatePage = true;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            ViewBag.ClientUrl = _configuration["ClientURL"];
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }

        [HttpGet]
        public IActionResult CheckEmail(string returnUrl)
        {
            ViewBag.ClientUrl = _configuration["ClientURL"];
            var viewModel = new CheckEmailViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CheckEmail(CheckEmailViewModel viewModel)
        {
            ViewBag.ClientUrl = _configuration["ClientURL"];
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userManager.FindByEmailAsync(viewModel.Email);
            if (user != null && await _userManager.IsEmailConfirmedAsync(user))
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action(
                    "RecoverPassword",
                    "Auth",
                    new { userId = user.Id, code, returnUrl = viewModel.ReturnUrl },
                    protocol: HttpContext.Request.Scheme);

                await _emailService.SendEmailAsync(viewModel.Email, "Восстановление пароля",
                    $"Для восстановления пароля перейдите по <a href='{callbackUrl}'>ссылке</a>.");
            }

            return RedirectToAction("WaitingForConfirmPasswordRecovery");
        }

        [HttpGet]
        public IActionResult WaitingForConfirmPasswordRecovery()
        {
            ViewBag.ClientUrl = _configuration["ClientURL"];
            ViewBag.IntermediatePage = true;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RecoverPassword(string userId, string code, string returnUrl)
        {
            ViewBag.ClientUrl = _configuration["ClientURL"];
            if (userId == null || code == null)
            {
                ModelState.AddModelError(string.Empty, "Некорректная ссылка для восстановления пароля.");
                return View("Error");
            }

            var viewModel = new RecoverPasswordViewModel
            {
                UserId = userId,
                Code = code,
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel viewModel)
        {
            ViewBag.ClientUrl = _configuration["ClientURL"];
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (viewModel.NewPassword != viewModel.ConfirmNewPassword)
            {
                ModelState.AddModelError(string.Empty, "Пароли не совпадают.");
                return View(viewModel);
            }

            var user = await _userManager.FindByIdAsync(viewModel.UserId);
            if (user == null)
            {
                return RedirectToAction("PasswordResetConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, viewModel.Code, viewModel.NewPassword);
            if (result.Succeeded)
            {
                return RedirectToAction("PasswordResetConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult PasswordResetConfirmation()
        {
            ViewBag.ClientUrl = _configuration["ClientURL"];
            ViewBag.IntermediatePage = true;
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword(string returnUrl)
        {
            ViewBag.ClientUrl = _configuration["ClientURL"];
            var viewModel = new ChangePasswordViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            ViewBag.ClientUrl = _configuration["ClientURL"];
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (viewModel.NewPassword != viewModel.ConfirmNewPassword)
            {
                ModelState.AddModelError(string.Empty, "Новый пароль и подтверждение не совпадают.");
                return View(viewModel);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Пользователь не найден.");
                return View(viewModel);
            }

            var result = await _userManager.ChangePasswordAsync(user, viewModel.OldPassword, viewModel.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                return Redirect(viewModel.ReturnUrl ?? "/");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(viewModel);
        }
    }
}