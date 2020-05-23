using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebUI.Extensions;
using WebUI.Facades;
using WebUI.Identity;
using WebUI.Models;
using WebUI.Models.Account;

namespace WebUI.Controllers
{
    [Authorize(Roles = Roles.Manager)]
    public class UsersController : Controller
    {
        private readonly UsersFacade _usersFacade;
        private readonly ILogger<UsersController> _logger;

        public UsersController(UsersFacade usersFacade, ILogger<UsersController> logger)
        {
            _usersFacade = usersFacade;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await _usersFacade.GetUsersAsync();
            _logger.LogInformation("Fetched user list");

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var user = await _usersFacade.GetUserAsync(id);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(User user)
        {
            try
            {
                await _usersFacade.UpdateUserAsync(user);
                _logger.LogInformation("Updated user");

                return RedirectToAction(nameof(List));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message);
                var error = new ErrorViewModel
                {
                    ErrorMessage = ex.Message,
                    ReturnUrl = Request.GetReferer(),
                };

                return View("Error", error);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _usersFacade.DeleteUserAsync(id);
                _logger.LogInformation("Deleted user");

                return RedirectToAction(nameof(List));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message);
                var error = new ErrorViewModel
                {
                    ErrorMessage = ex.Message,
                    ReturnUrl = Request.GetReferer(),
                };

                return View("Error", error);
            }
        }
    }
}
