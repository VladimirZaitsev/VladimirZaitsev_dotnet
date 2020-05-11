using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Services;

namespace WebUI.Controllers
{
    public class ClassController : Controller
    {
        private readonly ClassService _classService;

        public ClassController(ClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var models = await _classService.GetClassViewModelsAsync();

            return View(models);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var lecturer = _classService.GetClassManageViewModel();

            return View(lecturer);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(Class item, List<string> groups)
        {
            if (ModelState.IsValid)
            {
                await _classService.AddClassAsync(item, groups);

                return RedirectToAction(nameof(List));
            }

            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var item = await _classService.GetClassManageViewModel(id);

            return View(item);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(Class item, List<string> groups)
        {
            if (ModelState.IsValid)
            {
                await _classService.UpdateClassAsync(item, groups);

                return RedirectToAction(nameof(List));
            }

            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _classService.DeleteClassAsync(id);

            return RedirectToAction(nameof(List));
        }
    }
}