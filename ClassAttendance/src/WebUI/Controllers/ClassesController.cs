using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Facades;

namespace WebUI.Controllers
{
    public class ClassesController : Controller
    {
        private readonly ClassesFacade _classesFacade;

        public ClassesController(ClassesFacade classesFacade)
        {
            _classesFacade = classesFacade;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var models = await _classesFacade.GetClassViewModelsAsync();

            return View(models);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var lecturer = _classesFacade.GetClassManageViewModel();

            return View(lecturer);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(Class item, List<string> groups)
        {
            if (ModelState.IsValid)
            {
                await _classesFacade.AddClassAsync(item, groups);

                return RedirectToAction(nameof(List));
            }

            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var item = await _classesFacade.GetClassManageViewModel(id);

            return View(item);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(Class item, List<string> groups)
        {
            if (ModelState.IsValid)
            {
                await _classesFacade.UpdateClassAsync(item, groups);

                return RedirectToAction(nameof(List));
            }

            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _classesFacade.DeleteClassAsync(id);

            return RedirectToAction(nameof(List));
        }
    }
}