using BLL.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebUI.Facades;
using WebUI.Models;
using WebUI.Models.Account;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly AccountFacade _accountFacade;

        public AccountController(ILogger<AccountController> logger, AccountFacade accountFacade)
        {
            _logger = logger;
            _accountFacade = accountFacade;
        }

        public Uri Referer => new Uri(Request.Headers["Referer"].ToString());

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginModel();

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _accountFacade.SignInAsync(model);
                    _logger.LogInformation("User signed in");

                    return RedirectToAction("Index", "Home");
                }
                catch (BusinessLogicException ex)
                {
                    _logger.LogError(ex.Message);
                    var error = new ErrorViewModel
                    {
                        ErrorMessage = ex.Message,
                        ReturnUrl = Referer,
                    };

                    return View("Error", error);
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterModel();

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _accountFacade.RegisterAsync(model);
                    _logger.LogInformation("Created new user");

                    return RedirectToAction("Index", "Home");
                }
                catch (BusinessLogicException ex)
                {
                    _logger.LogError(ex.Message);
                    var error = new ErrorViewModel
                    {
                        ErrorMessage = ex.Message,
                        ReturnUrl = Referer,
                    };

                    return View("Error", error);
                }
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await _accountFacade.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
