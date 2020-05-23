using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Models.ViewModels.StudentModel;
using WebUI.Facades;
using Microsoft.Extensions.Logging;
using System;
using WebUI.Models;
using BLL.Exceptions;
using Microsoft.AspNetCore.Authorization;
using WebUI.Identity;

namespace WebUI.Controllers
{
    [Authorize(Roles = Roles.Manager)]
    public class StudentsController : Controller
    {
        private readonly StudentsFacade _studentsFacade;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(StudentsFacade studentsFacade, ILogger<StudentsController> logger)
        {
            _studentsFacade = studentsFacade;
            _logger = logger;
        }

        public Uri Referer => new Uri(Request.Headers["Referer"].ToString());

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var studentList = await _studentsFacade.GetStudentListAsync();
            _logger.LogInformation("Fetched students");

            return View(studentList);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = _studentsFacade.GetViewModel();

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(StudentManageViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _studentsFacade.AddStudentAsync(model.Student);
                    _logger.LogInformation("Added student");

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

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _studentsFacade.GetViewModel(id);

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(StudentManageViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _studentsFacade.EditStudentAsync(model.Student);
                    _logger.LogInformation("Updated student");

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

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _studentsFacade.DeleteStudentAsync(id);
                _logger.LogInformation("Deleted student");

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