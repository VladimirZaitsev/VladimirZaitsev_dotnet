using BLL.Exceptions;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebUI.Facades;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly SubjectsFacade _subjectsFacade;
        private readonly ILogger<SubjectsController> _logger;

        public SubjectsController(SubjectsFacade subjectsFacade, ILogger<SubjectsController> logger)
        {
            _subjectsFacade = subjectsFacade;
            _logger = logger;
        }

        public Uri Referer => new Uri(Request.Headers["Referer"].ToString());

        [HttpGet]
        public IActionResult List()
        {
            var models = _subjectsFacade.GetSubjects();
            _logger.LogInformation("Fetched subject");

            return View(models);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var lecturer = new Subject();

            return View(lecturer);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(Subject subject)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _subjectsFacade.AddSubjectAsync(subject);
                    _logger.LogInformation("Added subject");

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

            return View(subject);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var lecturer = await _subjectsFacade.GetByIdAsync(id);

            return View(lecturer);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(Subject subject)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _subjectsFacade.UpdateSubjectAsync(subject);
                    _logger.LogInformation("Updated subject");

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

            return View(subject);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _subjectsFacade.DeleteSubjectAsync(id);
                _logger.LogInformation("Deleted subject");

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