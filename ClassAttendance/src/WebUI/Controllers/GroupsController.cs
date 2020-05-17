using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebUI.Facades;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class GroupsController : Controller
    {
        private readonly GroupsFacade _groupFacade;
        private readonly ILogger<GroupsController> _logger;

        public GroupsController(GroupsFacade groupFacade, ILogger<GroupsController> logger)
        {
            _groupFacade = groupFacade;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var models = await _groupFacade.GetGroupViewModelsAsync();
            _logger.LogInformation("Fetched groups");

            return View(models);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var lecturer = new Group();

            return View(lecturer);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(Group group)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _groupFacade.AddGroupAsync(group);

                    return RedirectToAction(nameof(List));
                }
                catch (ArgumentException ex)
                {
                    _logger.LogError(ex.Message);
                    var error = new ErrorViewModel
                    {
                        ErrorMessage = ex.Message,
                    };

                    return View("Error", error);
                }  
            }

            return View(group);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var lecturer = await _groupFacade.GetByIdAsync(id);

            return View(lecturer);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(Group group)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _groupFacade.UpdateGroupAsync(group);

                    return RedirectToAction(nameof(List));
                }
                catch (ArgumentException ex)
                {
                    _logger.LogError(ex.Message);
                    var error = new ErrorViewModel
                    {
                        ErrorMessage = ex.Message,
                    };

                    return View("Error", error);
                }
            }

            return View(group);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _groupFacade.DeleteGroupAsync(id);

                return RedirectToAction(nameof(List));
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message);
                var error = new ErrorViewModel
                {
                    ErrorMessage = ex.Message,
                };

                return View("Error", error);
            }
        }
    }
}
