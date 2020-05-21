﻿using BLL.Exceptions;
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
        private ILogger<AccountController> _logger;
        private AccountFacade _accountFacade;

        public AccountController(ILogger<AccountController> logger, AccountFacade accountFacade)
        {
            _logger = logger;
            _accountFacade = accountFacade;
        }

        public Uri Referer => new Uri(Request.Headers["Referer"].ToString());

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginModel();

            return View(model);
        }

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

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterModel();

            return View(model);
        }

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

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await _accountFacade.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
