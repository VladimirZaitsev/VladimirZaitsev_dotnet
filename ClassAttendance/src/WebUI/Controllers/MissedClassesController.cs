using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Models.ViewModels.MissedClass;
using WebUI.Facades;
using WebUI.Models;
using Microsoft.Extensions.Logging;
using BLL.Exceptions;
using Microsoft.AspNetCore.Authorization;
using WebUI.Identity;
using WebUI.Extensions;

namespace WebUI.Controllers
{
    [Authorize(Roles = Roles.Manager)]
    public class MissedClassesController : Controller
    {
        private readonly MissedClassesFacade _missedClassesFacade;
        private readonly ILogger<MissedClassesController> _logger;

        public MissedClassesController(MissedClassesFacade missedClassesFacade, ILogger<MissedClassesController> logger)
        {
            _missedClassesFacade = missedClassesFacade;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var models = await _missedClassesFacade.GetMissedLecturesAsync();
            _logger.LogInformation("Fetched lecturers");

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var lecturer = await _missedClassesFacade.GetManageViewModelAsync();

            return View(lecturer);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(MissedClassManageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var missedClass = new MissedClass
                {
                    ClassId = model.ClassId,
                    StudentId = model.StudentId,
                };

                try
                {
                    await _missedClassesFacade.AddAsync(missedClass);
                    _logger.LogInformation("Added new missed class");

                    return RedirectToAction(nameof(List));

                }
                catch (BusinessLogicException ex)
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

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var item = await _missedClassesFacade.GetManageViewModelAsync(id);

            return View(item);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(MissedClassManageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var missedClass = new MissedClass
                {
                    ClassId = model.ClassId,
                    StudentId = model.StudentId,
                };

                try
                {
                    await _missedClassesFacade.UpdateAsync(missedClass);
                    _logger.LogInformation("Updated missed class");

                    return RedirectToAction(nameof(List));

                }
                catch (BusinessLogicException ex)
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

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _missedClassesFacade.DeleteAsync(id);
                _logger.LogInformation("Deleted missed class");

                return RedirectToAction(nameof(List));

            }
            catch (BusinessLogicException ex)
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

        [Authorize(Roles = Roles.UserAndManager)]
        [HttpGet]
        public async Task<IActionResult> Student(int id)
        {
            try
            {
                var viewModels = await _missedClassesFacade.GetStudentMissedClassesAsync(id);
                _logger.LogInformation("Fetched students's missed classes");

                return View(nameof(List), viewModels);
            }
            catch (BusinessLogicException ex)
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

        [Authorize(Roles = Roles.UserAndManager)]
        [HttpGet]
        public async Task<IActionResult> Lecturer(int id)
        {
            try
            {
                var viewModels = await _missedClassesFacade.GetLecturerMissedClassesAsync(id);
                _logger.LogInformation("Fetched lecturer's missed classes");

                return View(nameof(List), viewModels);
            }
            catch (BusinessLogicException ex)
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