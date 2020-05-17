using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Models.ViewModels.MissedClass;
using WebUI.Facades;

namespace WebUI.Controllers
{
    public class MissedClassesController : Controller
    {
        private readonly MissedClassesFacade _missedClassesFacade;

        public MissedClassesController(MissedClassesFacade missedClassesFacade)
        {
            _missedClassesFacade = missedClassesFacade;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var models = await _missedClassesFacade.GetMissedLecturesAsync();

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
                await _missedClassesFacade.AddAsync(missedClass);

                return RedirectToAction(nameof(List));
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

                await _missedClassesFacade.UpdateAsync(missedClass);

                return RedirectToAction(nameof(List));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _missedClassesFacade.DeleteAsync(id);

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Student(int id)
        {
            var viewModels = await _missedClassesFacade.GetStudentMissedClassesAsync(id);

            return View(nameof(List), viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Lecturer(int id)
        {
            var viewModels = await _missedClassesFacade.GetLecturerMissedClassesAsync(id);

            return View(nameof(List), viewModels);
        }
    }
}