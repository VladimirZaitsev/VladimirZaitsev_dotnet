using BLL.Exceptions;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebUI.Facades;
using WebUI.Identity;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize(Roles = Roles.Manager)]
    public class LecturersController : Controller
    {
        private readonly LecturersFacade _lecturersFacade;
        private readonly ILogger<LecturersController> _logger;

        public LecturersController(LecturersFacade lecturersFacade, ILogger<LecturersController> logger)
        {
            _lecturersFacade = lecturersFacade;
            _logger = logger;
        }

        public Uri Referer => new Uri(Request.Headers["Referer"].ToString());

        [AllowAnonymous]
        [HttpGet]
        public IActionResult List()
        {
            var models = _lecturersFacade.GetLecturers();
            _logger.LogInformation("Fetched lecturers");

            return View(models);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var lecturer = new Lecturer();

            return View(lecturer);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _lecturersFacade.AddLecturerAsync(lecturer);
                    _logger.LogInformation("Added new lecturer");

                    return RedirectToAction(nameof(List));
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

            return View(lecturer);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var lecturer = await _lecturersFacade.GetByIdAsync(id);

            return View(lecturer);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _lecturersFacade.UpdateLecturerAsync(lecturer);
                    _logger.LogInformation("Updated lecturer");

                    return RedirectToAction(nameof(List));
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

            return View(lecturer);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _lecturersFacade.DeleteLecturerAsync(id);
                _logger.LogInformation("Deleted lecturer");
                return RedirectToAction(nameof(List));
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
    }
}