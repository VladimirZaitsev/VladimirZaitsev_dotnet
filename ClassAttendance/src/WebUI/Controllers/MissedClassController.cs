using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Models.ViewModels.MissedClass;
using WebUI.Services;

namespace WebUI.Controllers
{
    public class MissedClassController : Controller
    {
        private readonly MissedClassService _missedClassService;

        public MissedClassController(MissedClassService missedClassService)
        {
            _missedClassService = missedClassService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var models = await _missedClassService.GetMissedLecturesAsync();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var lecturer = await _missedClassService.GetManageViewModelAsync();

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
                await _missedClassService.AddAsync(missedClass);

                return RedirectToAction(nameof(List));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var item = await _missedClassService.GetManageViewModelAsync(id);

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

                await _missedClassService.UpdateAsync(missedClass);

                return RedirectToAction(nameof(List));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _missedClassService.DeleteAsync(id);

            return RedirectToAction(nameof(List));
        }
    }
}